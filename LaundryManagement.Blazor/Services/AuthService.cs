using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LaundryManagement.Blazor.Services;

public class AuthService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public string? Token { get; private set; }

    public bool IsLoggedIn => !string.IsNullOrEmpty(Token);

    public AuthService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        var client = _httpClientFactory.CreateClient("LaundryApi");

        var response = await client.PostAsJsonAsync("api/auth/login", new
        {
            username,
            password
        });

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

        Token = result?.Token;

        return !string.IsNullOrEmpty(Token);
    }

    public HttpClient CreateAuthenticatedClient()
    {
        var client = _httpClientFactory.CreateClient("LaundryApi");

        if (!string.IsNullOrEmpty(Token))
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Token);
        }

        return client;
    }

    private class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}