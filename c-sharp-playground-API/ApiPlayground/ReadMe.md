## To run using docker

To create the docker image run the following in your terminal from within the ApiPlayground folder 

```docker build -t counter-image -f Dockerfile .`

to create the container run

```docker create --name  calculator-api calculator`

to run the container run 

```docker run -d -p 9028:9028 --name caluclator calculator-api`


for more info visit the [docker tutorial](https://docs.microsoft.com/en-us/dotnet/core/docker/build-container?tabs=linux)