using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using PetsAdoption.Api.Services.Abstractions;
using PetsAdoption.Api.Settings;
using System.Security.Cryptography;
using System.Text;

namespace PetsAdoption.Api.Services;

public class EncryptionService : IEncryptionService
{
    private readonly byte[] _key;
    private readonly byte[] _iv;
    public EncryptionService(IOptions<EncryptionSettings> encryptionSettings)
    {
        _key = Encoding.ASCII.GetBytes(encryptionSettings.Value.Key);
        _iv = Encoding.ASCII.GetBytes(encryptionSettings.Value.IV);
    }

    public string Encrypt(string value)
    {
        byte[] encrypted;

        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = _key;
            aesAlg.IV = _iv;

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(value);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }
        return Encoding.UTF8.GetString(encrypted);

    }
}
