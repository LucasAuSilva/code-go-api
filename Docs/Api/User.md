
# Code&Go API

- [Code&Go API](../../README.md)
  - [User](#user)
    - [Register Course](#register-course)

## User

> Route for manage users and make actions like register course

### Register Course

> This routes is used for register an course for user

#### Register Course Request

```http
POST /user/{userId}/register/{courseId}
```

```json
```

#### Register Responses

```http
200 OK
```

```json

{
  "id": "00000000-0000-0000-0000-000000000000",
  "firstName": "Lucas Augusto",
  "lastName": "Silva",
  "email": "lucas@email.com",
  "profilePicture": null,
  "bio": null,
  "streakCount": 0,
  "experiencePoints": 0,
  "level": "00000000-0000-0000-0000-000000000000",
  "courseIds": [
    "00000000-0000-0000-0000-000000000000"
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
  "traceId": "00-2bca2224fc0b38b32171dcd4b82ca30f-aca6964f3c112d01-00",
  "errors": {
    "User.NotFound": [
      "User with this id doesn't exists"
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
