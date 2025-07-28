using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Presentation.Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JWTSettings _settings;

        public JwtTokenGenerator(IOptions<JWTSettings> options)
        {
            _settings = options.Value;
        }

        public string Generate(Guid userId, string name, Domain.Models.Role role)
        {
            var key = Encoding.ASCII.GetBytes(_settings.SecretKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>
             {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role,role.Id.ToString()),
                new Claim(ClaimTypes.Name, name),
             };

           

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                NotBefore = DateTime.UtcNow,
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

       
    }
}

