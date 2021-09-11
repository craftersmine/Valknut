using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Launcher.Bootstrap
{
    public static class Sha256FileHash
    {
        public static byte[] CalculateFileHash(string filepath)
        {
            using (var sha256 = SHA256.Create())
            {
                using (var fileStream = File.OpenRead(filepath))
                {
                    var hash = sha256.ComputeHash(fileStream);
                    return hash;
                }
            }
        }

        public static string HashBytesToString(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
}
