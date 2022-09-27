using MagicVilla_VillaApi.DataAccess;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Models.DTOs;
using MagicVilla_VillaApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MagicVilla_VillaApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _conext;
        private readonly string secretKey;
        public UserRepository(AppDbContext conext, IConfiguration configuration)
        {
            _conext = conext;
            secretKey = configuration.GetSection("ApiSettings:Secret").Value;
        }
        public bool IsUniqueUser(string username)
        {
            var user = _conext.LocalUsers.FirstOrDefault(x => x.UserName == username);

            return user == null;
        }

        public async Task<LoginResponeDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await _conext.LocalUsers.FirstOrDefaultAsync(
                u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower() &&
                     u.Password == loginRequestDTO.Password
            );

            if (user == null)
            {
                return new LoginResponeDTO 
                {
                    Token = "",
                    User = null
                };
            }
            // if user found generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject =new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponeDTO loginResponeDTO = new()
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };
            return loginResponeDTO;

        }

        public async Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            LocalUser user = new()
            {
                UserName = registrationRequestDTO.UserName,
                Name = registrationRequestDTO.Name,
                Password = registrationRequestDTO.Password,
                Role = registrationRequestDTO.Role??"Customer"
            };

            await _conext.LocalUsers.AddAsync(user);
            await _conext.SaveChangesAsync();

            user.Password = "";
            return user;
            
        }
    }
}
