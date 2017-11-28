using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimpleVault
{
    /// <summary>
    /// This class creates a save key from a password
    /// </summary>
    public class KeyGen
    {
        #region variables
        private byte[] key;
        private byte[] iv;

        #endregion

        #region constructor
        /// <summary>
        /// Creates a new key pair
        /// </summary>
        /// <param name="password">Password of arbitrary length</param>
        public KeyGen(string password)
        {
            string keySalt = "45zsxfHXY3HnD8mk"; // an arbitrary constant string to randomize the hash
            string ivSalt = "JBYgyePNEAfF4mxG";  // another arbitrary constant string to generate a different hash
            key = SHA256.Create().ComputeHash(Encoding.Unicode.GetBytes(keySalt + password));
            iv = SHA256.Create().ComputeHash(Encoding.Unicode.GetBytes(ivSalt + password)).Take(16).ToArray();
        }
        #endregion

        #region variable access
        public byte[] Key
        {
            get { return key; }
        }

        public byte[] IV
        {
            get { return iv; }
        }
        #endregion


    }
}
