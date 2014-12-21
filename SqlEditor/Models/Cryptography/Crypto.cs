namespace SqlEditor.Models.Cryptography
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    /// <summary>
    /// Encryption using AES CBC. 
    /// </summary>
    public class Crypto
    {
        /// <summary>
        /// Encrypts a string using AES CBC.
        /// The initialization vector used to encrypt the string
        /// is prepended to the encrypted text. That is:
        /// Encrypted text = [IV + Encrypted Text].
        /// </summary>
        /// <param name="plainText">The text to encrypt.</param>
        /// <param name="key">The key to use to encrypt the text. </param>
        /// <returns>The encrypted text.</returns>
        public static byte[] Encrypt(string plainText, byte[] key)
        {
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }

            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            byte[] encrypted;
            using (var aesProvider = new AesCryptoServiceProvider())
            {
                using (var memoryStream = new MemoryStream())
                {
                    var encryptor = aesProvider.CreateEncryptor(key, aesProvider.IV);
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (var streamWriter = new StreamWriter(cryptoStream))
                        {
                            memoryStream.Write(aesProvider.IV, 0, aesProvider.BlockSize / 8);
                            streamWriter.Write(plainText);
                        }

                        encrypted = memoryStream.ToArray();
                    }
                }
            }

            return encrypted;
        }

        /// <summary>
        /// Decrypts the cipher text.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <param name="key">The encryption key.</param>
        /// <returns>The decrypted string.</returns>
        public static string Decrypt(byte[] cipherText, byte[] key)
        {
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }

            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            string plaintext;

            using (var aesProvider = new AesCryptoServiceProvider())
            {
                aesProvider.Key = key;
                aesProvider.IV = GetIv(cipherText, aesProvider);
                var decryptor = aesProvider.CreateDecryptor(aesProvider.Key, aesProvider.IV);
                
                using (var memoryStream = new MemoryStream(GetDataWithoutIv(cipherText, aesProvider)))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (var streamReader = new StreamReader(cryptoStream))
                        {
                            plaintext = streamReader.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        /// <summary>
        /// Gets the initialization vector which is stored in the first 16 
        /// bytes (block size / 8) of the cipher text.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <param name="provider">The crypto provider.</param>
        /// <returns>The IV portion of the cipher text.</returns>
        private static byte[] GetIv(byte[] cipherText, AesCryptoServiceProvider provider)
        {
            var iv = new byte[provider.BlockSize / 8];
            Buffer.BlockCopy(cipherText, 0, iv, 0, iv.Length);
            return iv;
        }

        /// <summary>
        /// Gets the cipher text without the initialization vector.
        /// </summary>
        /// <param name="cipherText">The cipher text of format [IV + CIPHER TEXT]</param>
        /// <param name="provider">The crypto provider</param>
        /// <returns>The cipher text.</returns>
        private static byte[] GetDataWithoutIv(byte[] cipherText, AesCryptoServiceProvider provider)
        {
            var vectorSize = provider.BlockSize / 8;
            var data = new byte[cipherText.Length - vectorSize];
            Buffer.BlockCopy(cipherText, vectorSize, data, 0, cipherText.Length - vectorSize);
            return data;
        }
    }
}