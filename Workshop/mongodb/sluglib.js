// Handy Slug Library

function generateSlug() {

    let slug_part = '';
    let chars = 'abcdefghijklmnopqrstuvwxyz0123456789';
  
    for ( let i = 0; i < 5; i++ ) {
        slug_part += chars.charAt(Math.floor(Math.random() * chars.length));
    }
  
    return slug_part;
}

function timeStamp(dt) {
    let timestamp = moment(dt).format('YYYY.MM.DD.hh:mm:ss');
    return timestamp;
}

function unifiedSlug(timestamp, slug_part) {
    return timestamp + ':' + slug_part;
}

var slugLib = {
    generateSlug : generateSlug,
    timeStamp: timeStamp,
    unifiedSlug: unifiedSlug
};

exports.slugLib;