

# Code&Go API

- [Code&Go API](../../README.md)
  - [Category](#course)
    - [List Categories](#list-categories)
    - [Create Category](#create-category)

## Category

> Routes related to the categories, like create new categories, find categories and list as well.
> **IMPORTANT** Only `admin` can access this kind of routes

### List Categories

> This routes is for list categories of the application

#### List Categories Request

> Do yu **`need`** to send an language value for the request 
```http
GET /categories?language=1
```

#### List Categories Response

```http
200 OK
```

```json
[
  {
    "id": "0e628f32-faff-49bc-83d4-d6bbb396f61f",
    "name": "Declaring Variables",
    "description": "This category is for user learn how to setting variables on the language",
    "language": 1
  }
]
```

### Create Category

> This route is for create an new category for th language on the application

#### Create Category Request

```http
POST /category
```
```json
{
    "name": "Declaring Variables",
    "description": "This category is for user learn how to setting variables on the language",
    "languageValue": 1
}
```

#### Create Category Response

```http
200 Ok
```
```json
{
  "id": "d58a5613-5b58-4954-8949-010532d8dc8c",
  "name": "Declaring Variables",
  "description": "This category is for user learn how to setting variables on the language",
  "language": 1
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
