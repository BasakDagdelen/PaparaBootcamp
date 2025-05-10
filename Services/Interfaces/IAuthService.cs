namespace Patikadev_RestfulApi.Services.Interfaces;

public interface IAuthService
{
    bool ValidateApiKey(string apiKey);
    string GenerateApiKey(string username, string password);
}
