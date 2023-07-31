using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helper
{
    public static class EncriptHelper
    {
        public static string GetSHA384(this string password) 
        {
            SHA384 sha384 = SHA384Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = null;
            StringBuilder sb = new StringBuilder();
            data = sha384.ComputeHash(encoding.GetBytes(password));
            for (int i = 0; i < data.Length; i++) sb.AppendFormat("{0:x2}", data[i]);
            return sb.ToString();
        }
    }
}
