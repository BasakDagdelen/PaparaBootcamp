namespace Patikadev_RestfulApi.Interfaces;

public interface IAuthService
{
    bool ValidateApiKey(string apiKey);
    string GenerateApiKey(string username, string password);
}
