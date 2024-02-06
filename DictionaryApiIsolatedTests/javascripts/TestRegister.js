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

describe('Test for', function () {
    this.timeout(10000)
    before(function (done) {
        parser.dereference('./swagger.json', function (err, api) {
            if (err) return done(err)
            dereferencedSwagger = api
            done()
        })
    })


    describe('Register', () => {
        it('on getting correct details create resource and send 201', function (done) {
            hippie(dereferencedSwagger, hippieOptions)
                .base(`${baseUrl}`)
                .json()
                .post('/api/User/Register')
                .send({
                    email: 'new@gmail.com',
                    password: 'Qwert1.'
                })
                .end(function (err, res, body) {
                    if (err) return done(err);
                    expect(res.statusCode).to.equal(201);
                    expect(body.succeeded).to.equal(true);
                    done();
                });
        });
        it('on getting username that already exists return 400 and userconflict is true', function (done) {
            hippie(dereferencedSwagger, hippieOptions)
                .base(`${baseUrl}`)
                .json()
                .post('/api/User/Register')
                .send({
                    email: 'sak@gmail.com',
                    password: 'Qwert1.'
                })
                .end(function (err, res, body) {
                    if (err) return done(err);
                    expect(res.statusCode).to.equal(400);
                    expect(body.succeeded).to.equal(false);
                    expect(body.errors[0].code).to.equal('DuplicateUserName');
                    done();
                });
        });
        it('on getting password that do not matches the standard return 400 and userconflict is true', function (done) {
            hippie(dereferencedSwagger, hippieOptions)
                .base(`${baseUrl}`)
                .json()
                .post('/api/User/Register')
                .send({
                    email: 'password@gmail.com',
                    password: 'Qwert'
                })
                .end(function (err, res, body) {
                    if (err) return done(err);
                    expect(res.statusCode).to.equal(400);
                    expect(body.errors.Password[0]).to.equal('Password must be between 6 and 100 characters.');
                    done();
                });
        });
     
    });
});