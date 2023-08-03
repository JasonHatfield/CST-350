using System;
using System.Data;
using System.Data.SqlClient;
using CST_350_Milestone.Models;

namespace CST_350_Milestone.Services;
#pragma warning disable S101 // Types should be named in PascalCase
public class SecurityDAO
#pragma warning restore S101 // Types should be named in PascalCase
{
	private readonly string _connectionString =
		@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UserManagementDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

	public bool FindUserByNameAndPassword(UserModel user)
	{
		var success = false;

		var sqlStatement = "SELECT * FROM dbo.Users WHERE username = @USERNAME and password = @PASSWORD";

		using (var connection = new SqlConnection(_connectionString))
		{
			var command = new SqlCommand(sqlStatement, connection);

			command.Parameters.Add("@USERNAME", SqlDbType.VarChar, 40).Value = user.Username;
			command.Parameters.Add("@PASSWORD", SqlDbType.VarChar, 40).Value = user.Password;

			try
			{
				connection.Open();
				var reader = command.ExecuteReader();

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

	public int GetUserIdByUsername(string username)
	{
		string query = "SELECT Id FROM dbo.Users WHERE Username = @Username";
		using (SqlConnection cn = new SqlConnection(_connectionString))
		using (SqlCommand cmd = new SqlCommand(query, cn))
		{
			cmd.Parameters.AddWithValue("@Username", username);
			cn.Open();
			using (SqlDataReader reader = cmd.ExecuteReader())
			{
				if (reader.HasRows)
				{
					reader.Read();
					return reader.GetInt32(0);  // return the user's ID
				}
			}
		}
		return 0;  // return 0 if user not found
	}

}