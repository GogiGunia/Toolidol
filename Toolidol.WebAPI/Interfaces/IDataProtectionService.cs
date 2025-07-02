namespace Toolidol.WebAPI.Interfaces
{
    public interface IDataProtectionService
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}
