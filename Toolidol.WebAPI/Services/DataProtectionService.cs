using Microsoft.AspNetCore.DataProtection;
using Toolidol.WebAPI.Interfaces;

namespace Toolidol.WebAPI.Services
{
    public class DataProtectionService : IDataProtectionService
    {
        private readonly IDataProtector _protector;

        private const string PurposeString = "Facebook.Page.AccessToken";

        public DataProtectionService(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector(PurposeString);
        }

        public string Encrypt(string plainText) => _protector.Protect(plainText);
        public string Decrypt(string cipherText) => _protector.Unprotect(cipherText);
    }
}
