using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace BW8AGU.Samples.CSharp.Common
{
    public class PasswordVaultHelper
    {
        private static PasswordVault vault =
            new Windows.Security.Credentials.PasswordVault();

        public static IList<PasswordCredential> LoadPasswordVault()
        {
            //Load all credentials
            IReadOnlyList<PasswordCredential> creds = vault.RetrieveAll();

            return creds.ToList<PasswordCredential>();
        }

        public static bool CheckPassword(string username, string password)
        {
            bool res = false;

            try
            {
                IReadOnlyList<PasswordCredential> creds = vault.FindAllByUserName(username);

                foreach (PasswordCredential cred in creds)
                {
                    cred.RetrievePassword();
                    res = cred.Password == password;
                }
            }
            catch
            {
                //do nothing
            }

            return res;
        }

        public static void SavePasswordToVault(string username, string password)
        {
            LoadPasswordVault();

            PasswordCredential cred = new PasswordCredential("BW8AGU", username, password);

            vault.Add(cred);
        }
    }
}
