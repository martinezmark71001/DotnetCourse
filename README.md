# C# .NET with MS SQL Complete Beginner to Master 2024
##Overview
This repository is dedicated on learning the necessary knowledge to build a console application and asp.net application.
# API Overview
## Features

* **API Endpoints:**
  + User management (registration, login, profile management)
  + Data retrieval (GET endpoints for specific resources)
  + Data manipulation (POST, PUT, DELETE endpoints for specific resources)
* **Authentication and Authorization:**
  + Support for JWT
* **Data Validation and Error Handling:**
  + Input validation using data annotations or FluentValidation
  + Standardized error responses
## API Endpoints

| Method | Endpoint | Description |
| GET | /UserComplete/TestConnection | Test Connection |
| GET | /UserComplete/GetUsers | Retrieve all users |
| GET | /UserComplete/GetUsers/{0}/false | Retrieve a user by ID |
| GET | /UserComplete/GetUsers/0/true | Retrieve all active user |
| DEL | UserComplete/DeleteUser/{0} | Delete a user by ID |
| PUT | UserComplete/UpsertUser | Create or Update a user by form|
| POST | /Auth/Login | User login to create a token|
| GET | /Auth/RefreshToken | Refresh Token |
| PUT | /Post/UpsertPost | Create or Update a post |
| GET | /Post/MyPosts | Retrieve all posts |
| GET | /Post/Posts/{0}/0/0 | Retrieve post by UserID |
| GET | /Post/Posts/0/{0}/0 | Retrieve post by Post ID |
| GET | /Post/Posts/0/0/{search} | Retrieve user by search parameter (title/content) |
| DEL | /Post/Post/{0} | Delete post by ID |

## Getting Started

# Running the SQL Script
## Prerequisites
* SQL Server 
* The SQL script file `TablePrep.sql` located in the `SQLQueryFiles` folder
* The SQL script file `PostSqlScript.sql` located in the `SQLQueryFiles` folder
* The Stored Procedure SQL script file `ProcedureAuth_Registration.sql`, `ProcedureUser_Get.sql` , `ProcedureUser_Upsert.sql` ,`ProcedureUser_Delete.sql` ,`ProcedurePosts_Upsert.sql`, `ProcedurePosts_Get.sql`, `ProcedurePosts_Delete.sql` located in `SQLQueryFiles` folder
## Steps
1. Open a query window in your database management system.
2. Execute the query.
   + `TablePrep.sql`
   + `PostSqlScript.sql`
3. Execute the query for stored procedure.
   + `ProcedureAuth_Registration.sql`
   + `ProcedureUser_Get.sql`
   + `ProcedureUser_Upsert.sql`
   + `ProcedureUser_Delete.sql`
   + `ProcedurePosts_Upsert.sql`
   +  `ProcedurePosts_Get.sql`
   +  `ProcedurePosts_Delete.sql` 

# Running the Program
## Prerequisites
* Dotnet API
* The folder name `DotnetAPI`.
## Steps
1. Open a command prompt or terminal.
2. Open the folder name `DotnetAPI`.
3. Run the program using `Dotnet run` or `Dotnet watch run`.

# Using Postman to Test the API

## Prerequisites
* Postman application installed
* The postman collection file `Localhost-5000.postman_collection.json` located in Postman Collection
## Steps

1. Import the API collection file `Localhost-5000.postman_collection.json`.
2. Choose the request method and add parameter as needed
* **POST localhost:5000/Auth/Login:**
	+ Body Parameters: `{"email": {"your email"},"password": {"your password"}}`
3. Send the request and verify the response.
4. Use the token for chosen request method and change the parameter as needed
