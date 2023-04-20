using System.Text.Json.Serialization;
namespace DJPPDL.Models.SpotifyModels;

public class SpotifyAuthResponse
{
    [JsonPropertyName("access_token")]
    public string? accessToken { get; set; }

    [JsonPropertyName("token_type")]
    public string? tokenType { get; set; }

    [JsonPropertyName("expires_in")]
    public int expiresIn { get; set; }
}