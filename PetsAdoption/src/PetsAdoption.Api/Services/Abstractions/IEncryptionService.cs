namespace PetsAdoption.Api.Services.Abstractions;

public interface IEncryptionService
{
    public string Encrypt(string value);
}
