
## Setup RabbitMQ Broker

For setup an RabbitMQ Broker you can use two options as any application, install locally and run or with docker, on this instructions we will only use the docker option because of the practicality of it, you can setup yourself to install locally, but is **very recommend** to use on docker

### Setup with docker

Because is already made `some configuration` for the docker-compose is easy as running an command to get the `RabbitMQ` running on your docker.  
>**IMPORTANT** run the follow commands on the root of the project to not compromise any config location.

```bash
# Run this command on your machine
docker run -d --hostname my-rabbit --name CodeGoRabbitMQ -p 8080:15672 -p 5672:5672 -v ./Configs/RabbitMQ/rabbitmq.conf:/etc/rabbitmq/rabbitmq.conf:ro -v ./Configs/RabbitMQ/definitions.json:/etc/rabbitmq/definitions.json:ro rabbitmq:3-management
```

The command above starts an container an map the the ports to your computer. You can access the this [RabbitMQ Management](http://localhost:8080), that has open with your container to management the MQ Queues and see what have been created. the base user is guest/guest.
