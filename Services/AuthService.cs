using Patikadev_RestfulApi.Domain;
using Patikadev_RestfulApi.Interfaces;

namespace Patikadev_RestfulApi.Services;

public class AuthService : IAuthService
{
    // Fake kullanıcı listesi
    private readonly List<User> _users = new()
        {
            new User { Username = "admin", Password = "admin123", },
            new User { Username = "user", Password = "user123" }
        };

    private readonly Dictionary<string, string> _apiKeys = new();

    public bool ValidateApiKey(string apiKey)
    {
        return _apiKeys.ContainsKey(apiKey);
    }

    public string GenerateApiKey(string username, string password)
    {
        var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);
        if (user is null)
            return null;

        // API Key oluşturma
        var apiKey = Guid.NewGuid().ToString();
        _apiKeys[apiKey] = username;

        return apiKey;
    }
}