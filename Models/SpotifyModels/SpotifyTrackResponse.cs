using System.Text.Json.Serialization;
namespace DJPPDL.Models.SpotifyModels;

public class SpotifyTrackResponse
{

    [JsonPropertyName("album")]
    public SpotifyAlbum? album { get; set; }

    [JsonPropertyName("artists")]
    public IList<SpotifyArtist>? artists { get; set; }

    [JsonPropertyName("duration_ms")]
    public int? trackDurationMS { get; set; }

    [JsonPropertyName("explicit")]
    public bool? isExplicitTrack { get; set; }

    [JsonPropertyName("id")]
    public string? id { get; set; }

    [JsonPropertyName("name")]
    public string? trackName { get; set; }


}