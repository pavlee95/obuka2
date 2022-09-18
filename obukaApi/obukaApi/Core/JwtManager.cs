using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace obukaApi.Core
{
    public class JwtManager
    {
        private readonly BazaContext _context;

        public JwtManager(BazaContext context)
        {
            _context = context;
        }

        public string MakeToken(string firstName, string password) 
        {
            var user = _context.Users.Include(u => u.UserRoles)
                .FirstOrDefault(x => x.FirstName == firstName && x.Password == password);

            if (user == null)
            {
                return null;
            }

            var actor = new JwtActor
            {
                Id = user.Id,
                AllowedRoles = user.UserRoles.Select(x => x.RoleId),
                Identity = user.FirstName
            };


            var issuer = "asp_api"; 
            var secretKey = "ThisIsMyVerySecretKey"; 
            var claims = new List<Claim> 
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, issuer), 
                new Claim(JwtRegisteredClaimNames.Iss, "asp_api", ClaimValueTypes.String, issuer),  
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, issuer), 
                new Claim("UserId", actor.Id.ToString(), ClaimValueTypes.String, issuer), 
                new Claim("ActorData", JsonConvert.SerializeObject(actor), ClaimValueTypes.String, issuer)
                
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)); 

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); 

            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken( 
                issuer: issuer, 
                audience: "Any", 
                claims: claims, 
                notBefore: now, 
                expires: now.AddSeconds(300),  
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }
    }
}
