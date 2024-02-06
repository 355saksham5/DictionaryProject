const chai = require('chai');
const jwt = require('../JwtCreateModule');
const expect = chai.expect
const SwaggerParser = require('swagger-parser')
var parser = new SwaggerParser()
const hippie = require('hippie-swagger');
let baseUrl = 'http://localhost:7123'
var dereferencedSwagger
let hippieOptions = {
    validateResponseSchema: false,
    validateParameterSchema: false,
    errorOnExtraParameters: false,
    errorOnExtraHeaderParameters: false
};

describe('Test For', function () {
    this.timeout(10000)
    before(function (done) {
        parser.dereference('./swagger.json', function (err, api) {
            if (err) return done(err)
            dereferencedSwagger = api
            done()
        })
    })


    describe('GetDefinition', () => {
        it('on getting correct authorization jwt and available word return 200 and definition of word', function (done) {
            hippie(dereferencedSwagger, hippieOptions)
                .base(`${baseUrl}`)
                .header("Authorization", "Bearer " + jwt.GetJwt())
                .json()
                .get('/api/Word/Definition')
                .qs({ wordId: "61c557cb-f633-4f54-bc49-296da6a25761" })
                .end(function (err, res, body) {
                    if (err) return done(err);
                    expect(res.statusCode).to.equal(200);
                    expect(body.definitionText).to.equal("definition of apple");
                    done();
                });
        });
        it('on getting correct authorization but wrong word Id jwt return 400', function (done) {
            hippie(dereferencedSwagger, hippieOptions)
                .base(`${baseUrl}`)
                .header("Authorization", "Bearer " + jwt.GetJwt())
                .json()
                .get('/api/Word/Definition')
                .qs({ wordId: "62c557cb-f633-4f54-bc49-296da6a25762" })
                .end(function (err, res, body) {
                    if (err) return done(err);
                    expect(res.statusCode).to.equal(400);
                    expect(body.ErrorMessage).to.equal('InValid Word Id');
                    done();
                });
            
        });
        it('on getting empty authorization jwt return 401 unauthorized', function (done) {
            hippie(dereferencedSwagger, hippieOptions)
                .base(`${baseUrl}`)
                .json()
                .get('/api/Word/Definition')
                .qs({ wordId: "61c557cb-f633-4f54-bc49-296da6a25761" })
                .end(function (err, res, body) {
                    if (err) return done(err);
                    expect(res.statusCode).to.equal(401);
                    done();
                });
        });
            it('on getting wrong authorization jwt return 401 unauthorized', function (done) {
                hippie(dereferencedSwagger, hippieOptions)
                    .base(`${baseUrl}`)
                    .json()
                    .header("Authorization", "Bearer " + "wrong jwt")
                    .get('/api/Word/Definition')
                    .qs({ wordId: "61c557cb-f633-4f54-bc49-296da6a25761" })
                    .end(function (err, res, body) {
                        if (err) return done(err);
                        expect(res.statusCode).to.equal(401);
                        done();
                    });
            });
        });
    });