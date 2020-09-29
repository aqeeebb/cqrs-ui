module.exports = async function (context, req) {

    var keyId = context.bindingData.keyId;

    const key = () => {
        let rnd   = Math.floor(Math.random() * Math.floor(Number.MAX_SAFE_INTEGER)).toString()
        return Buffer.from(rnd).toString('base64')
    }

    responseMessage = {
        keyId:          keyId,
        key:            key(),
        readHost:       "reader-host",
        writeHost:      "writer-host",
        readRegion:     "eastus2",
        writeRegion:    "ukwest",
        timeStamp:      Date.now().toString()
    };
    
    context.res = {
        body: responseMessage
    }
}