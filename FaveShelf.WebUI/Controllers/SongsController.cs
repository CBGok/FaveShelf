using FaveShelf.Business.Services;
using FaveShelf.Data.Entities;
using FaveShelf.WebUI.Models;
using FaveShelf.WebUI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FaveShelf.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongsController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SpotifyService _spotifyService;
        private readonly ISongService _songService;

        public SongsController(IHttpClientFactory httpClientFactory, SpotifyService spotifyService, ISongService songService)
        {
            _httpClientFactory = httpClientFactory;
            _spotifyService = spotifyService;
            _songService = songService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTopSongs()
        {
            var songs = await _songService.GetTopSongs(); // Burada veritabanındaki şarkıları çekiyoruz
            var songDtos = songs.Select(song => new
            {
                Id = song.Id, // Veritabanındaki Id
                Name = song.Name,
                Artist = song.Artist,
                Url = song.Url,
                ImageUrl = song.ImageUrl
            }).ToList();

            return Ok(songDtos);
        }

        [HttpGet("load-songs")]
        public async Task<IActionResult> LoadTopSongs()
        {
            var accessToken = await _spotifyService.GetAccessToken();
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetStringAsync("https://api.spotify.com/v1/playlists/2YRe7HRKNRvXdJBp9nXFza/tracks");
            var spotifyResponse = JsonSerializer.Deserialize<SpotifyTracksResponse>(response);

            var songs = spotifyResponse.Items.Select(item => new SongEntity
            {
                Name = item.Track.Name,
                Artist = string.Join(", ", item.Track.Artists.Select(a => a.Name)),
                Url = item.Track.ExternalUrls.Spotify,
                ImageUrl = item.Track.Album.Images.FirstOrDefault()?.Url
            }).ToList();

            // Şarkıları veritabanına kaydet
            foreach (var song in songs)
            {
                await _songService.AddSong(song);
            }

            return Ok("Songs loaded and saved.");
        }

    }
    public class SpotifyTracksResponse
    {
        [JsonPropertyName("items")]
        public List<SpotifyTrackItem> Items { get; set; }
    }

    public class SpotifyTrackItem
    {
        [JsonPropertyName("track")]
        public SpotifyTrack Track { get; set; }
    }

    public class SpotifyTrack
    {
        [JsonPropertyName("id")] // Spotify şarkı ID'si
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("artists")]
        public List<SpotifyArtist> Artists { get; set; }

        [JsonPropertyName("external_urls")]
        public SpotifyExternalUrls ExternalUrls { get; set; }

        [JsonPropertyName("album")]
        public SpotifyAlbum Album { get; set; }
    }

    public class SpotifyArtist
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class SpotifyExternalUrls
    {
        [JsonPropertyName("spotify")]
        public string Spotify { get; set; }
    }

    public class SpotifyAlbum
    {
        [JsonPropertyName("images")]
        public List<SpotifyImage> Images { get; set; }
    }

    public class SpotifyImage
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
