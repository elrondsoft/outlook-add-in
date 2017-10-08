namespace Helios.Api.Utils.Encryption
{
    public interface IStringEncryptor
    {
        string EncryptString(string text, string keyString);
        string DecryptString(string cipherText, string keyString);
    }
}
