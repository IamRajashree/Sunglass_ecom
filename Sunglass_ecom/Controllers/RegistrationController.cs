using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sunglass_ecom.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity.Data;
using System;
using Sunglass_ecom.Data;

namespace Sunglass_ecom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly EcommerceDbContext _dbContext;
       

        // Constructor to inject AppDbContext
        public RegistrationController(EcommerceDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registration")]
        public string Registration(Registration registration)
        {
            string responseMessage;

            // Retrieve the connection string from configuration
            string connectionString = _configuration.GetConnectionString("conn").ToString();

            // Use `using` statements for proper resource management
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open(); // Open the database connection

                    // SQL query with parameters to avoid SQL injection
                    string query = @"
                INSERT INTO Customer
                (Username, Password, Email, FirstName, LastName, DateOfBirth, PhoneNumber, City, Zones, Streets, Address) 
                VALUES 
                (@Username, @Password, @Email, @FirstName, @LastName, @DateOfBirth, @PhoneNumber, @City, @Zones, @Streets, @Address)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters with appropriate values
                        cmd.Parameters.AddWithValue("@Username", registration.Username);
                        cmd.Parameters.AddWithValue("@Password", registration.Password);
                        cmd.Parameters.AddWithValue("@Email", registration.Email);
                        cmd.Parameters.AddWithValue("@FirstName", registration.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", registration.LastName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", registration.DateOfBirth);
                        cmd.Parameters.AddWithValue("@PhoneNumber", registration.PhoneNumber);
                        cmd.Parameters.AddWithValue("@City", registration.City);
                        cmd.Parameters.AddWithValue("@Zones", registration.Zones);
                        cmd.Parameters.AddWithValue("@Streets", registration.Streets);
                        cmd.Parameters.AddWithValue("@Address", registration.Address);

                        // Execute the command
                        int rowsAffected = cmd.ExecuteNonQuery();
                       

                        // Check if the data was successfully inserted
                        responseMessage = rowsAffected > 0 ? "Data inserted successfully!" : "Error inserting data.";
                    }
                }
                catch (Exception ex)
                {
                    // Return a detailed error message
                    responseMessage = $"An error occurred: {ex.Message}";
                }
                finally
                {
                    con.Close();
                }
            }

            return responseMessage;
        }

       
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            try
            {
                // Check if the user exists in the database
                var user = _dbContext.Registrations
                    .FirstOrDefault(u => u.Username == loginDto.Username);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Verify the password
                if (user.Password != loginDto.Password)
                {
                    return Unauthorized("Invalid password.");
                }

                // Login successful
                return Ok(new { Message = "Login successful", User = user });
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



    }


}

