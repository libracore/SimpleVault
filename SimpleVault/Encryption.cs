using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimpleVault
{
    public static class Encryption
    {
        ///<summary>
        /// Encrypt a file using Rijndael algorithm.
        ///</summary>
        /// <param name="content">Text to encrypt</param>
        /// <param name="password">Password</param>
        /// <param name="file">Target file name</param>
        /// <returns>True on success, false on failure</returns>
        public static bool Encrypt(string content, string password, string file)
        {
            bool success = true;
            try
            {
                UnicodeEncoding uniEnc = new UnicodeEncoding();
                KeyGen key = new KeyGen(password);

                string cryptFile = file;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key.Key, key.IV),
                    CryptoStreamMode.Write);

                byte[] contentBytes = uniEnc.GetBytes(content);

                foreach (byte b in contentBytes)
                {
                    cs.WriteByte(b);
                }

                cs.Close();
                fsCrypt.Close();
            }
            catch
            {
                success = false;
            }
            return success;
        }


        ///<summary>
        /// Decrypt a file using Rijndael algorithm.
        ///</summary>
        /// <param name="file">Encrypted source file</param>
        /// <param name="password">Password</param>
        /// <returns>Decoded content string</returns>
        public static string Decrypt(string file, string password)
        {
            string content = string.Empty;
            try
            {
                UnicodeEncoding uniEnc = new UnicodeEncoding();
                KeyGen key = new KeyGen(password);

                FileStream fsCrypt = new FileStream(file, FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key.Key, key.IV),
                    CryptoStreamMode.Read);

                int data;
                List<byte> contentBytes = new List<byte>();
                while ((data = cs.ReadByte()) != -1)
                {
                    contentBytes.Add((byte)data); 
                }
                content += uniEnc.GetString(contentBytes.ToArray());

                cs.Close();
                fsCrypt.Close();

            }
            catch
            {
                content = string.Empty;
            }
            return content;
        }
    }
}
