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