namespace PetsAdoption.Api.Settings;

public class EncryptionSettings
{
    public string Key { get; set; } = string.Empty;
    public string IV { get; set; } = string.Empty;
}
