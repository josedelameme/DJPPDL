using System.Text.Json.Serialization;
namespace DJPPDL.Models.SpotifyModels;

public class SpotifyArtist
{
    [JsonPropertyName("id")]
    public string? id { get; set; }

    [JsonPropertyName("name")]
    public string? name { get; set; }

}