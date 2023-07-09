using CST_350_Milestone.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CST_350_Milestone.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly string _connectionString;

        public RegistrationController(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProcessRegistration(UserModel user)
        {
            if (ModelState.IsValid)
            {
                // Insert the user data into the Users table
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO Users (FirstName, LastName, Sex, Age, State, Email, Username, Password) " +
                        "VALUES (@FirstName, @LastName, @Sex, @Age, @State, @Email, @Username, @Password)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Set parameter values
                        command.Parameters.AddWithValue("@FirstName", user.FirstName);
                        command.Parameters.AddWithValue("@LastName", user.LastName);
                        command.Parameters.AddWithValue("@Sex", user.Sex);
                        command.Parameters.AddWithValue("@Age", user.Age);
                        command.Parameters.AddWithValue("@State", user.State);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@Username", user.Username);
                        command.Parameters.AddWithValue("@Password", user.Password);

                        // Open the connection and execute the query
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                return View();
            }
            return View();
        }
    }
}
