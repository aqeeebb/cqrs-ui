module.exports = async function (context, req) {

    var keyId = context.bindingData.keyId;
    
    responseMessage = {
        keyId:          keyId,
        key:            Math.floor(Math.random() * Math.floor(Number.MAX_SAFE_INTEGER )),
        readHost:       "reader-host",
        writeHost:      "writer-host",
        readRegion:     "eastus2",
        writeRegion:    "ukwest",
        timeStamp:      Date.now()
    };
    
    context.res = {
        body: responseMessage
    }
}