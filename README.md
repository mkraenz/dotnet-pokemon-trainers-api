## Some Commands

```sh
# create new api project in directory `dotnettest` with .net6.0
dotnet new webapi -f net6.0 -o dotnettest
# create .gitignore for .net
dotnet new gitignore

# core commands
dotnet build
dotnet run
dotnet watch

# add package
dotnet add package <package e.g. Microsoft.EntityFrameworkCore.Sqlite>

# entity framework commands
## globally install entity framework CLI
dotnet tool install --global dotnet-ef
## create migration
dotnet ef migrations add InitialCreate --context PizzaContext
## remove all(?) migrations and the snapshot
dotnet ef migrations remove --context PizzaContext
## apply migration
dotnet ef database update --context PizzaContext
```
