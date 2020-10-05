using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMailSender.Interfaces
{
    public interface IEncryptorService
    {
        string Encryptor(string str, string Password);

        string Decrypt(string str, string Password);

        byte[] Encrypt(byte[] data, string Password);

        byte[] Decrypt(byte[] data, string Password);
    }
}
