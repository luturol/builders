# Builders

It's a .Net 5 project that implements a Binary Search Tree.

## Dependencies

- Docker
- .Net 5 installed (optional)


## How to run

### Running with docker

to run using docker, you can just execute the following command:

```bash
docker-compose up
```

It will start two containers, one that has the API and another that has MongoDB. You can access the project by navigating through http://localhost:8080

### Running in local
To run the project in local machine, execute the following command:

```bash
dotnet run --project .\Builders\
```

To execute tests, you can run in root folder:

```bash
dotnet test .\Builders.Test\
```

To execute integration test, you can run in root folder:

```bash
dotnet test .\Builders.Integration.Test\
```

## Endpoints

Running the project with docker, you will access endpoints using 8080 as the port in localhost.

1. Palindrome
    http://localhost:8080/Palindrome?word=arara

    By setting the word it will execute the endpoint with GET Method.

1. Binary Search Tree

    - GET http://localhost:8080/BinarySearchTree/{id}

    - GET Find Node With Value http://localhost:8080/BinarySearchTree/{id}/{value}

    - POST http://localhost:8080/BinarySearchTree/

        In the body you will pass a list with integers.
        ```json
        [ ]
        ```

    - PATCH http://localhost:8080/BinarySearchTree/{id}

        In the body you will pass a list with integers.
        ```json
        [ ]
        ```

    - DELETE http://localhost:8080/BinarySearchTree/{id}
