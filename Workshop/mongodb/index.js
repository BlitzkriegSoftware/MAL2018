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

readFile(settingsFile, 'utf8')
    .then((data) => {
        console.log(data);
        settings = JSON.parse(data);
        console.log(settings.ConnectionString);
        console.log(settings.Database);
    })
    .then(() => {
        openDB()
            .then((db) => {
                collectionExistsWithData(db, userCollection)
                    .then((flag) => {
                        console.log(flag);
                        if (!flag) {
        
                        }
                    })
            })
            .then(() => {
                process.exit(exitCode);
            })
            .catch((err) => {
                throw err;
            })
    })

    .catch((err) => {
        throw err;
    })

function openDB() {
    return new Promise((resolve, reject) => {
        mongoClient.connect(settings.ConnectionString)
            .then((db) => {
                db = db.db(settings.Database);
                console.log('Current DB: ' + db.databaseName);
                resolve(db);
            })
            .catch((err) => {
                throw err;
            })
    })
}

function collectionExistsWithData(db, collectionName) {
    return new Promise((resolve, reject) => {
        db.collections(collectionName).find().toArray()
            .then((items) => {
                if (items) {
                   resolve((items.length <= 0));
                } else {
                    resolve(true);
                }
            })
            .catch((err) => {
                db.createCollection(collectionName, function (err, res) {
                    if (err) reject(err);
                    console.log(`Collection Created: ${ res }`)
                    resolve(true);
                });
            });
    })
}