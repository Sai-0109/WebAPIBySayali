using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using WebApiFirst.Models;

namespace WebApiFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        AppDbContext _db;
        IConfiguration _config;
        public AccountController(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }
        
        [HttpPost]
        [Route("SignUp")]
        public IActionResult Register(RegisterModel registerModel)
        {
            if (registerModel != null)
            {
                if (ModelState.IsValid)
                {
                    User user = new User()
                    {
                        FirstName = registerModel.FirstName,
                        LastName = registerModel.LastName,
                        Email = registerModel.Email,
                        UserName = registerModel.Email,
                        Password = registerModel.Password
                    };
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    return Ok();
                }
                return BadRequest("Please Correct Input Values");

            }
            return BadRequest("Please Correct Input Values");
        }
        
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            if (loginModel != null)
            {
                if (ModelState.IsValid)
                {
                    var user = _db.Users.Where(u => u.Email.Equals(loginModel.Email)
                      && u.Password.Equals(loginModel.Password))?.FirstOrDefault();
                    if (user != null)
                    {
                        UserModel userModel = new UserModel()
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            UserName = user.UserName,
                            Email = user.Email,
                            //AccessToken="Login Success"
                        };
                        userModel.AccessToken = GenerateToken(userModel);
                        return Ok(userModel);
                    }
                    return NotFound("User Not exists");
                }
                return BadRequest("Invalid Email And / Or Password");

            }
            return BadRequest("Please Enter Email And Password");
        }
      
        private string GenerateToken(UserModel userModel)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                new Claim("UserId",userModel.Id.ToString()),
                new Claim("DisplayName",userModel.UserName),
                new Claim("UserName",userModel.UserName),
                new Claim("Email",userModel.Email)

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var signIn = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);
            return new JwtSecurityTokenHandler().WriteToken(token); 

        }
    }
}
