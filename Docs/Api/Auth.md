
# Code&Go API

- [Code&Go API](../../README.md)
  - [Authentication](#authentication)
    - [Register](#register)
    - [Login](#login)

## Authentication

> Routes use for register, authenticate and authorize users on the application

### Register

> This routes is used for register new users on the application

#### Register Request

```http
POST /auth/register
```

```json
{
    "firstName": "Lucas Augusto",
    "lastName": "Silva",
    "email": "lucas@email.com",
    "password": "lucas@1234"
}
```

#### Register Responses

```http
200 OK
```

```json
 "user": {
    "id": "e282261e-ccf4-4697-a72e-83a4dab28a80",
    "firstName": "Lucas Augusto",
    "lastName": "Silva",
    "email": "lucas@email.com",
    "profilePicture": null, // or string with url of the image
    "bio": null, // or string with the bio
    "streakCount": 0,
    "experiencePoints": 0,
    "visibility": 2,
    "friendshipRequests": [],
    "friendIds": [],
    "courseIds": []
  },
  "token": "eyJhb...UI8a0"
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
  "traceId": "00-2bca2224fc0b38b32171dcd4b82ca30f-aca6964f3c112d01-00",
  "errors": {
    "User.DuplicateEmail": [
      "User with this email already exists"
    ]
  }
}
```

---

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


### Login

> This routes is used for authenticate registered users on application

#### Login Request

```http
POST /auth/login
```

```json
{
    "email": "lucas@email.com",
    "password": "lucas@1234"
}
```

#### Login Responses

```http
200 OK
```

```json
 "user": {
    "id": "e282261e-ccf4-4697-a72e-83a4dab28a80",
    "firstName": "Lucas Augusto",
    "lastName": "Silva",
    "email": "lucas@email.com",
    "profilePicture": null, // or string with url of the image
    "bio": null, // or string with the bio
    "streakCount": 0,
    "experiencePoints": 0,
    "visibility": 2,
    "friendshipRequests": [],
    "friendIds": [],
    "courseIds": []
  },
  "token": "eyJhb...UI8a0"
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
  "traceId": "00-a1399216c7f66c3d57d7cb999d62bef9-0e58729f2779b61b-00",
  "errors": {
    "Authentication.InvalidCredentials": [
      "Invalid Credentials"
    ]
  }
}
```

---

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
