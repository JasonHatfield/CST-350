using LoginForm.Models;
using System.Data.SqlClient;


namespace LoginForm.Services
{
#pragma warning disable S101 // Types should be named in PascalCase
    public class SecurityDAO
#pragma warning restore S101 // Types should be named in PascalCase
    {
        private readonly string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UserManagementDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

         public bool FindUserByNameAndPassword(UserModel user)
         {
             bool success = false;

             string sqlStatement = "SELECT * FROM dbo.Users WHERE username = @USERNAME and password = @PASSWORD";

             using (SqlConnection connection = new(_connectionString))
             {
                 SqlCommand command = new(sqlStatement, connection);

                 command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 40).Value = user.UserName;
                 command.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar, 40).Value = user.Password;

                 try
                 {
                     connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                     if (reader.HasRows)
                         success = true;
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine(ex.Message);
                 }
             }
             return success;
         }
    }
}
