// @ts-check
'use strict';

var exitCode = 0;

var settings = { };

const settingsFile = './settings.json';
const dataFolder = 'seeddata';

const userCollection = 'users';
const postCollection = 'posts';

var util = require('util');

var fs = require('fs');
var readFile = util.promisify(fs.readFile);

var path = require('path');

var mongo = require("mongodb");
var mongoClient = require("mongodb").MongoClient;

readFile(settingsFile, 'utf8')
    .then((data) => {
        console.log(data);
        settings = JSON.parse(data);
    })
    .then(settings) => {
        console.log(settings.ConnectionString);
        console.log(settings.Database);
    })
    .catch((err) => {
        throw err;
    })

process.exitCode = exitCode;
