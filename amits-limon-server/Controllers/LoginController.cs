using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amits_limon_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        readonly private ILogger<LoginController> _logger;


        public LoginController(IConfiguration config,ILogger<LoginController> logger)
        {
            _config = config;
            _logger = logger;
        }
        [HttpPost]
        public IActionResult Login([FromBody] AdminModel admin)
        {
            IActionResult response = Unauthorized();
            if (IsAdmin(admin))
            {
                var tokenString = GenerateJSONWebToken();
                response = Ok(new { token = tokenString });
                _logger.LogInformation("admin logged in");
            }
            return response;
        }

        private string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool IsAdmin(AdminModel admin)
        {
            // app for now have only 1 admin so his hard coded
            string validName = "Admin";
            string validPassword = "12345";
            return admin.Name == validName && admin.Password == validPassword;
        }
    }

    public class AdminModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
