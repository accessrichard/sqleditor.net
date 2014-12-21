namespace SqlEditor.Tests.Models.Cryptography
{
    using System.Security.Cryptography;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SqlEditor.Models.Cryptography;

    [TestClass]
    public class CryptographyTest
    {
        [TestMethod]
        public void Encryption()
        {
            const string Original = "Here is some data to encrypt!";
            using (var aes = new AesCryptoServiceProvider())
            {
                var encrypted = Crypto.Encrypt(Original, aes.Key);
                var roundtrip = Crypto.Decrypt(encrypted, aes.Key);
                Assert.AreEqual(Original, roundtrip);
            }
        }
    }
}
