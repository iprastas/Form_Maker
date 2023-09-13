using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Security.Claims;

namespace FormMaker.Pages
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
		readonly IConfiguration configuration;


		public IndexModel(ILogger<IndexModel> logger, IConfiguration _configuration)
		{
			_logger = logger;
			configuration = _configuration;

		}

        [BindProperty]
		public string Username { get; set; } = "";
        [BindProperty]
        public string Password { get; set; } = "";

        public IActionResult OnGetExit()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Index");
        }

        public IActionResult OnPostEnter()
        {
            if (ModelState.IsValid && Authenticate(Username, Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, Username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties { AllowRefresh = true };
                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return Redirect("/Home");
            }
            return RedirectToPage();
        }
        private bool Authenticate(string user, string pwd)
        {
            string? connectionString = configuration.GetConnectionString("DefaultConnection");
			string login = ""; string password = "";

			using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                Npgsql.NpgsqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM svod2.targetusers WHERE login='admin'";

                Npgsql.NpgsqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    login = reader.GetString(0);
                    if (!reader.IsDBNull(1))
                        password = reader.GetString(1);
                }

                conn.Close();
            }
            if (user == login && pwd == password)
                return true;
			return false;
        }
    }
}