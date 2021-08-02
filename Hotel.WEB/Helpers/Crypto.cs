using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Hotel.WEB.Helpers
{
    public class Crypto
    {
        public static string Hash(string str)
        {
            MD5 md = new MD5Cng();
            return BitConverter.ToString(md.ComputeHash(Encoding.UTF8.GetBytes(str + "some salt")));
        }
    }
}