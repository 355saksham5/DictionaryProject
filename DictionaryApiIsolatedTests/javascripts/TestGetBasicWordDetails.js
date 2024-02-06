const chai = require('chai');
const jwt = require('../JwtCreateModule');
const RightWord = require('../MockedExternalApi/ResponseForRightWord');
const Stub = require('../MockedExternalApi/AddStub');
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


    describe('GetBasicDetails', () => {
        it('on getting correct authorization jwt and available word return 200 and basic details of word', function (done) {
            Stub.addStubs(RightWord.Response("ball"));
            hippie(dereferencedSwagger, hippieOptions)
                .base(`${baseUrl}`)
                .header("Authorization", "Bearer " + jwt.GetJwt())
                .json()
                .get('/api/Word/BasicDetails')
                .qs({queryWord : "ball"})
                .end(function (err, res, body) {
                    if (err) return done(err);
                    expect(res.statusCode).to.equal(200);
                    expect(body.word).to.equal("ball");
                    done();
                });
        });
        it('on getting correct authorization but wrong word jwt return 404', function (done) {
            hippie(dereferencedSwagger, hippieOptions)
                .base(`${baseUrl}`)
                .header("Authorization", "Bearer " + jwt.GetJwt())
                .json()
                .get('/api/Word/BasicDetails')
                .qs({ queryWord: "wrongwordabcd" })
                .end(function (err, res, body) {
                    if (err) return done(err);
                    expect(res.statusCode).to.equal(404);
                    expect(body.ErrorMessage).to.equal('Given Word not found');
                    done();
                });
        });
        it('on getting empty authorization jwt return 401 unauthorized', function (done) {
                hippie(dereferencedSwagger, hippieOptions)
                    .base(`${baseUrl}`)
                    .json()
                    .get('/api/Word/BasicDetails')
                    .qs({ queryWord: "apple" })
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
                        .get('/api/Word/BasicDetails')
                        .qs({ queryWord: "apple" })
                        .end(function (err, res, body) {
                            if (err) return done(err);
                            expect(res.statusCode).to.equal(401);
                            done();
                        });
                });
        });

        
    });
