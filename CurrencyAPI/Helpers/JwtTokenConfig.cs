using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Helpers
{
    /// <summary>
    ///
    /// </summary>
    public static class JwtTokenConfig
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="user"></param>
        /// <param name="_config"></param>
        /// <param name="userRoles"></param>
        /// <returns></returns>
        public static string GetToken(User user, IConfiguration _config,
            IList<string> userRoles)
        {
            //get application user model
            var _user = user;
            var config = _config;
            //Create claim for JWT
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //Create jwt secret key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));

            //Generate signin creadentials
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //Create security token descriptor
            var securityTokenDescribe = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            //build token
            var handleToken = new JwtSecurityTokenHandler();

            SecurityToken token = handleToken.CreateToken(securityTokenDescribe);

            return handleToken.WriteToken(token);
        }
    }
}