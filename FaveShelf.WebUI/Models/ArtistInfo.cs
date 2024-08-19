using System.Text.Json.Serialization;

namespace FaveShelf.WebUI.Models
{
    public class ArtistInfoResponse
    {
        [JsonPropertyName("artist")]
        public ArtistInfo Artist { get; set; }
    }

    public class ArtistInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("image")]
        public List<Image> Images { get; set; }
    }

    public class Image
    {
        [JsonPropertyName("#text")]
        public string Url { get; set; }

        [JsonPropertyName("size")]
        public string Size { get; set; }
    }
}
