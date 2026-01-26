using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace StarNet.JWTManager
{
    public static class TokenManager
    {
        /// <summary>
        /// Create Token
        /// </summary>
        /// <param name="userInformation"></param>
        /// <returns></returns>
        public static string CreateToken(TokenInfo tokenInformation)
        {
            string userID = tokenInformation.UserId.ToString();

            var claims = new List<Claim>();
            claims.Add(CreateClaim("UserID", userID));

            var tokenHandler = new JwtSecurityTokenHandler();

            DateTime now = DateTime.Now; //.UtcNow.AddHours(TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow).Hours);

            var symmetricKey = GetBytes(tokenInformation.SecurityKey);

            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(symmetricKey);


            var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new ClaimsIdentity(claims)),
                Issuer = tokenInformation.TokenIssuerName,
                Audience = tokenInformation.Address,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;

        }
        /// <summary>
        /// Validate Token
        /// </summary>
        /// <param name="tokenString"></param>
        /// <returns></returns>
        public static ClaimsPrincipal ValidateToken(string tokenString, TokenInfo tokenInfo)
        {
            var symmetricKey = GetBytes(tokenInfo.SecurityKey);

            var validationParameters = new TokenValidationParameters()
            {
                ValidIssuer = tokenInfo.TokenIssuerName,
                ValidAudience = tokenInfo.Address,
                ValidateLifetime = true,
                ValidateIssuer = true,
                RequireExpirationTime = true,
                IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(symmetricKey)

            };

            Microsoft.IdentityModel.Tokens.SecurityToken token = new JwtSecurityToken();

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal principal = tokenHandler.ValidateToken(tokenString, validationParameters, out token);

                return principal;
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
            }

            return null;

        }

        /// <summary>
        /// Create Claim
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value);
        }

        /// <summary>
        /// Get Bytes
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static byte[] GetBytes(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;

        }

        /// <summary>
        /// Get UserID
        /// </summary>
        /// <param name="tokenString"></param>
        /// <returns></returns>
        public static int GetUserID(string tokenString, TokenInfo tokenInfo)
        {
            ClaimsPrincipal principal = TokenManager.ValidateToken(tokenString, tokenInfo);

            var claim = principal.Claims.Where(p => p.Type == "UserID").SingleOrDefault();

            if (claim == null)
            {
                return 0;
            }

            int userID = Convert.ToInt32(claim.Value);

            return userID;

        }


        public static TokenInfo CreateStarnetTokenInfo()
        {
            var info = new TokenInfo();
            info.TokenIssuerName = "KBIS";
            info.Address = "http://www.kbis.co.il";
            info.SecurityKey = ConfigurationManager.AppSettings["JWT:Token"];

            return info;
        }
    }
}
