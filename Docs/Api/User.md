
# Code&Go API

- [Code&Go API](../../README.md)
  - [User](#user)
    - [Edit Profile](#edit-profile)
    - [Register Course](#register-course)
    - [Response Friend Request](#response-friend-request)
    - [Send Friend Request](#send-friend-request)
    - [List Friends Request](#list-friends-requests)

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

### Send Friend Request

> This routes is used for send an friendship request to another user.

#### Send 'Friend Request' Request

```http
POST /user/{userId}/request/{otherUserId}
```

```json
{
    "message": "Lets be friends!!" // optional field
}
```

#### Send 'Friend Request' Response

```http
200 OK
```

```json

{
  "id": "00000000-0000-0000-0000-000000000000",
  "requesterId": "00000000-0000-0000-0000-000000000000",
  "message": "Lets be friends!!"
}
```

---

### Response Friend Request

> This routes is used for respond an friendship request

#### Response 'Friend Request' Request

```http
POST /user/{otherUserId}/request/{requestId}/response
```

```json
{
    // Can be: Accepted - 2, Refused - 3, Ignored - 4, Blocked - 5
    "Response": 2 // only numbers
}
```

#### 'Response Friend Request' Response

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

### Edit Profile

> This routes is used for user for edit his profile on the website

#### Edit Profile Request

```http
POST /user/{userId}/edit
```

```json
{
    "FirstName": "Lucas Augusto",
    "LastName": "Silva",
    "Email": "lucas@email.com",
    // Can be: Public - 1 and Private - 2
    "Visibility": 1, //only numbers
    "Bio": "I'm like very much to develop in C# and NodeJS"
}
```

#### Edit Profile Response

```http
200 OK
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "firstName": "Lucas Augusto",
  "lastName": "Silva",
  "email": "lucas@email.com",
  "profilePicture": null, // or https://imagem.com
  "bio": "I'm like very much to develop in C# and NodeJS", // or null
  "streakCount": 0,
  "experiencePoints": 0,
  "level": "00000000-0000-0000-0000-000000000000",
  "friendshipRequests": [],
  "friendIds": [],
  "courseIds": []
}
```

---

### List Friends Requests

> This routes is used for list all the friends requests from an user

#### 'List Friends Requests' Request

```http
GET /user/{userId}/requests
```

> You can use the parameter to filter the requests by their status like so.
> The default is status=1 == 'pending'
```http
GET /user/{userId}/requests?status=2
```

```json
{}
```

#### 'List Friends Requests' Response

```http
200 OK
```

```json
[
  {
    "id": "56b7113c-84ee-43c7-b916-261b021d3e4d",
    "requesterId": "317e3a97-ca6f-4af6-9313-07e125c4b4f7",
    "requesterEmail": "user@email.com",
    "requesterPhoto": null, // or image like this https://profile-picture-name.png
    "message": "Lets be friends!!"
  }
]
```

```json
[]
```

---

### General Responses

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
403 Forbidden
```

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.3",
  "title": "Forbidden",
  "status": 403,
  "detail": "User logged can access this content",
  "traceId": "00-00000000000000000000000000000000-0000000000000000-00",
  "errorsCodes": [
    "User.CantAccess"
  ]
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

---
