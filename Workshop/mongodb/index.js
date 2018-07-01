'use strict';

require('make-promises-safe')

var exitCode = 0;

var settings = {};

const settingsFile = './settings.json';
const dataFolder = 'seeddata';

const userCollection = 'users';
const postCollection = 'posts';

var util = require('util');

var fs = require('fs');
var readFile = util.promisify(fs.readFile);

var path = require('path');

var mongo = require('mongodb');
var mongoClient = require('mongodb').MongoClient;

var db = null;

readFile(settingsFile, 'utf8')
    .then((data) => {
        console.log(data);
        settings = JSON.parse(data);
        console.log(settings.ConnectionString);
        console.log(settings.Database);
        return settings;
    })
    .then((settings) => {
        return openDB(settings);
    })
    .then((_db) => { 
        db = _db
        return _db;
    })
    .then((db) => {
        return collectionExistsWithData(db, userCollection);
    })
    .then((flag) => {
       console.log(`Flag: ${ flag }`);
       if(flag) makeUserTestData();
    })
    .then((db) => {
        return collectionExistsWithData(db, postCollection);
    })
    .then((flag) => {
       console.log(`Flag: ${ flag }`);
       if(flag) makePostsTestData(db);
    })
    .then(() => {
        timeToGo(0);
    })
    .catch((err) => {
        console.log( err );
        timeToGo(1);
    });

// Useful Promises

function openDB(settings) {
    return new Promise((resolve, reject) => {
        mongoClient.connect(settings.ConnectionString)
            .then((_db) => {
                _db = _db.db(settings.Database);
                console.log('Current DB: ' + _db.databaseName);
                resolve(_db);
            })
    })
}

function collectionExistsWithData(db, collectionName) {
    return new Promise((resolve, reject) => {
        db.collection(collectionName).findOne({}, function(err, result) {
            if (err) reject( err );
            if(result) resolve(false);
            else {
                db.createCollection(collectionName, function (err, res) {
                    if (err) reject(err);
                    resolve(true);
                });
            }
        });
    })
}

// Helpers

var timeToGo = function(exitCode) {
    process.exit(exitCode);
}

var makeUserTestData = function (db) {
    var dataFile = path.join(__dirname, dataFolder, 'users.json');

    readFile(dataFile, 'utf8')
        .then((data) => {
            var users = JSON.parse(data);
            users.forEach(function (item, index) {
                db.collection(userCollection).insertOne(item, function (err, res) {
                    if (err) reject(err);
                    console.log(res);
                });
            });
        })
}

var makePostsTestData = function (db) {

    var dataFile = path.join(__dirname, dataFolder, 'posts.json');
    
    var bucket = new mongo.GridFSBucket(db, {
        chunkSizeBytes: 1024,
        bucketName: 'images'
    });

    readFile(dataFile, 'utf8')
        .then(function (data) {
            var posts = JSON.parse(data);
            posts.forEach(function (item, index) {
                makePost(db, item, bucket);
            });
        })
}

var makePost = function(db, item, bucket) {
    var imagePath = path.join(__dirname, dataFolder, item.image);

    fs.createReadStream(imagePath)
        .pipe(
            bucket.openUploadStream(item.image)
        )
        .on('error', function (err) {
            reject(err);
        })
        .on('finish', function () {
            db.collection(postCollection).insertOne(item, function (err, res) {
                if (err) reject(err);
                console.log(res);
                resolve(true);
            });
        });
}
