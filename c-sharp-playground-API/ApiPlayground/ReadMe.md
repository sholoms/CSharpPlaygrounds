## To run using docker

To create the docker image run the following in your terminal from within the ApiPlayground folder 

```docker build -t calculator -f Dockerfile .```

to run the container

```docker run -d -p 9028:9028 --name caluclator-api --mount type=bind,source="$(pwd)"/data/test.txt,target=/app/test.txt```


for more info visit the [docker tutorial](https://docs.microsoft.com/en-us/dotnet/core/docker/build-container?tabs=linux)