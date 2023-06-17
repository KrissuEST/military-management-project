# military-management-project

## Military planning, management, database.

# Generate db migration

~~~bash
# install or update
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef

# create migration
dotnet ef migrations add Initial --project DAL.EF.App --startup-project WebApp --context ApplicationDbContext 

# apply migration
dotnet ef database update --project DAL.EF.App --startup-project WebApp --context ApplicationDbContext 
~~~


# generate rest controllers

Add nuget packages
- Microsoft.VisualStudio.Web.CodeGeneration.Design
- Microsoft.EntityFrameworkCore.SqlServer
-
~~~bash
# install tooling
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet tool update --global dotnet-aspnet-codegenerator

cd WebApp
# MVC
dotnet aspnet-codegenerator controller -m TrainingPlan -name TrainingPlansController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
# Rest API
dotnet aspnet-codegenerator controller -m TrainingPlan -name TrainingPlansController -outDir ApiControllers -api -dc ApplicationDbContext  -udl -f
~~~


Generate Identity UI

~~~bash
cd WebApp
dotnet aspnet-codegenerator identity -dc DAL.EF.App.ApplicationDbContext --userClass AppUser -f 
~~~