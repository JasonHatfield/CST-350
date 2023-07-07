## Database Setup Instructions

1. Open Visual Studio 2022.
2. Connect to the SQL Server instance using SQL Server Object Explorer:
   - Go to View -> SQL Server Object Explorer.
   - Right-click on "SQL Server" and select "Add SQL Server."
   - Enter the server name and authentication details for your SQL Server instance.
   - Click "Connect."

3. Create a new database:
   - Right-click on the "Databases" node and select "Add New Database."
   - Enter a name for the database (e.g., UserManagementDB).
   - Configure any additional settings as needed.
   - Click "OK" to create the database.

4. Update the `appsettings.json` file:
   - Open your Visual Studio project.
   - Locate the `appsettings.json` file.
   - Replace the existing contents with the following code:
     ```json
     {
       "Logging": {
         "LogLevel": {
           "Default": "Information",
           "Microsoft.AspNetCore": "Warning"
         }
       },
       "AllowedHosts": "*",
       "ConnectionStrings": {
         "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=UserManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true"
       }
     }
     ```

5. Save the `appsettings.json` file.

6. Run the following SQL script to create the "Users" table in the "UserManagementDB" database:
   ```sql
   USE UserManagementDB;

   CREATE TABLE Users (
       Id INT PRIMARY KEY,
       FirstName NVARCHAR(50),
       LastName NVARCHAR(50),
       Sex NVARCHAR(10),
       Age INT,
       State NVARCHAR(50),
       EmailAddress NVARCHAR(100),
       Username NVARCHAR(50),
       Password NVARCHAR(50)
   );
   ```

7. Run the following SQL queries to add the 10 users to the "Users" table:
    ```sql
    -- Insert 10 user records with unique and real-world data
    INSERT INTO Users (Id, FirstName, LastName, Sex, Age, State, EmailAddress, Username, Password)
    VALUES
        (1, 'Emma', 'Johnson', 'Female', 28, 'California', 'emma.johnson@example.com', 'emma.johnson', 'P@ssw0rd123'),
        (2, 'Oliver', 'Brown', 'Male', 35, 'New York', 'oliver.brown@example.com', 'oliver.brown', 'Secret456'),
        (3, 'Sophia', 'Lee', 'Female', 42, 'Texas', 'sophia.lee@example.com', 'sophia.lee', 'StrongP@ssword789'),
        (4, 'James', 'Garcia', 'Male', 31, 'Florida', 'james.garcia@example.com', 'james.garcia', 'S3curePwd!'),
        (5, 'Ava', 'Martinez', 'Female', 39, 'California', 'ava.martinez@example.com', 'ava.martinez', 'Pa$$w0rd123'),
        (6, 'Liam', 'Lopez', 'Male', 26, 'Texas', 'liam.lopez@example.com', 'liam.lopez', 'MyP@ssw0rd!'),
        (7, 'Mia', 'Nguyen', 'Female', 33, 'New York', 'mia.nguyen@example.com', 'mia.nguyen', 'StrongPwd789'),
        (8, 'Noah', 'Wang', 'Male', 29, 'California', 'noah.wang@example.com', 'noah.wang', 'P@ssw0rd!'),
        (9, 'Isabella', 'Smith', 'Female', 36, 'Florida', 'isabella.smith@example.com', 'isabella.smith', 'MySecretPwd123'),
        (10, 'Lucas', 'Kim', 'Male', 34, 'Texas', 'lucas.kim@example.com', 'lucas.kim', 'P@ssw0rd789');
    ```

8. The database setup is now complete. You can access and utilize the "UserManagementDB" database in your application.
