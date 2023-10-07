# Code&Go Api

[![Dotnet Build and Test](https://github.com/LucasAuSilva/code-go-api/actions/workflows/dotnet-build-test.yml/badge.svg?branch=master)](https://github.com/LucasAuSilva/code-go-api/actions/workflows/dotnet-build-test.yml)

<!-- <div align="center">
  <img height="60%" width="60%" src="https://github.com/rmanguinho/clean-ts-api/blob/master/public/img/logo-course.png?raw=true" alt="Node and typescript"/>
</div> -->

<div align="center">
  <img height="42" width="42" src="https://simpleicons.now.sh/dotnet/512BD4" alt="Node and typescript"/>
  <img height="42" width="42" src="https://simpleicons.now.sh/nuget/004880" alt="git" />
  <img height="42" width="42" src="https://simpleicons.now.sh/git/F05032" alt="git" />
  <img height="42" width="42" src="https://simpleicons.now.sh/postgresql/4169E1" alt="mongodb logo" />
  <img height="42" width="42" src="https://simpleicons.now.sh/githubactions/2088FF" alt="github actions logo" />
</div>

<p align="center">Dotnet api for the application Code&Go</p>

Contents
=============

<!--ts-->
* [Concept](#concept)
* [Api docs](#link-to-api-docs)
* [Concepts used](#programming-concepts-used)
  * [Principles](#principles)
  * [Methodologies](#designs-and-methodologies)
* [How to run it](#how-to-run-it)
  * [Without Docker](#without-docker)
  * [With Docker](#with-docker)
  * [Database](#database)
* [Technologies](#technologies-in-the-project)
* [Versioning](#versioning)
* [History](#history)
* [Author](#author)
<!--te-->

## Concept

This API is for the application Code&GO, where is design to be a educational platform with gamification, stimulating the user to practice code skills and knowledge on daily bases  
> You can find the Front-End for the application on [this Github](https://github.com/guiillescas/code-and-go)

## Link to api docs

- [Auth](./Docs/Api/Auth.md)
- [User](./Docs/Api/User.md)
- [Course](./Docs/Api/Course.md)
- [Exercise](./Docs/Api/Exercise.md)
- [Question](./Docs/Api/Question.md)

## Programming concepts used

### Principles

* Single Responsibility Principle (SRP)
* Liskov Substitution Principle (LSP)
* Interface Segregation Principle (ISP)
* Dependency Inversion Principle (DIP)
* Separation of Concerns (SOC)
* Don't Repeat Yourself (DRY)
* You Aren't Gonna Need It (YAGNI)
* Keep It Simple, Silly (KISS)
* Command Query Responsibility Segregation (CQRS)

### Designs and Methodologies

* Clean Architecture
* DDD
* Conventional Commits
* GitFlow
* Dependency Diagrams
* Use Cases
* Continuous Integration
<!-- * Continuous Delivery -->

## How to run it

### Without docker

> Before run the project you need to confirm that you have this tools installed:
> [Git](https://git-scm.com), [Dotnet](https://dotnet.microsoft.com/download)

```bash
# Clone this repository
git clone https://github.com/LucasAuSilva/code-go-api
```
```bash
# Access the directory where you clone the repo
cd code-go-api
```
```bash
# Install the dependencies
dotnet restore
```
```bash
# Start the project
dotnet build
```

> **Before Run:** You need to setup an rabbit-mq broker for the application, you can follow [here](./Docs/configuration/rabbitmq.md) 
> **Before Run:** You need to execute the migrations on your database with [this](#database) steps.  
> And don't forget to change the your appsettings for you configuration.

```bash
# Start the with the CodeGo.Api project
dotnet run --project CodeGo.Api
```

### With Docker

> Before run the project you need to confirm that you have installed the following tools:
> [Git](https://git-scm.com), [Docker](https://www.docker.com/)

```text
# For docker you need to set an .env file with this variables, in the root of the project
# base connection string for the docker no need to change
ConnectionStrings__CodeGoDatabase="Host=localhost; Database=codegodev; Username=user; Password=teste@123"

# Broker connection settings, no need to change
BrokerSettings__Username=guest
BrokerSettings__Password=guest
BrokerSettings__Host=rabbit
BrokerSettings__Port=5672

# Queue for recover life settings, no need to change
LifeQueueSettings__QueueName=life.in
LifeQueueSettings__DelayInMinutes=2

# You don't need to setup this key, but the api to run exercise will not work ;)
Judge0Settings__ApiKey=YourApiKeyHere
Judge0Settings__Host=YourJudge0HostHere

JwtSettings__Secret=YourSecretHere
JwtSettings__Issuer=CodeGoApi
JwtSettings__Audience=CodeGoApp
```

```bash
# Clone this repository
git clone https://github.com/LucasAuSilva/code-go-api
```
```bash
# Access the directory where you clone the repo
cd code-go-api
```
```bash
# Executing the docker compose
docker compose up
```
```bash
# Or you can use this for detach mode
docker compose up -d
```
```bash
# For take the application down even when you CTRL+C the app you need to make this
docker compose down
```
> ***IMPORTANT:** If any change happens in the code, and you have already done the `docker-compose up`, you will need to run the follow command to apply the changes:
```bash
docker compose up --build
```

### Database
> **IMPORTANT:** This step is only applicable if you are running the app without docker, running with docker compose will run the migrations on startup
> You can use an database installed by driver or docker. Either way you have to make sure that is an **PostgreSQL** database.

> **PS:** This steps for the migrations only have to been applied for the first time or when clean or changed the database

> You need to execute the project migrations for the running. For this you need the following:

```bash
# Install the EntityFramework cli global
dotnet tool install --global dotnet-ef
```

#### Normal way (without docker)
```bash
# Executing the migration with your connection string
dotnet ef database update -p CodeGo.Infrastructure -s CodeGo.Api --connection "your connection string goes here"
```

## Technologies in the project

* [.NET](https://dotnet.microsoft.com/)
* [Git](https://git-scm.com/)
* [Docker](https://www.docker.com/)
* [PostgreSQL](https://www.postgresql.org/)
* [Github Actions](https://github.com/features/actions)

## Versioning

To keep better organization of releases we follow the [Semantic Versioning 2.0.0](http://semver.org/) guidelines.

## History

See [Releases](https://github.com/LucasAuSilva/code-go-api/releases) for detailed changelog.
