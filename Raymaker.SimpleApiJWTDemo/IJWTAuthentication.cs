using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Raymaker.SimpleApiJWTDemo
{
    public interface IJWTAuthentication
    {
        string Login(string username, string password);
    }

    public class JWTAuthentication : IJWTAuthentication
    {
        private readonly string user = "string";
        private readonly string pwd = "string";
        private readonly string secret;

        public JWTAuthentication(string secret)
        {
            this.secret = secret;
        }

        public string Login(string username, string password)
        {
            try
            {
                if (!user.Equals(username) && !pwd.Equals(password))
                {
                    return null;
                }
                
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                        new[]
                        {
                            new Claim(ClaimTypes.Name, username)
                        }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
