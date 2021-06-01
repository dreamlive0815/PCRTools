using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Core.Common
{
    public class SimpleEncryptor
    {

        private static SimpleEncryptor instance;

        public static SimpleEncryptor Default
        {
            get
            {
                if (instance == null)
                    instance = new SimpleEncryptor("KoishiKo", "MadokaHo");
                return instance;
            }
        }

        private string key;

        private string iv;

        public SimpleEncryptor(string key, string iv)
        {
            this.key = key;
            this.iv = iv;
        }

        public string Encrypt(string rawText)
        {
            using (var provider = new DESCryptoServiceProvider() { Key = Encoding.UTF8.GetBytes(key), IV = Encoding.UTF8.GetBytes(iv) })
            {
                using (var encryptor = provider.CreateEncryptor())
                {
                    var bytes = Encoding.UTF8.GetBytes(rawText);
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            cs.Write(bytes, 0, bytes.Length);
                            cs.FlushFinalBlock();
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        public string Decrypt(string encryptedText)
        {
            using (var provider = new DESCryptoServiceProvider(){ Key = Encoding.UTF8.GetBytes(key), IV = Encoding.UTF8.GetBytes(iv) })
            {
                using (var decryptor = provider.CreateDecryptor())
                {
                    var bytes = Convert.FromBase64String(encryptedText);
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                        {
                            cs.Write(bytes, 0, bytes.Length);
                            cs.FlushFinalBlock();
                        }
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
        }
    }
}
