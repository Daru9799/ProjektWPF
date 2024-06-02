using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektWPF.ViewModels.Converters
{
    //Klasa odpowiedzialna za szyfrowanie i deszyfrowanie haseł przy pomocy BCrypt (dlugosc hasha 60 znaków)
    public static class PasswordEncryptionAndDecryption
    {
        // Hashowanie hasła
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Weryfikacja hasła
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
