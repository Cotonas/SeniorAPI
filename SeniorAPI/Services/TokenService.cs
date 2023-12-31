﻿using Microsoft.IdentityModel.Tokens;
using SeniorAPI.Data;
using SeniorAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SeniorAPI.Services
{
    public class TokenService
    {
        public static object GenerateToken(UserModel user)
        {
            var key = Encoding.ASCII.GetBytes(Key.Secret);
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("pessoaCodigo", user.Login.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);

            return new
            {
                token = tokenString
            };
        }
    }
}
