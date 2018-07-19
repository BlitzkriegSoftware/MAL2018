'use strict';

require('make-promises-safe');

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

    var database = await openDB(settings);

    var db = await switchDB(settings, database);

    var flag1 = await needsData(db, userCollection);
    if (flag1) {
        await makeUserTestData(db);
    } else {
        console.log("No users needed");
    }

    var flag2 = await needsData(db, postCollection);
    if (flag2) {
        await makePostsTestData(db);
    } else {
        console.log("No posts on needed");
    }

    await database.close();
}

Main().then(() => {
    console.log("Done...");
    process.exit(exitCode);
});

// f(x)

async function openDB(settings) {
    var _db = await mongoClient.connect(settings.ConnectionString,  { useNewUrlParser: true });
    return _db;
}

async function switchDB(settings, database) {
    var tdb = await database.db(settings.Database);
    return tdb;
}

async function needsData(db, collectionName) {
    var flag = false;
    var result = await db.collection(collectionName).findOne({});
    if (!result) {
        var res = await db.createCollection(collectionName);
        flag = true;
    }
    return flag;
}

async function makeUserTestData(db) {
    var dataFile = path.join(__dirname, dataFolder, 'users.json');
    var data = await readFile(dataFile, 'utf8');
    var users = JSON.parse(data);
    
    users.forEach(function (item) {
      
        await db.collection(userCollection).insertOne(item);

    });

    return true;
}

async function makePostsTestData(db) {
    var dataFile = path.join(__dirname, dataFolder, 'posts.json');
    var data = await readFile(dataFile, 'utf8');
    var posts = JSON.parse(data);

    posts.forEach(function (item) {

        await db.collection(postCollection).insertOne(item);

        var imagePath = path.join(__dirname, dataFolder, item.image);

        var bucket = new mongo.GridFSBucket(db, {
            chunkSizeBytes: 1024,
            bucketName: 'images'
        });

        var upStream = bucket.openUploadStream(item.image);

        upStream.options.metadata = {
            'file': item.image,
            'post': item.postid,
            'id': upStream.id
        };

        upStream.on('finish', () => {
            console.log(`finish: ${ item.image }`);
        });

        upStream.on('error', (err) => {
            console.log(err);
        });

        await fs.createReadStream(imagePath).pipe(upStream);
    });

    return true;
}