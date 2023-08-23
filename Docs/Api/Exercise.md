
# Code&Go API

- [Code&Go API](../../README.md)
  - [Exercises](#exercises)
    - [Create](#create)
    - [Resolve Exercises](#resolve-exercise)
  - [General Responses](#general-responses)

## Exercises

> Routes for create, list and resolve exercises

### Create

> This routes is used for create an new exercise for course
> **IMPORTANT:** Only `admin` can access this route

#### Create Request

```http
POST /exercise
```

```json
{
    "courseId": "00000000-0000-0000-0000-000000000000",
    "title": "Declaração de MyVariable",
    "description": "Usando o editor abaixo declare a variável 'MyVariable', e com seu valor(em string) de: I'm an variable",
    "categoryId": "00000000-0000-0000-0000-000000000000",
    "difficultyValue": 1,
    "typeValue": 1,
    "testCases": [
        {
            "title": "Test Case 1",
            "result": "I'm an variable\n"
        }
    ],
    "baseCode": "\nBaseCode\n\nconsole.log(myVariable);\n"
}
```

#### Create Responses

```http
200 OK
```

```json
{
  "exerciseId": "00000000-0000-0000-0000-000000000000",
  "title": "Declaração de MyVariable",
  "description": "Usando o editor abaixo declare a variável 'MyVariable', e com seu valor(em string) de: I'm an variable",
  "difficultyValue": 1,
  "type": "Complete",
  "baseCode": "\nBaseCode\n\nconsole.log(myVariable);\n",
  "courseId": "00000000-0000-0000-0000-000000000000",
  "categoryId": "00000000-0000-0000-0000-000000000000",
  "testCases": [
    {
      "testCaseId": "00000000-0000-0000-0000-000000000000",
      "title": "Test Case 1",
      "result": "I'm an variable\n"
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

### Resolve Exercise

> This routes is used for send an solution for exercise

#### Resolve Exercise Request

```http
POST /exercise/{exerciseId}/resolve/{testCaseId}
```

```json
{
    "solutionCode": "var myVariable = \"I'm an variable\""
}
```

#### Resolve Exercise Responses

```http
200 OK
```

```json
{
  "message": "Sucesso no Teste",
  "firstName": true
}
```

---

```http
200 OK
```

```json
{
  "message": "Falha no Teste",
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

