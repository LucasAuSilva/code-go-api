
# Code&Go API

- [Code&Go API](../../README.md)
  - [Course](#course)
    - [Find Course](#find-course)
    - [List Courses](#list-courses)
    - [Languages](#languages)*
    - [Create Course](#create-course)*
    - [Create Section](#create-section)*
    - [Create Module](#create-module)*
    - [General Responses](#general-responses)

`*` Requested that are only allowed for system **`admins`**

## Course

> Routes related to the courses, like create new course, find course, modules or sections. As well 

### Find Course

> Route used for find an existing course on the application and see the details

#### Find Course Request

```http
GET /course/{courseId}
```

#### Find Course Response

```http
200 OK
```

```json
{
  "id": "653b5f0f-7460-4b36-95f6-f7f71811d050",
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
      "id": "1c88cb7c-b3f3-4a20-bbd2-dc43bfaf7f9b",
      "name": "Seção 1",
      "description": "Básico do Javascript",
      "modules": [
        {
          "id": "b9def288-a3c6-4011-8521-1cd4dd1786d6",
          "name": "Declaração de Variáveis - Mudanças pelo tempo",
          "totalLessons": 3,
          "moduleType": "Skill"
        }
      ]
    }
  ]
}
```

---

```http
404 NotFound
```

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Not Found",
  "status": 404,
  "detail": "Course with this id doesn't exists",
  "traceId": "00-431e02c08ee2128cc5f5e16575f3398a-0f6d8ac12a5ad07b-00",
  "errorsCodes": [
    "Course.NotFound"
  ]
}
```

---

### Languages

> This routes is for check the supported languages of the application
> **IMPORTANT:** Only `admin` can access this route

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

### List Courses

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
> **IMPORTANT:** Only `admin` can access this route

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
> **IMPORTANT:** Only `admin` can access this route

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
> **IMPORTANT:** Only `admin` can access this route

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
