
# Code&Go API

- [Code&Go API](../../README.md)
  - [User](#user)
    - [User Profile](#user-profile)
    - [Edit Profile](#edit-profile)
    - [Register Course](#register-course)
    - [Response Friend Request](#response-friend-request)
    - [Send Friend Request](#send-friend-request)
    - [List Friends Request](#list-friends-requests)
    - [List Users by Name](#list-users-by-name)
    - [List Users by Email](#list-users-by-email)*
    - [Update User Role](#update-user-role)*

`*` Requested that are only allowed for system **`admins`**

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

#### Response Friend Request

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
PUT /user/{userId}/edit
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

### List Users by Email

> This routes is used for list all the users by email
> **IMPORTANT:** Only `admin` can access this route

#### List Users by Email Request

> `page`: The number of the page of the results 
> `pagesize`: The number of records that will return on each page
```http
GET /user/admin/list?page=1&pagesize=10
```

> You can use the parameter `email` to filter the records like so
> **Example**: All the users with email like luc will appear like: lucas@email.com
```http
GET /user/admin/list?page=1&pagesize=10&email=luc
```

```json
{}
```

#### List Users by Email Response

```http
200 OK
```

```json
{
  "page": 1,
  "pageSize": 10,
  "totalPages": 1,
  "totalRecords": 2,
  "data": [
    {
      "id": "9ae46b0c-0a7b-45c1-aa9c-3ba5cc313a57",
      "firstName": "Guilherme Arthur Leimann",
      "lastName": "Illescas",
      "email": "guilherme@email.com",
      "profilePicture": null,
      "role": "User"
    },
    {
      "id": "e282261e-ccf4-4697-a72e-83a4dab28a80",
      "firstName": "Lucas Augusto",
      "lastName": "Silva",
      "email": "lucas@email.com",
      "profilePicture": null,
      "role": "Admin"
    }
  ],
  "hasNextPage": false,
  "hasPreviousPage": false
}
```

> filtered with luc
```json
{
  "page": 1,
  "pageSize": 10,
  "totalPages": 1,
  "totalRecords": 1,
  "data": [
    {
      "id": "e282261e-ccf4-4697-a72e-83a4dab28a80",
      "firstName": "Lucas Augusto",
      "lastName": "Silva",
      "email": "lucas@email.com",
      "profilePicture": null,
      "role": "Admin"
    }
  ],
  "hasNextPage": false,
  "hasPreviousPage": false
}
```

> Empty list
```json
[]
```

---

### List Users by Name

> This routes is used for list all the users by name

#### List Users by Name Request

> `page`: The number of the page of the results 
> `pagesize`: The number of records that will return on each page
```http
GET /user/list?page=1&pagesize=10
```

> You can use the parameter `name` to filter the records like so
> **Example**: All the users with email like luc will appear like: lucas@email.com
```http
GET /user/{userId}/requests?page=1&pagesize=10&name=luc
```

```json
{}
```

#### List Users by Name Response

```http
200 OK
```

```json
{
  "page": 1,
  "pageSize": 10,
  "totalPages": 1,
  "totalRecords": 2,
  "data": [
    {
      "email": "guilherme@email.com",
      "profilePicture": null
    },
    {
      "email": "lucas@email.com",
      "profilePicture": null
    }
  ],
  "hasNextPage": false,
  "hasPreviousPage": false
}
```

> filtered with luc
```json
{
  "page": 1,
  "pageSize": 10,
  "totalPages": 1,
  "totalRecords": 1,
  "data": [
    {
      "email": "lucas@email.com",
      "profilePicture": null
    }
  ],
  "hasNextPage": false,
  "hasPreviousPage": false
}
```

> Empty list
```json
[]
```

---

### User Profile

> Route used for get the user profile details, can be used for see others users profile if they are friends or visibility is public

#### User Profile Request

```http
GET /user/{userId}
```

#### User Profile Response

```http
200 OK
```

```json
{
  "id": "aeb702af-6767-477f-aa63-93f7b44fac7a",
  "firstName": "Lucas Augusto",
  "lastName": "Silva",
  "email": "lucas@email.com",
  "profilePicture": null,
  "bio": null,
  "streakCount": 3,
  "lifeCount": 5,
  "lifeTotal": 5,
  "experiencePoints": 300,
  "visibility": 2, // 1 = Public | 2 = Private
  "friendshipRequests": [], // or ["00000000-0000-0000-0000-000000000000", ...]
  "friendIds": [], // or ["00000000-0000-0000-0000-000000000000", ...]
  "courseIds": [
    "653b5f0f-7460-4b36-95f6-f7f71811d050"
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
  "detail": "User with this id doesn't exists",
  "traceId": "00-fc1b9f61cb9b07d29f2109fb9210dda9-9fd9a0b5b932fa9c-00",
  "errorsCodes": [
    "User.NotFound"
  ]
}
```

---

### Update User Role

> Route used for update the user role to admin or back to normal user.
> **IMPORTANT**: Only **`admins`** can use this request.

#### Update User Role Request

```http
PUT /user/admin/{userId}/transform/{role}
```

```json
{}
```

#### Update User Role Response

```http
200 OK
```

```json
{
  "id": "174bd611-c7c9-4387-b94b-51e748d22475",
  "firstName": "Guilherme Arthur Leimann",
  "lastName": "Illescas",
  "email": "guilherme@email.com",
  "profilePicture": null,
  "bio": null,
  "streakCount": 0,
  "lifeCount": 5,
  "lifeTotal": 5,
  "experiencePoints": 0,
  "level": null,
  "visibility": 2,
  "friendshipRequests": [
    {
      "id": "9c2b20ed-5068-4196-8796-d7e94d8329b2",
      "requesterId": "aeb702af-6767-477f-aa63-93f7b44fac7a",
      "requesterEmail": "lucas@email.com",
      "requesterPhoto": null,
      "message": "Lets be friends!!"
    }
  ],
  "friendIds": [],
  "courseIds": []
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
  "traceId": "00-2a50beca3cccb7c0a895218dcb120169-29e7a5b8041bb516-00",
  "errors": {
    "Role": [
      "'Role' must be less than '3'."
    ]
  }
}
```

---

```http
404 Not Found
```

```json

{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Not Found",
  "status": 404,
  "detail": "User with this id doesn't exists",
  "traceId": "00-10248f5a2c3b26ac31c4f0bb1b23fa9e-1fdda2263647a697-00",
  "errorsCodes": [
    "User.NotFound"
  ]
}
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
