# Quiz service

## Api documentation
The latest api documentation can be found [here](https://func-quizservice.azurewebsites.net/api/swagger/ui)

## How to communicate with the api
To communicate with the api, use a secret function key. This key is secret, do not version control!
Add the following header to each request:
```
x-functions-key: <secret>
```

### Example curl request to create a new quiz:
```
curl --location --request POST 'https://func-quizservice.azurewebsites.net/api/quizzes' \
--header 'x-functions-key: <the_secret_key>'
```

## Functionality, examples

### Creating a new empty quiz
Send a http request: POST https://func-quizservice.azurewebsites.net/api/quizzes
and a new quiz will be created with a guid identifier.

### Get an existing quiz
Send a http request: GET https://func-quizservice.azurewebsites.net/api/quizzes/{your_quiz_id}.

### Add a new question to the quiz
Currently only one type of question is supported a question with several alternative answers.

Send a http request: POST https://func-quizservice.azurewebsites.net/api/quizzes/{your_quiz_id}/questions with body
```
{   
    "Type": "Alternative",
    "AlternativeQuestion": {
        "Question": "How long is a rope really?",
        "Alternatives": [
            {
                "Alternative": "Very long",
                "IsCorrect": false
            },
            {
                "Alternative": "Not very long",
                "IsCorrect": true
            },
            {
                "Alternative": "Medium long",
                "IsCorrect": false
            }
        ]
    }
}
```
