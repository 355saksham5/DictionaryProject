async function addStubs(stub) {
    const url = `http://localhost:2525/imposters/8090/stubs`;
    var body = JSON.stringify(stub)
    await fetch(url, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            "stub": stub

        })
    })
}

module.exports = { addStubs };