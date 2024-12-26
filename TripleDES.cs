using System.Security.Cryptography;
using System.Text;

namespace PasswordManager
{
    public class TripleDES
    {
        /// <summary>
        /// เข้ารหัสข้อมูลด้วย AES
        /// </summary>
        /// <param name="key">Key สำหรับการเข้ารหัส</param>
        /// <param name="plainData">ข้อความที่ต้องการเข้ารหัส</param>
        /// <returns>ข้อมูลที่ถูกเข้ารหัส (Base64)</returns>
        public string EncryptData(string key, string plaindata)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(key));
                aesAlg.Mode = CipherMode.CFB;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plaindata);
                            return Convert.ToBase64String(aesAlg.IV.Concat(msEncrypt.ToArray()).ToArray());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ถอดรหัสข้อมูลที่ถูกเข้ารหัสด้วย AES
        /// </summary>
        /// <param name="key">Key สำหรับการถอดรหัส</param>
        /// <param name="encryptedData">ข้อมูลที่ถูกเข้ารหัส (Base64)</param>
        /// <returns>ข้อความที่ถูกถอดรหัส</returns>
        public string Decrypt(string key, string encryptedData)
        {
            try
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedData);

                using (Aes aesAlg = Aes.Create())
                {
                    byte[] keyBytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(key));
                    aesAlg.Key = keyBytes;
                    byte[] iv = new byte[aesAlg.IV.Length];
                    Array.Copy(encryptedBytes, iv, iv.Length);
                    aesAlg.IV = iv;
                    aesAlg.Mode = CipherMode.CFB;
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream())
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                        {
                            csDecrypt.Write(encryptedBytes, aesAlg.IV.Length, encryptedBytes.Length - aesAlg.IV.Length);
                        }

                        return Encoding.UTF8.GetString(msDecrypt.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
