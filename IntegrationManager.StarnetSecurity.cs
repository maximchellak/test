using StarNet.Common;
using StarNet.Services.Client.StarNetAPI.WCF.StarnetSecurity;
using System;

namespace StarNet.Services.Client
{
    public static partial class IntegrationManager
    {
        public static string GeneratePassword()
        {
            using (var client = new StarnetSecurityClient())
            {
                return client.GeneratePassword();
            }
        }

        public static string SpecialPasswordCharacters()
        {
            using (var client = new StarnetSecurityClient())
            {
                return client.SpecialPasswordCharacters();
            }
        }

        public static ChangePasswordResponse ChangePassword(
            int userId,
            string oldPassword,
            string newPassword)
        {
            using (var client = new StarnetSecurityClient())
            {
                var encryptedOldPassword = Cryptography.Cryptography
                    .EncryptPlainTextToCipherText(oldPassword.Trim());    
                var encryptedNewPassword = Cryptography.Cryptography
                    .EncryptPlainTextToCipherText(newPassword.Trim());    

                return client.ChangePassword(
                    userId, 
                    encryptedOldPassword,
                    encryptedNewPassword,
                    true);
            }
        }

        public static Common.PublicEnums.AuthenticationMethod GetAuthenticationMethod()
        {
            using (var client = new StarnetSecurityClient())
            {
                return client.GetAuthenticationMethod();
            }
        }

        public static PublicEnums.AuthenticationResultsEnum AuthenticateUser(
            string userName,
            string password)
        {
            using (var client = new StarnetSecurityClient())
            {
                var encryptedPassword = Cryptography.Cryptography
                    .EncryptPlainTextToCipherText(password.Trim());

                return client.AuthenticateUser(
                    userName,
                    encryptedPassword,
                    true);
            }
        }

        public static int GetStarNetUserId(string fullUserName)
        {
            using (var client = new StarnetSecurityClient())
            {
                return client.GetStarNetUserId(fullUserName);
            }
        }

        public static int? GetDaysUntilPasswordExpires(int userId)
        {
            using (var client = new StarnetSecurityClient())
            {
                return client.GetDaysUntilPasswordExpires(userId);
            }
        }

        public static bool ChangePasswordAtNextLogon(int userId)
        {
            using (var client = new StarnetSecurityClient())
            {
                return client.ChangePasswordAtNextLogon(userId);
            }
        }

        public static bool IsPasswordComplexityEnabled()
        {
            using (var client = new StarnetSecurityClient())
            {
                return client.IsPasswordComplexityEnabled();
            }
        }

        public static int GetPasswordHistory()
        {
            using (var client = new StarnetSecurityClient())
            {
                return client.GetPasswordHistory();
            }
        }

        public static int GetMinimumPasswordLength()
        {
            using (var client = new StarnetSecurityClient())
            {
                return client.GetMinimumPasswordLength();
            }
        }

        public static int GetMinimumPasswordAge()
        {
            using (var client = new StarnetSecurityClient())
            {
                return client.GetMinimumPasswordAge();
            }
        }

        public static DateTime? GetPasswordLastSet(int userId)
        {
            using (var client = new StarnetSecurityClient())
            {
                return client.GetPasswordLastSet(userId);
            }
        }

    }

}
