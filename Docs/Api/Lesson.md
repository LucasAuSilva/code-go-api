

# Code&Go API

- [Code&Go API](../../README.md)
  - [Lesson](#lesson)
    - [Start Lesson](#start-lesson)
    - [Resolve Question](#resolve-question)
    - [Resolve Exercise](#resolve-exercise)
    - [Finish Lesson](#finish-lesson)
    - [General Responses](#general-responses)

## Category

> Routes for starting, resolving, finishing and tracking lessons.

### Start Lesson

> This routes is for list categories of the application

#### Start Lesson Request

```http
POST /lesson/{courseId}/module/{moduleId}/start
```

#### Start Lesson Response

```http
200 OK
```

```json
{
  "lessonId": "bc342e6f-055c-4a9c-bc49-08e680613549",
  "questions": [
    {
      "id": "54ab17cf-2cc9-42fe-b59c-5cf04158e547",
      "title": "Variáveis Javascript",
      "description": "Qual a diferença entre \"let\" e \"const\" na declaração de variáveis?",
      "alternatives": [
        {
          "id": "1c2b0e74-3d84-4e27-bec0-6b040b87647d",
          "description": "A diferença é que \"let\" é apenas read-only quando assinada pela primeira vez, já \"const\" pode ser assinada varias vezes."
        },
        {
          "id": "548aae48-0c26-4cef-a9ca-1431b43c41b5",
          "description": "As keywords \"let\" e \"const\" não são usadas para declarar variáveis."
        },
        {
          "id": "587658e3-5ec5-4a0c-bba5-5e9999043d5d",
          "description": "Nenhuma diferença, ambos declaram uma variável em JavaScript."
        },
        {
          "id": "6268b9bf-4f5c-42dd-a4f7-d8aca2860f2c",
          "description": "A principal diferença é que \"let\" permite uma mudança em seu valor, enquanto \"const\" quando assinada a um valor pela primeira vez se torna **read-only**."
        }
      ]
    }
  ],
  "exercises": [
    {
      "id": "f15cc351-5af5-49d5-b01b-a52c0ae2278f",
      "title": "Declaração de MyVariable",
      "description": "Usando o editor abaixo declare a variável 'MyVariable', e com seu valor(em string) de: I'm an variable",
      "baseCode": "\nBaseCode\n\nconsole.log(myVariable);\n",
      "testCases": [
        {
          "id": "3a1553b4-9d2f-454c-aae1-74f6fe3d88cc",
          "title": "Test Case 1"
        }
      ]
    }
  ]
}
```

---

### Resolve Question

> This route is for resolving the questions from the lesson

#### Resolve Question Request

```http
PUT /lesson/{lessonId}/resolve/question
```
```json
{
    "questionId": "eb939990-0d6a-45e0-bb70-b4bd1f841326",
    "alternativeId": "6e0f987b-977c-4050-ab66-db2656bf21cd"
}
```

#### Resolve Question Response

```http
200 Ok
```
```json
{
  "message": "Resposta Correta",
  "isCorrect": true
}
```

```http
200 Ok
```
```json
{
  "message": "Resposta Incorreta",
  "isCorrect": false
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
  "traceId": "00-14ffdf65d10f78d30badd1a2af7a583f-75d4493bf5f797e7-00",
  "errors": {
    "Description": [
      "The Description field is required."
    ]
  }
}
```

---

### Resolve Exercise

> This route is used for resolving the exercise of the lesson

#### Resolve Exercise Request

```http
PUT /lesson/{lessonId}/resolve/exercise
```
```json
{
    "ExerciseId": "f15cc351-5af5-49d5-b01b-a52c0ae2278f",
    "TestCaseId": "3a1553b4-9d2f-454c-aae1-74f6fe3d88cc",
    "SolutionCode": "var myVariable = \"I'm an variable\""
}
```

#### Resolve Exercise Response

```http
200 Ok
```
```json
{
  "message": "Resposta Correta",
  "isCorrect": true
}
```

```http
200 Ok
```
```json
{
  "message": "Resposta Incorreta",
  "isCorrect": false
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
  "traceId": "00-14ffdf65d10f78d30badd1a2af7a583f-75d4493bf5f797e7-00",
  "errors": {
    "Description": [
      "The Description field is required."
    ]
  }
}
```

---

### Finish Lesson

> This route is finish the lesson after make all the exercises and questions

#### Finish Lesson Request

```http
PUT /lesson/{lessonId}/finish
```
```json
{}
```

#### Finish Lesson Response

```http
200 Ok
```
```json
{
  "message": "Que pena você falhou na lição, continue tentando",
  "isFailed": true
}
```

```json
{
  "message": "Parabéns você finalizou essa lição com sucesso",
  "isFailed": false
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
  "traceId": "00-14ffdf65d10f78d30badd1a2af7a583f-75d4493bf5f797e7-00",
  "errors": {
    "Description": [
      "The Description field is required."
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
