'use strict';

require('make-promises-safe')

var exitCode = 0;

const settingsFile = './settings.json';
const dataFolder = 'seeddata';

const userCollection = 'users';
const postCollection = 'posts';

var util = require('util');
var fs = require('fs');
const readFile = util.promisify(fs.readFile);

var path = require('path');

var mongo = require('mongodb');
var mongoClient = require('mongodb').MongoClient;

// Processing
async function Main() {
    var sData = await readFile(settingsFile, 'utf8');
    var settings = JSON.parse(sData);
    console.log(settings.ConnectionString);
    console.log(settings.Database);
    var db = await openDB(settings);
    var flag1 = await collectionExistsWithData(db, userCollection);
    if (flag1) {
        await makeUserTestData(db);
    }
    var flag2 = await collectionExistsWithData(db, postCollection);
    if (flag2) {
        await makePostsTestData(db);
    }
}

Main().then(() => {
    process.exit(exitCode);
});

// f(x)

async function openDB(settings) {
    var _db = await mongoClient.connect(settings.ConnectionString);
    _db = await _db.db(settings.Database);
    console.log('Current DB: ' + _db.databaseName);
    return _db;
}

async function collectionExistsWithData(db, collectionName) {
    var flag = false;
    var result = await db.collection(collectionName).findOne({});
    if (!result) {
        var res = await db.createCollection(collectionName);
        flag = true;
    }
    return flag;
}

async function makeUserTestData(db) {
    var res = {};
    var dataFile = path.join(__dirname, dataFolder, 'users.json');
    var data = await readFile(dataFile, 'utf8');
    var users = JSON.parse(data);
    users.forEach(function (item) {
        res = db.collection(userCollection).insertOne(item);
        console.log(res);
    });
    return true;
}

async function makePostsTestData(db) {
    var bucket = new mongo.GridFSBucket(db, {
        chunkSizeBytes: 1024,
        bucketName: 'images'
    });

    var dataFile = path.join(__dirname, dataFolder, 'posts.json');
    var data = await readFile(dataFile, 'utf8');
    var posts = JSON.parse(data);
    posts.forEach(function (item) {
        await makePost(db, item, bucket);
    });
    return true;
}

async function makePost(db, item, bucket) {

    var imagePath = path.join(__dirname, dataFolder, item.image);

    fs.createReadStream(imagePath)
        .pipe(
            bucket.openUploadStream(item.image)
        )
        .on('error', function (err) {
            throw err;
        })
        .on('finish', function () {
            var res = await db.collection(postCollection).insertOne(item);
            console.log(res);
            return true;
        });
}