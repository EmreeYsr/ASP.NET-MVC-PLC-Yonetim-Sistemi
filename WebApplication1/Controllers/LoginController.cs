using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginCheck(string username, string password)
        {
            string connectionString = _configuration.GetConnectionString("SQLiteConnection");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE Username = @username AND Password = @password";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string role = reader["Role"].ToString();
                            string redirectPage = reader["RedirectPage"].ToString();

                            HttpContext.Session.SetString("Username", username);
                            HttpContext.Session.SetString("Role", role);

                            return Redirect(role == "Admin" ? "/Admin/Index" : redirectPage);
                        }
                    }
                }
            }

            ViewBag.Error = "Login failed. Please check your username and password.";

            return View("Index");
        }
    }
}
