async function postImposter(body) {
    const url = `http://localhost:2525/imposters`;

    await fetch(url, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
    })
    
}

module.exports = { postImposter };
