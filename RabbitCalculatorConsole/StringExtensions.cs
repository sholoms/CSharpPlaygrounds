﻿using System.Security.Cryptography;
using System.Text;

namespace Emn.DataAccess;

public static class StringExtensions
{
    public static string Encrypt(this string value)
    {
        var encryptionKey = GetEncryptionKey();
        byte[] clearBytes = Encoding.Unicode.GetBytes(value);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                value = Convert.ToBase64String(ms.ToArray());
            }
        }
        return value;
    }
    public static string Decrypt(this string value)
    {
        var encryptionKey = GetEncryptionKey();
        value = value.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(value);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                value = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return value;
    }

    private static string GetEncryptionKey()
    {
        string encryptionKey = Environment.GetEnvironmentVariable("EncryptionKey");
        if (string.IsNullOrEmpty(encryptionKey))
        {
            string errorMessage = "Encryption key are empty or not exist";
            throw new ArgumentException(errorMessage);
        }
        return encryptionKey;
    }
}
