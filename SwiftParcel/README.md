here the very nice and super proferrional html and css of the projec t name goes ...
# 🚀 Swift Parcel 📦



# Solution start

To start the sollution of the current project:

1. Open the d-docker-composer directory:

```bash
cd  d-docker-composer directory
```

2. To run the infrastructure of the project in the background run:

```bash
docker-compose -f micro-infrastructure.yml up
```

3. After this you can:

    - 3.1.Start each of the project independently:
         - By going to the scripts directory of the project directory you want to run up and execute:

            ```bash
            ./start.sh # remember about the chmod +x ./start.sh if the previlages of execution are not in the state of presence
            ```
    - 3.2. Start all the project by running the contatintes defined with the images with the help of execution:
        ```bash
        docker-compose -f micro-services-local.yml up
        ```