using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Services
{
    public static class PasswordService
    {
        public static void AddPassword(UserData user, PasswordEntry entry, string userPassword)
        {
            entry.Password = EncryptionHelper.Encrypt(entry.Password, userPassword);
            user.PasswordSites.Add(entry);
        }

        public static void UpdatePassword(UserData user, PasswordEntry entry, string userPassword)
        {
            var existing = user.PasswordSites.FirstOrDefault(p => p.Site == entry.Site && p.Username == entry.Username);
            if (existing != null)
            {
                existing.Password = EncryptionHelper.Encrypt(entry.Password, userPassword);
            }
        }

        public static void DeletePassword(UserData user, PasswordEntry entry)
        {
            user.PasswordSites.Remove(entry);
        }

        public static string RecoverPassword(UserData user, PasswordEntry entry, string userPassword)
        {
            return EncryptionHelper.Decrypt(entry.Password, userPassword);
        }
    
}
}
