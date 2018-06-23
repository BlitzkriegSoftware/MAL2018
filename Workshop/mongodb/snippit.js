
/*

var setupMongo = function (settings) {

    mongoClient.connect(settings.ConnectionString)
        .then(function (db) {
            db = db.db(settings.Database);
            console.log('Current DB: ' + db.databaseName);
            
        })
       
}

var anyRecords = function (db, collectionName) {
    db.listCollections()
        .then(function (list) {
            list.toArray(function (err, collections) {
                if (collections.includes(collectionName)) {
                    db.collection(collectionName).find().toArray(function (err, items) {
                        if (err) reject(err);
                        if (items) {
                            if (items.length <= 0) {
                                resolve(true);
                            }
                        }
                    });
                } else {
                    db.createCollection(collectionName, function (err, res) {
                        if (err) reject(err);
                        resolve(true);
                    });
                }
            });
        })
        .catch(function (err) {
            reject( err);
        });
        resolve(false);
};

var makeUserTestData = function (db) {
    var dataFile = path.join(__dirname, dataFolder, 'users.json');

    fs.readFile(dataFile, 'utf8')
        .then(function (data) {
            var users = JSON.parse(data);
            users.forEach(function (item, index) {
                db.collection(userCollection).insertOne(item, function (err, res) {
                    if (err) reject(err);
                    console.log(res);
                });
            });
        })
        .catch(function (err) {
            reject(err);
        });
}

var makePostsTestData = function (db) {

    var dataFile = path.join(__dirname, dataFolder, 'posts.json');
    
    var bucket = new mongo.GridFSBucket(db, {
        chunkSizeBytes: 1024,
        bucketName: 'images'
    });

    fs.readFile(dataFile, 'utf8')
        .then(function (data) {
            var posts = JSON.parse(data);
            posts.forEach(function (item, index) {
                makePost(db, item, bucket);
            });
        })
        .catch(function (err) {
           reject(err);
        });
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

*/