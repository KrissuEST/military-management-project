# military-management-project

## Military planning, management, database.
### Running the project:
- Add project to your development environment program.
- From "Solution items" folder you can find file: docker-compose.yml, <br/>
  right click on the file -> Run '..: Compose Deployment' <br/>
  Also can add it to your own Docker Desktop program. <br/>
  Then you need to run your Docker image to use it like a database.
- While Docker image is running you can run the program with your <br/>
  development environment program.
- When running use in: Run configurations - WebApp: http
- If I understand correctly no need to do DB migrations because migrations <br/>
  are already in the project.

### Need to remember:
- Military plans are visible only when you have registered a user and logged in.
- Currently it's basically CRUD application with user registration and log in system.
- Comments added to the code everywhere where it was possible to make things more clearer.

### ERD diagram:
![ERD diagram](https://github.com/KrissuEST/military-management-project/assets/5465035/b1733e4f-8191-4526-9a1a-bd776b6985b5)

# Generate db migration

~~~bash
# install or update
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef

# create migration for db
dotnet ef migrations add Initial --project DAL.EF.App --startup-project WebApp --context ApplicationDbContext 
dotnet ef migrations add Token --project DAL.EF.App --startup-project WebApp --context ApplicationDbContext 
# migration for event person
dotnet ef migrations add Planperson --project DAL.EF.App --startup-project WebApp --context ApplicationDbContextt

# apply migration for db
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
# Reame vaja täiendada!
# Use area
# MVC
dotnet aspnet-codegenerator controller -m MilitaryPlan -name MilitaryPlansController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
# Rest API, generating controllers
dotnet aspnet-codegenerator controller -m MilitaryPlan -name MilitaryPlansController -outDir ApiControllers -api -dc ApplicationDbContext  -udl -f
~~~


Generate Identity UI, generating pages

~~~bash
cd WebApp
dotnet aspnet-codegenerator identity -dc DAL.EF.App.ApplicationDbContext --userClass AppUser -f
~~~