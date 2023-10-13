
# Code&Go API

- [Code&Go API](../../README.md)
  - [Questions](#questions)
    - [Create](#create)*
    - [Delete](#delete)*
    - [Edit](#edit)*
  - [General Responses](#general-responses)

`*` Requested that are only allowed for system **`admins`**

## Questions

> Routes for create, list and resolve questions from modules

### Create

> This routes is used for create an new question for course
> **IMPORTANT:** Only `admin` can access this route

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

### Edit

> Route used for edit the question and its alternatives.
> **IMPORTANT** only **`admins`** can use this route.

#### Edit Request

```http
PUT /question/{questionId}
```

```json
{
    "title": "Variáveis Javascript",
    "description": "Como declarar **variáveis** em javascript ?",
    "categoryId": "{{categoryId}}",
    "difficultyValue": 1,
    "alternatives": [
        {
            "id": "2c4a6f77-b1df-4e0a-93c9-1934f5fc8082",
            "description": "`myVariable = \"I'm an variable\"`",
            "isCorrect": false
        },
        {
            "id": "446d057e-fb5a-4adc-9576-7aceb2ebf94f",
            "description": "`const myVariable = \"I'm an variable\"`",
            "isCorrect": true
        },
        {
            "id": "53c39212-5833-40bc-b3ac-4671b0a0c09d",
            "description": "`const myVariable: string = \"I'm an variable\"`",
            "isCorrect": false
        },
        {
            "id": "6e0f987b-977c-4050-ab66-db2656bf21cd",
            "description": "`string myVariable = \"I'm an variable\"`",
            "isCorrect": false
        }
    ]
}
```

#### Edit Response

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

### Delete

> Route used for delete an question
> **IMPORTANT** only **`admins`** can use this route.

#### Delete Request

```http
DELETE /question/{questionId}
```

#### Delete Response

```http
204 No Content
```

```http
404 Not Found
```

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "traceId": "00-aba16406c28aecc661c737b03425b2aa-40fc36865ff6f26f-00",
  "errors": {
    "Question.NotFound": [
      "Question with this id doesn't exists"
    ]
  }
}
```

---

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

