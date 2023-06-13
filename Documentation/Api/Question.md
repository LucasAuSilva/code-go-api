
# Code&Go API

- [Code&Go Api](#codego-questions-api)
  - [Questions](#questions)
    - [Create](#create)
    - [Resolve Question](#resolve-question)
  - [General Responses](#general-responses)

## Questions

> Routes for create, list and resolve questions from modules

### Create

> This routes is used for create an new question for course

#### Create Request

```http
POST /question
```

```json
{
    "courseId": "00000000-0000-0000-0000-000000000000",
    "title": "Variáveis Javascript",
    "description": "Qual é a **Keyword** para declarar variáveis em javascript",
    "categoryId": "00000000-0000-0000-0000-000000000000",
    "difficultyValue": 1,
    "alternatives": [
        {
            "description": "myVariable = \"I'm an variable\"",
            "isCorrect": false
        },
        {
            "description": "var myVariable = \"I'm an variable\"",
            "isCorrect": true
        },
        {
            "description": "var myVariable: string = \"I'm an variable\"",
            "isCorrect": false
        },
        {
            "description": "string myVariable = \"I'm an variable\"",
            "isCorrect": false
        }
    ]
}
```

#### Create Responses

```http
200 OK
```

```json
{
  "questionId": "00000000-0000-0000-0000-000000000000",
  "title": "Variáveis Javascript",
  "description": "Qual é a **Keyword** para declarar variáveis em javascript",
  "categoryId": "00000000-0000-0000-0000-000000000000",
  "difficultyValue": 1,
  "courseId": "00000000-0000-0000-0000-000000000000",
  "alternatives": [
    {
      "alternativeId": "00000000-0000-0000-0000-000000000000",
      "description": "myVariable = \"I'm an variable\"",
      "isCorrect": false
    },
    {
      "alternativeId": "00000000-0000-0000-0000-000000000000",
      "description": "var myVariable = \"I'm an variable\"",
      "isCorrect": true
    },
    {
      "alternativeId": "00000000-0000-0000-0000-000000000000",
      "description": "var myVariable: string = \"I'm an variable\"",
      "isCorrect": false
    },
    {
      "alternativeId": "00000000-0000-0000-0000-000000000000",
      "description": "string myVariable = \"I'm an variable\"",
      "isCorrect": false
    }
  ]
}
```

---

```http
400 Bad Request
```

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "traceId": "00-1f2ca1a3a9b06568c2e9d8c65146bc79-e44202a0d3f7ee7c-00",
  "errors": {
    "CategoryId": [
      "The CategoryId field is required."
    ]
  }
}
```
<!-- TODO: It will have response for not authorized if its not an admin  -->
---

### Resolve Question

> This routes is used for resolve the question with an alternative

#### Resolve Question Request

```http
POST /question/{questionId}/resolve/{alternativeId}
```

```json
{}
```

#### Resolve Question Responses

```http
200 OK
```

```json
{
  "message": "Resposta Correta",
  "firstName": true
}
```

---

```http
200 OK
```

```json
{
  "message": "Resposta Incorreta",
  "firstName": false
}
```

---

<!-- TODO: make application return json for not found -->
```http
404 Not Found
```

```json
{}
```

### General Responses

> Any route in this document can return one of the following answers as well: 

```http
500 Internal Server Error
```

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.6.1",
  "title": "An error occurred while processing your request.",
  "status": 500,
  "traceId": "00-99b3a04a641db2a5e0e4235b16f3ce1b-e02ba7fb23e2ed88-00"
}
```

