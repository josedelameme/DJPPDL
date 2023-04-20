using System.Text.Json.Serialization;
namespace DJPPDL.Models.SpotifyModels;

public class SpotifyImage
{

    [JsonPropertyName("url")]
    public string? url { get; set; }

    [JsonPropertyName("height")]
    public int? height { get; set; }

    [JsonPropertyName("width")]
    public int? width { get; set; }
}