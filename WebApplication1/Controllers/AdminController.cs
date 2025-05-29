using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;

        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }

            List<UserModel> userList = new List<UserModel>();
            string connectionString = _configuration.GetConnectionString("SQLiteConnection");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users";
                using (var command = new SqliteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userList.Add(new UserModel
                        {
                            Id = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Password = reader.GetString(2),
                            Role = reader.GetString(3),
                            RedirectPage = reader.GetString(4)
                        });
                    }
                }
            }

            return View(userList);
        }

        [HttpPost]
        public IActionResult Add(UserModel user)
        {
            if (ModelState.IsValid)
            {
                string connectionString = _configuration.GetConnectionString("SQLiteConnection");

                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Users (Username, Password, Role, RedirectPage) VALUES (@username, @password, @role, @redirect)";
                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", user.Username ?? "");
                        command.Parameters.AddWithValue("@password", user.Password ?? "");
                        command.Parameters.AddWithValue("@role", user.Role ?? "");
                        command.Parameters.AddWithValue("@redirect", user.RedirectPage ?? "");

                        command.ExecuteNonQuery();
                    }
                }

                TempData["SuccessMessage"] = "Kullanıcı başarıyla eklendi!";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Lütfen tüm alanları doldurun.";
            return View("Index");
        }

        public IActionResult Delete(int id)
        {
            string connectionString = _configuration.GetConnectionString("SQLiteConnection");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Users WHERE Id = @id";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index"); 
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            string connectionString = _configuration.GetConnectionString("SQLiteConnection");
            UserModel user = null;

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE Id = @id";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new UserModel
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Password = reader.GetString(2),
                                Role = reader.GetString(3),
                                RedirectPage = reader.GetString(4)
                            };
                        }
                    }
                }
            }

            if (user == null)
            {
                TempData["ErrorMessage"] = "Kullanıcı bulunamadı.";
                return RedirectToAction("Index");
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(UserModel user)
        {
            string connectionString = _configuration.GetConnectionString("SQLiteConnection");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Users SET Username = @username, Password = @password, Role = @role, RedirectPage = @redirect WHERE Id = @id";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", user.Username);
                    command.Parameters.AddWithValue("@password", user.Password);
                    command.Parameters.AddWithValue("@role", user.Role);
                    command.Parameters.AddWithValue("@redirect", user.RedirectPage);
                    command.Parameters.AddWithValue("@id", user.Id);

                    command.ExecuteNonQuery();
                }
            }

            TempData["SuccessMessage"] = "Kullanıcı güncellendi.";
            return RedirectToAction("Index");
        }
    }
}
