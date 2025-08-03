## How to Run the Backend and Access Swagger

1. Run the backend application:

   dotnet run

2. Access Swagger:

   After running the backend application, open your browser and navigate to:

   https://localhost:$port_number/swagger

   Replace port_number with the actual port number shown in your terminal.

3. Access Database:
   mysql -u root -p

   Passworld: 12082003

4. Add a Migrations after change code:
   dotnet ef migrations add <MigrationName>

   Then, use follow syntax to update
   dotnet ef database update

   If you want to remove last migration, use follow syntax:
   dotnet ef migrations remove
