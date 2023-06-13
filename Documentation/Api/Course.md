
# Code&Go API

- [Code&Go API](#code&go-api)
  - [Course](#course)
    - [Languages](#languages)
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
