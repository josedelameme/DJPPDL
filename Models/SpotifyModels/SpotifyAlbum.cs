using System.Text.Json.Serialization;
namespace DJPPDL.Models.SpotifyModels;

public class SpotifyAlbum
{
    [JsonPropertyName("album_type")]
    public string? albumType { get; set; }

    [JsonPropertyName("id")]
    public string? id { get; set; }

    [JsonPropertyName("images")]
    public IList<SpotifyImage>? images { get; set; }

    [JsonPropertyName("name")]
    public string? albumName { get; set; }
}