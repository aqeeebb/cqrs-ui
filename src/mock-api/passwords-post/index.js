const guid = require("guid")

module.exports = async function (context, req) {

    var keys = context.bindingData.keys;
    
    responseMessages = []
    
    for (let index = 0; index < keys; index++) {
        responseMessages.push({
            keyId:          guid.raw(),
            key:            Math.floor(Math.random() * Math.floor(Number.MAX_SAFE_INTEGER )),
            readHost:       "reader-host",
            writeHost:      "writer-host",
            readRegion:     "eastus2",
            writeRegion:    "ukwest",
            timeStamp:      Date.now()
        });
    }
    
    context.res = {
        body: responseMessages
    }
}