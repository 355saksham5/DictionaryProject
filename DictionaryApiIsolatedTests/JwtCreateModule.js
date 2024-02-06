const jwt = require('jsonwebtoken')
function GetJwt() {

    var data = {
        "jti": "53cc7d5b-8314-47ea-8891-144f8156e64f",
        "UserId": "23daab02-a1ec-469b-af60-ee27174a4c79",
        "nbf": 1707111337,
        "exp": Math.round(Date.now() / 1000) + 84000,
        "iat": 1707111337,
        "iss": "http://dictionaryapi",
        "aud": "http://dictionaryapi"
    };
    var secret = "93yT871w-GeTI4Es5-TR4s-Oo00ovFx-sk";

    return jwt.sign(data, secret);
}
module.exports = { GetJwt };