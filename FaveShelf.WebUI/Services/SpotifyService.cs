using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace FaveShelf.WebUI.Services
{
    public class SpotifyService
    {
        private readonly HttpClient _httpClient;

        public SpotifyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAccessToken()
        {
            var clientId = "b5237529e3fb42d6947a28b6486f3094";
            var clientSecret = "e4809f2d01e44711853436391b7552f0";
            var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

            var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authHeader);
            request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Request to Spotify API failed. Status Code: {response.StatusCode}, Response: {errorContent}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<SpotifyTokenResponse>(responseContent);

            return tokenResponse.AccessToken;
        }

        public class SpotifyTokenResponse
        {
            [JsonPropertyName("access_token")]
            public string AccessToken { get; set; }
        }

    }
}
