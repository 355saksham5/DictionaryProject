const WrongWord = require('./ResponseForWrongWord');
const RightWord = require('./ResponseForRightWord');
const Imposter = require('./MountebankHelper');

var imposter = {
    "protocol": "http",
    "port": 8090,
    "stubs": [
    ]
}
imposter.stubs.push(RightWord.Response("apple"));
imposter.stubs.push(WrongWord.Response("wrongwordabcd"));


Imposter.postImposter(imposter);
