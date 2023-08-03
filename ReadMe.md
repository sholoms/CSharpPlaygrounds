## To run using docker

To create the docker image run the following in your terminal from within the ApiPlayground folder 

```docker build -t calculator -f Dockerfile .```

to run the container

```docker run -d -p 9028:9028 --name caluclator-api --mount type=bind,source="$(pwd)"/data/test.txt,target=/app/test.txt calculator```

to run Rabbitmq
```docker run -d --hostname my-rabbit --name some-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management```

for more info visit the [docker tutorial](https://docs.microsoft.com/en-us/dotnet/core/docker/build-container?tabs=linux)

controller generated using swagger and nswag in repository root 
to regenerate run ```nswag run``` from the root

to run the db in docker pull the db image using
```docker pull mcr.microsoft.com/mssql/server:2022-latest```
and then start the container with 

```
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Str0ngPassW0rd" \
   -p 55000:1433 --name sql1 --hostname sql1 \
   -d \
   mcr.microsoft.com/mssql/server:2022-latest
```


TODO
work out how to get cotainers to talk to each other when running everything in docker