const guid = require("guid")

module.exports = async function (context, req) {

    var keys = context.bindingData.keys;
    
    const key = () => {
        let rnd   = Math.floor(Math.random() * Math.floor(Number.MAX_SAFE_INTEGER)).toString()
        return Buffer.from(rnd).toString('base64')
    }

    responseMessages = []
    
    for (let index = 0; index < keys; index++) {
        responseMessages.push({
            keyId:          guid.raw(),
            key:            key(),
            readHost:       "reader-host",
            writeHost:      "writer-host",
            readRegion:     "eastus2",
            writeRegion:    "ukwest",
            timeStamp:      Date.now().toString()
        });
    }
    
    context.res = {
        body: responseMessages
    }
}