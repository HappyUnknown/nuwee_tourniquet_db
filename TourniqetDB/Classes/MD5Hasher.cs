using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourniqetDB.Classes
{
    public static class MD5Hasher
    {
        const string HASH = "X2";
        public static string Encrypt(string input, string hash = HASH)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input);

            var MD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var tripDES = new System.Security.Cryptography.TripleDESCryptoServiceProvider();

            tripDES.Key = MD5.ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(hash));
            tripDES.Mode = System.Security.Cryptography.CipherMode.ECB;

            System.Security.Cryptography.ICryptoTransform transform = tripDES.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(bytes, 0, bytes.Length);

            return Convert.ToBase64String(result);
        }
        /// <summary>
        /// https://www.youtube.com/watch?v=EEItNLDw0-A
        /// </summary>
        /// <param name="output"></param>
        /// <returns>MD5 decryption (possibly)</returns>
        public static string Decrypt(string output, string hash = HASH)
        {
            byte[] bytes = Convert.FromBase64String(output);

            var MD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var tripDES = new System.Security.Cryptography.TripleDESCryptoServiceProvider();

            tripDES.Key = MD5.ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(hash));
            tripDES.Mode = System.Security.Cryptography.CipherMode.ECB;

            System.Security.Cryptography.ICryptoTransform transform = tripDES.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(bytes, 0, bytes.Length);

            return System.Text.UTF8Encoding.UTF8.GetString(result);
        }
        public static string EncryptOneWay(string input, string hash = HASH)
        {
            using (var MD5 = new System.Security.Cryptography.MD5CryptoServiceProvider())
            {
                byte[] b = System.Text.Encoding.UTF8.GetBytes(input);
                b = MD5.ComputeHash(b);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (byte x in b)

                    sb.Append(x.ToString(hash));

                return sb.ToString();
            }
        }
        //public static string Decode(string hash)
        //{
        //    try
        //    {
        //        var encoder = new System.Text.UTF8Encoding();
        //        System.Text.Decoder utf8Decode = encoder.GetDecoder();
        //        byte[] todecodeByte = Convert.FromBase64String(input);
        //        int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
        //        char[] decodedChar = new char[charCount];
        //        utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
        //        string result = new String(decodedChar);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error in base64Decode" + ex.Message);
        //    }
        //}
    }
}
