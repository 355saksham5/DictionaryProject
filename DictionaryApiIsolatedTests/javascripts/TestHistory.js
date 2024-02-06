const chai = require('chai');
const expect = chai.expect
const SwaggerParser = require('swagger-parser')
var parser = new SwaggerParser()
const jwt = require('../JwtCreateModule');
const hippie = require('hippie-swagger');
let baseUrl = 'http://localhost:7123'
var dereferencedSwagger
let hippieOptions = {
    validateResponseSchema: false,
    validateParameterSchema: false,
    errorOnExtraParameters: false,
    errorOnExtraHeaderParameters: false
};

describe('Test for', function () {
    this.timeout(10000)
    before(function (done) {
        parser.dereference('./swagger.json', function (err, api) {
            if (err) return done(err)
            dereferencedSwagger = api
            done()
        })
    })


    describe('GetHistory', () => {
        it('on getting authorized jwt return 200 and with user history', function (done) {
            hippie(dereferencedSwagger, hippieOptions)
                .base(`${baseUrl}`)
            .header("Authorization", "Bearer " + jwt.GetJwt())
                .header("User-Agent", "hippie")
                .json()
                .get('/api/History')
                .end(function (err, res, body) {
                    if (err) return done(err);
                    expect(res.statusCode).to.equal(200);
                    expect(body[0].word).to.equal('apple');
                    done();
                });

        });

        it('on getting non authorized jwt return 401 and without user history', function (done) {
            hippie(dereferencedSwagger, hippieOptions)
                .base(`${baseUrl}`)
                .header("User-Agent", "hippie")
                .json()
                .get('/api/History')
                .end(function (err, res, body) {
                    if (err) return done(err);
                    expect(res.statusCode).to.equal(401);
                    done();
                });

        });
    });
});