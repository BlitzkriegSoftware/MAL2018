// @ts-check
'use strict';

const settingsFile = './settings.json';
const dataFolder = 'seeddata';

const userCollection = 'users';
const postCollection = 'posts';

var fs = require('fs');
var path = require('path');
var http = require('http');

var mongo =  require("mongodb");
var mongoClient = require("mongodb").MongoClient;

// This is the format of the configuration file (case sensitive)
var _settings = {
    "ConnectionString": "",
    "Database": ""
};

fs.readFile(settingsFile, 'utf8', function (err, data) {
    if (err) throw err;

    _settings = JSON.parse(data);

    console.log(_settings.ConnectionString);
    console.log(_settings.Database);

    global.settings = _settings;

    setupMongo();
});

var setupMongo = function () {
    mongoClient.connect(settings.ConnectionString, function (err, db) {
        if (err) throw err;

        // Switch DB from 'admin' (default) to the desired Database, this is far from obvious
        db = db.db(settings.Database);
        console.log('Current DB: ' + db.databaseName);

        // if there are no records read the data folder and make some
        anyRecords(db, userCollection, makeUserTestData);
        anyRecords(db, postCollection, makePostsTestData);
    });
}

var anyRecords = function (db, collectionName, callback) {
    db.listCollections().toArray(function(err, collections) { 
        if (collections.includes(collectionName)) {
            db.collection(collectionName).find().toArray(function (err, items) {
                if (err) throw err;
                if (items) {
                    if (items.length <= 0) {
                        callback(db);
                    }
                }
            });
        } else {
            db.createCollection(collectionName, function (err, res) {
                if (err) throw err;
                callback(db);
            });
        }
    });
};

var makeUserTestData = function (db) {
    var dataFile = path.join(__dirname, dataFolder, 'users.json');
    fs.readFile(dataFile, 'utf8', function (err, data) {
        if (err) throw err;

        var users = JSON.parse(data);
        users.forEach(function (item, index) {
            db.collection(userCollection).insertOne(item, function(err, res) {
                if (err) throw err;
                console.log(res);
            });
        });

    });
}

var makePostsTestData = function (db) {

    var bucket = new mongo.GridFSBucket(db, {
        chunkSizeBytes: 1024,
        bucketName: 'images'
    });

    var dataFile = path.join(__dirname, dataFolder, 'posts.json');
    fs.readFile(dataFile, 'utf8', function (err, data) {
        if (err) throw err;

        var posts = JSON.parse(data);
        posts.forEach(function (item, index) {

            var imagePath = path.join(__dirname, dataFolder, item.image);
            fs.createReadStream(imagePath)
                .pipe(
                    bucket.openUploadStream(item.image)
                )
                .on('error', function (error) {
                    throw error;
                })
                .on('finish', function () {
                    db.collection(postCollection).insertOne(item, function(err, res) {
                        if (err) throw err;
                        console.log(res);
                    });
                });

        });
    });
}

process.exitCode = 0;