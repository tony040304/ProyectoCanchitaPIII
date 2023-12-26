using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.DTO;
using Models.Helper;
using Models.MODELS;
using Models.ViewModel;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class AuthenticateServices : IAuthenticate
    {
        private readonly CANCHITASGOLContext _context;
        private readonly IConfiguration _appSetings;

        public AuthenticateServices(CANCHITASGOLContext context, IConfiguration appSetting)
        {
            _context = context;
            _appSetings = appSetting;
        }   

        public string CreatUser(UserDTO User)
        {
            if (string.IsNullOrEmpty(User.Username))
            {
                return "Ingrese usuario";
            }

            Users? user = _context.Users.FirstOrDefault(x => x.Username == User.Username);
            if (user != null)
            {
                return "Usuario existente";
            }

            _context.Users.Add(new Users()
            {
                Username = User.Username,
                Email = User.Email,
                Userpassword = User.Userpassword,
                Role = User.Role,               
            });
            _context.SaveChanges();

            string response = GetToken(_context.Users.OrderBy(x => x.Id).Last());
            return response;
        }   

        public string Login(AuthenticateViewModel User)
        {
            Users? user = _context.Users.FirstOrDefault(x => x.Username == User.Username && x.Userpassword == User.Password);
            if (user == null)
            {
                return string.Empty;
            }

            return GetToken(user);

        }

        private string GetToken(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSetings["AppSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("userId", user.Id.ToString()));
            claimsForToken.Add(new Claim("given_name", user.Username));
            claimsForToken.Add(new Claim("email", user.Email));
            claimsForToken.Add(new Claim("role", user.Role.ToString()));

            var Sectoken = new JwtSecurityToken(_appSetings["AppSettings:Issuer"],
              _appSetings["AppSettings:Issuer"],
              claimsForToken,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
            return token;
        }
    }
}
