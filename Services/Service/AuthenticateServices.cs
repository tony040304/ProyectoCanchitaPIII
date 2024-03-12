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

        public string CreateUser(UserDTO User)
        {
            if (string.IsNullOrEmpty(User.Username))
            {
                return "Ingrese usuario";
            }
            Pitch? pitch = _context.Pitch.FirstOrDefault(x => x.Nombre == User.Username);
            Users? user = _context.Users.FirstOrDefault(x => x.Username == User.Username);

            if (pitch != null || user != null)
            {
                return "Nombre en uso";
            }
            Pitch? pitchMail = _context.Pitch.FirstOrDefault(x => x.Email == User.Email);
            Users? userMail = _context.Users.FirstOrDefault(x => x.Email == User.Email);

            if (pitchMail != null || userMail != null)
            {
                return "Email en uso";
            }

            _context.Users.Add(new Users()
            {
                Id = User.Id,
                Username = User.Username,
                Userpassword = User.Userpassword,
                Email = User.Email
            });
            _context.SaveChanges();

            string response = GetToken(_context.Users.OrderBy(x => x.Id).Last());
            return response;
        }
        public string CreatePitch(PitchDTO Pitch)
        {
            if (string.IsNullOrEmpty(Pitch.Nombre))
            {
                return "Ingrese el nombre Cancha";
            }

            Users? user = _context.Users.FirstOrDefault(x => x.Username == Pitch.Nombre);
            Pitch? pitch = _context.Pitch.FirstOrDefault(x => x.Nombre == Pitch.Nombre);
            if (pitch != null || user != null)
            {
                return "Nombre en uso";
            }

            Pitch? pitchMail = _context.Pitch.FirstOrDefault(x => x.Email == Pitch.Email);
            Users? userMail = _context.Users.FirstOrDefault(x => x.Email == Pitch.Email);

            if (pitchMail != null || userMail != null)
            {
                return "Email en uso";
            }

            _context.Pitch.Add(new Pitch()
            {
                Id = Pitch.Id,
                Nombre = Pitch.Nombre,
                Password = Pitch.Password,
                Email = Pitch.Email,
                Horario = Pitch.Horario,
                Ubicacion = Pitch.Ubicacion,
                IsBlocked = false
            });
            _context.SaveChanges();

            string response = GetToken(_context.Pitch.OrderBy(x => x.Id).Last());
            return response;
        }
        public string Login(AuthenticateViewModel User)
        {
            Users? user = _context.Users.FirstOrDefault(x => x.Username == User.name && x.Userpassword == User.Password);
            if (user != null)
            {
                return GetToken(user);
            }

            Pitch? pitch = _context.Pitch.FirstOrDefault(x=>x.Nombre == User.name && x.Password == User.Password);
            if (pitch != null)
            {
                return GetToken(pitch);
            }

            return string.Empty;

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
        private string GetToken(Pitch pitch)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSetings["AppSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("userId", pitch.Id.ToString()));
            claimsForToken.Add(new Claim("given_name", pitch.Nombre));
            claimsForToken.Add(new Claim("email", pitch.Email));
            claimsForToken.Add(new Claim("roles", pitch.Role.ToString()));

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
