using LibMailSender.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibMailSender.Service
{
    public class Rfc2898Encryptor : IEncryptorService
    {
        private static readonly byte[] SALT =
            {
            0x26,0xdc,0xff,0x00,
            0xad,0xed,0x7a,0xee,
            0xc5,0xfe,0x07,0xaf,
            0x4d,0x08,0x22,0x3c
            };

        public Encoding Encoding { get; set; } = Encoding.UTF8;

        private static ICryptoTransform GetAlgorythm(string password)
        {
            var pdb = new Rfc2898DeriveBytes(password, SALT);
            var algorythm = Rijndael.Create();
            algorythm.Key = pdb.GetBytes(32);
            algorythm.IV = pdb.GetBytes(16);
            return algorythm.CreateEncryptor();
        }

        private static ICryptoTransform GetInverseAlgorythm(string password)
        {
            var pdb = new Rfc2898DeriveBytes(password, SALT);
            var algorythm = Rijndael.Create();
            algorythm.Key = pdb.GetBytes(32);
            algorythm.IV = pdb.GetBytes(16);
            return algorythm.CreateDecryptor();
        }

        public string Decrypt(string str, string Password)
        {
            var encoding = Encoding ?? Encoding.UTF8;
            var crypted_bytes = Convert.FromBase64String(str);
            var bytes = Decrypt(crypted_bytes, Password);
            return encoding.GetString(bytes);
        }

        public byte[] Decrypt(byte[] data, string Password)
        {
            var algorythm = GetInverseAlgorythm(Password);
            using (var stream = new MemoryStream())
            using (var crypto_stream = new CryptoStream(stream, algorythm, CryptoStreamMode.Write))
            {
                crypto_stream.Write(data, 0, data.Length);
                crypto_stream.FlushFinalBlock();
                return stream.ToArray();
            }
        }

        public byte[] Encrypt(byte[] data, string Password)
        {
            var algorythm = GetAlgorythm(Password);
            using (var stream = new MemoryStream())
            using (var crypto_stream = new CryptoStream(stream, algorythm, CryptoStreamMode.Write))
            {
                crypto_stream.Write(data, 0, data.Length);
                crypto_stream.FlushFinalBlock();
                return stream.ToArray();
            }
        }

        public string Encryptor(string str, string Password)
        {
            var encoding = Encoding ?? Encoding.UTF8;
            var bytes = encoding.GetBytes(str);
            var crypted_bytes = Encrypt(bytes, Password);
            return Convert.ToBase64String(crypted_bytes);
        }
    }
}
