using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Helper;
using Models.MODELS;
using Models.ViewModel;
using Services.Helper;
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
        private readonly AppSetings _appSetings;

        public AuthenticateServices(CANCHITASGOLContext context, IOptions<AppSetings> appSetting)
        {
            _context = context;
            _appSetings = appSetting.Value;
        }   

        public string CreatUser(UserViewModel User)
        {
            if (string.IsNullOrEmpty(User.UserName))
            {
                return "Ingrese usuario";
            }

            Users? user = _context.Users.FirstOrDefault(x => x.Username == User.UserName);
            if (user == null)
            {
                return "Usuario existente";
            }

            _context.Users.Add(new Users()
            {
                Username = user.Username,
                Email = user.Email,
                Userpassword = user.Userpassword.GetSHA384(),
                Role = user.Role,
                
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
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSetings.key);
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Username),
                        //new Claim(ClaimTypes.Role, user.Role)
                    }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.Aes128KW)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescription));
        }
    }
}
