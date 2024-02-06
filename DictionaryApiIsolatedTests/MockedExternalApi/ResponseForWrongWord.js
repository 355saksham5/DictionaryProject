function Response(word) {
    var response = {
        "predicates": [
            {
                "equals": {
                    "method": "GET",
                    "path": "/api/v2/entries/en/"+word
                }
            }
        ],
        "responses": [
            {
                "is": {
                    "statusCode": 404,
                    "headers": { "Content-Type": "application/json" },
                    "body": {
                        "title": "No Definitions Found",
                        "message": "Sorry pal, we couldn't find definitions for the word you were looking for.",
                        "resolution": "You can try the search again at later time or head to the web instead."
                    }
                }
            }
        ]
    }
    return response;
}
module.exports = { Response };