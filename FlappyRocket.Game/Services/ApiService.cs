using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

public class ApiService
{
    private readonly HttpClient _client;

    public ApiService(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri("http://localhost:5225/");
    }

    public async Task<bool> RegisterAsync(string username , string password)
    {
        var request = new RegisterRequest
        {
            Username = username,
            Password = password
        };

        var response = await _client.PostAsJsonAsync("http://localhost:5225/api/auth/register" , request);

        return response.IsSuccessStatusCode;
    }

    public async Task<string> LoginAsync(string username , string password)
    {
        var request = new LoginRequest
        {
            Username = username,
            Password = password
        };

        var response = await _client.PostAsJsonAsync("http://localhost:5225/api/auth/login" , request);


        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

        return loginResponse?.Token;
    }

    public async Task<List<LeaderboardEntryDto>?>GetLeaderboardsAsync(Difficulty difficulty)
    {
        var response = await _client.GetAsync($"http://localhost:5225/api/leaderboard?difficulty={difficulty}");

        if (!response.IsSuccessStatusCode)
        {
            System.Console.WriteLine($"Leaderboard request failed: {(int)response.StatusCode}{response.StatusCode}");
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<List<LeaderboardEntryDto>>();

        System.Console.WriteLine($"Recived leaderboard entries: {result?.Count}");

        return result;
    }

    public async Task<bool> AddScoreAsync(int score , Difficulty difficulty , string token)
    {
        var request = new AddScoreRequest
        {
          Score = score,
          Difficulty = difficulty  
        };

        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer" , token);

        var response = await _client.PostAsJsonAsync("http://localhost:5225/api/leaderboard" , request);

        return response.IsSuccessStatusCode;
    }


}