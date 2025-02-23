# Coding Tracker

Coding Tracker is a C# console crud application that allows you to track how long you code. Featured on [The C# Academy](https://www.thecsharpacademy.com/)

# Requirements:
1. This is an application designed to help you track how long each of your coding sessions last.
2. User must imput both start date/time end date/time in the format specified. 
3. The application stores this information in a sqlite database which is created at application startup if it does not already exist.
4. The coding sessions will be stored in a database table and the user should be able to insert, delete update and view all coding sessions.
5. All possible errors should be handled so that the application does not crash.
6. Uses Dapper which is a micro-ORM to interact with the database.
7. Users will not input the duration of the coding session - this will be done automatically.
8. Application uses a configuration file that contains relevant information for database creation/connection.

# Features
- Sqlite Database Connection
- Console application to allows users to track time spent coding.
- Allows users to manage all crud operations on the database from the console

# External libraries used:
[Spectre Console](https://github.com/spectreconsole/spectre.console)

[Microsoft.Data.Sqlite](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/?tabs=net-cli)
[Dapper](https://github.com/DapperLib/Dapper)

