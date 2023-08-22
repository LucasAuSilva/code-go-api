
# Code&Go API

- [Code&Go API](../../README.md)
  - [Course](#course)
    - [List Courses](#list-courses)
    - [Languages](#languages)
    - [Create Course](#create-course)
    - [Create Section](#create-section)
    - [Create Module](#create-module)
    - [Start Module](#start-module)
    - [General Responses](#general-responses)

## Course

> Routes related to the courses, like create new course, find course, modules or sections. As well 
> **IMPORTANT:** Only `admin` can access this type of routes

### Languages

> This routes is for check the supported languages of the application

#### Languages Request

```http
GET /course/languages
```

#### Languages Response

```http
200 OK
```

```json
[
  {
    "name": "Csharp",
    "value": 2
  },
  {
    "name": "Javascript",
    "value": 1
  },
  {
    "name": "Python",
    "value": 3
  }
]
```

## List Courses

> This routes is for list courses of the application

#### List Course Request

```http
GET /course
```

#### List Course Response

```http
200 OK
```

```json
[
    {
        "id": "00000000-0000-0000-0000-000000000000",
        "name": "Javascript",
        "authorName": "code&go",
        "description": "The Code&Go base Javascript Course",
        "courseIcon": null,
        "language": {
            "name": "Javascript",
            "value": 1
        },
        "sections": []
    }
]
```

### Create Course

> This route is for create an new course on the application

#### Create Course Request

```http
POST /course
```
```json
{
    "name": "Javascript",
    "description": "The Code&Go base Javascript Course",
    "languageValue": 1
}
```

#### Create Course Response

```http
200 Ok
```
```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "name": "Javascript",
  "authorName": "code&go",
  "description": "The Code&Go base Javascript Course",
  "courseIcon": null,
  "language": {
    "name": "Javascript",
    "value": 1
  },
  "sections": []
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

### Create Section

> This route is used for create an section for specific course

#### Create Section Request

```http
POST course/{courseId}/section
```
```json
{
    "name": "Seção 1",
    "description": "Básico do Javascript"
}
```

#### Create Section Response

```http
200 OK
```
```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "name": "Javascript",
  "authorName": "code&go",
  "description": "The Code&Go base Javascript Course",
  "courseIcon": null,
  "language": {
    "name": "Javascript",
    "value": 1
  },
  "sections": [
    {
      "id": "00000000-0000-0000-0000-000000000000",
      "name": "Seção 1",
      "description": "Básico do Javascript",
      "modules": []
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
  "traceId": "00-14ffdf65d10f78d30badd1a2af7a583f-75d4493bf5f797e7-00",
  "errors": {
    "Name": [
      "The Name field is required."
    ]
  }
}
```

### Create Module

> This route is for create an module on a specific course and section

#### Create Module Request

```http
POST course/{courseId}/module
```
```json
{
    "sectionId": "00000000-0000-0000-0000-000000000000",
    "name": "Declaração de Variáveis",
    "totalLessons": 4,
    "moduleTypeValue": 1,
    "difficulty": 1
}
```

#### Create Module Response

```http
200 OK
```
```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "name": "Javascript",
  "authorName": "code&go",
  "description": "The Code&Go base Javascript Course",
  "courseIcon": null,
  "language": {
    "name": "Javascript",
    "value": 1
  },
  "sections": [
    {
      "id": "00000000-0000-0000-0000-000000000000",
      "name": "Seção 1",
      "description": "Básico do Javascript",
      "modules": [
        {
          "id": "00000000-0000-0000-0000-000000000000",
          "name": "Declaração de Variáveis",
          "totalLessons": 4,
          "moduleType": "Skill"
        }
      ]
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
  "traceId": "00-14ffdf65d10f78d30badd1a2af7a583f-75d4493bf5f797e7-00",
  "errors": {
    "Name": [
      "The Name field is required."
    ]
  }
}
```

### Start Module

> This route is used when the user start an module for practice

> **BREAKING:** Probably this route going to change in future updates

#### Start Module Request

```http
GET course/{courseId}/module/{moduleId}/start
```
```json
{}
```

#### Start Module Response

```http
200 Ok
```
```json
{
  "questions": [
    {
      "id": "43696e04-0aef-4699-a4bb-ca47a8b01592",
      "title": "Variáveis Javascript",
      "description": "Qual é a **Keyword** para declarar variáveis em javascript",
      "alternatives": [
        {
          "id": "6f32bd92-7372-4345-991c-c02f591e4521",
          "description": "var myVariable: string = \"I'm an variable\""
        },
        {
          "id": "b6c7cdf3-4880-4ae8-ae22-bef9fb903943",
          "description": "var myVariable = \"I'm an variable\""
        },
        {
          "id": "bf2e429b-b1c8-421f-9a93-21797381a48a",
          "description": "string myVariable = \"I'm an variable\""
        },
        {
          "id": "d9c6fc78-b86a-467d-82e2-34c423c2f7ef",
          "description": "myVariable = \"I'm an variable\""
        }
      ]
    }
  ],
  "exercises": [
    {
      "id": "666a0b1c-6534-4881-8f1f-9d2d8d45d231",
      "title": "Declaração de MyVariable",
      "description": "Usando o editor abaixo declare a variável 'MyVariable', e com seu valor(em string) de: I'm an variable",
      "baseCode": "\nBaseCode\n\nconsole.log(myVariable);\n",
      "testCases": [
        {
          "id": "ed46197e-851c-4fd9-a26c-d9568eb17036",
          "title": "Test Case 1"
        }
      ]
    }
  ]
}
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
