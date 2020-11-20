using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
namespace OnlineFutbol
{
    public static class StringCipher
    {
        private const int value = 89 * 787 * 491;
        private const string caracters = "TB9P5D63EWG";
        public static string Encrypt(int id)
        {
            var values  = (value * id).ToString();          
            return values;
        }

        public static int Decrypt(string cipherText)
        {
            if(cipherText == "0")
                return 0;         

            var id = Convert.ToInt64(cipherText);
            return Convert.ToInt32(id / value);
        }

    }

    public static class TimerZone
    {
        
    }
}