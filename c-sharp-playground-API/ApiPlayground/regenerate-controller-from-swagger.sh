docker pull asia.gcr.io/glx-devops-au/docker-nswag:28
docker run --rm -u ${UID}:${GROUPS} -v ${PWD}/templates:/templates -v ${PWD}:/src -w /src asia.gcr.io/glx-devops-au/docker-nswag:28 run ./nswag.json /runtime:Net60
