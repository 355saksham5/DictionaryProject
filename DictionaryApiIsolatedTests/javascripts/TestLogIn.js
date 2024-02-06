const chai = require('chai');
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


    describe('Login', () => {
        it('on getting correct credenticials return 200 and with jwt', function (done) {
            hippie(dereferencedSwagger, hippieOptions)
                .base(`${baseUrl}`)
                .header("User-Agent", "hippie")
                .json()
                .post('/api/User/Login')
                .send({
                    email: 'sak@gmail.com',
                    password: 'Qwert1.'
                })
                .end(function (err, res, body) {
                    if (err) return done(err);
                    expect(res.statusCode).to.equal(200);
                    done();
                });

        });

        it('on getting wrong user name return 404 and message user not found', function (done) {
            hippie(dereferencedSwagger, hippieOptions)
                .base(`${baseUrl}`)
                .header("User-Agent", "hippie")
                .json()
                .post('/api/User/Login')
                .send({
                    email: 'not@gmail.com',
                    password: 'Qwert1'
                })
                .end(function (err, res, body) {
                    if (err) return done(err);
                    expect(res.statusCode).to.equal(404);
                    done();
                });

        });

        it('on getting wrong password return 401 and message wrong password', function (done) {
            hippie(dereferencedSwagger, hippieOptions)
                .base(`${baseUrl}`)
                .header("User-Agent", "hippie")
                .json()
                .post('/api/User/Login')
                .send({
                    email: 'sak@gmail.com',
                    password: 'Qwert1'
                })
                .end(function (err, res, body) {
                    if (err) return done(err);
                    expect(res.statusCode).to.equal(401);
                    done();
                });

        });
        it('on getting wrong password & wrong username return 404 and message user not found', function (done) {
            hippie(dereferencedSwagger, hippieOptions)
                .base(`${baseUrl}`)
                .header("User-Agent", "hippie")
                .json()
                .post('/api/User/Login')
                .send({
                    email: 'NoUser@gmail.com',
                    password: 'Qwert1'
                })
                .end(function (err, res, body) {
                    if (err) return done(err);
                    expect(res.statusCode).to.equal(404);
                    done();
                });

        });
    });
});