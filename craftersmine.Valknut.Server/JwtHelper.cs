using craftersmine.Valknut.Server.Database;

using Microsoft.IdentityModel.Tokens;

using Swan.Logging;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Server
{
    public static class JwtHelper
    {
        public static string GenerateJwtToken(string username, string uuid, string sessionId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(Program.Config.SecurityConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("userId", uuid), new Claim("username", username), new Claim("sessionId", sessionId) }),
                Expires = DateTime.Now.AddMonths(1),
                IssuedAt = DateTime.Now,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public static string ValidateJwtToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(Program.Config.SecurityConfig.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var uuid = jwtToken.Claims.First(x => x.Type == "userId").Value;
                var sessionId = jwtToken.Claims.First(x => x.Type == "sessionId").Value;
                var userSession = SessionsTableHelper.GetUserSession(uuid);
                if (userSession.SessionId != sessionId)
                    return null;
                return uuid;
            }
            catch (Exception ex)
            {
                Logger.Warn(ex, "JwtHelper.ValidateJwtToken(string)", "Unable to validate JWT token!");
                return null;
            }
        }
    }
}
