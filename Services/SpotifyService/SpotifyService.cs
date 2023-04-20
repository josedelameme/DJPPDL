using System.Net.Http.Json;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using DJPPDL.Options;
using DJPPDL.Models.SpotifyModels;
using DJPPDL.Models.ServiceModels;

namespace DJPPDL.Services;

public class SpotifyService : ISpotifyService
{

    private readonly HttpClient _httpClient;
    private readonly SpotifyOptions? _spotifyOptions;
    private string? _spotifyAccessToken;
    public SpotifyService(HttpClient httpClient, IOptionsMonitor<ServiceOptions> optionsMonitor)
    {
        _spotifyOptions = optionsMonitor.CurrentValue?.SpotifyOptions;
        _httpClient = httpClient;
    }

    private async Task GetSpotifyTokenAsync()
    {

        var tokenRequestContent = new List<KeyValuePair<string, string>>();
        tokenRequestContent.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

        string authenticationString = $"{_spotifyOptions?.clientID}:{_spotifyOptions?.clientSecret}";
        string base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));


        HttpRequestMessage getSpotifyAccessToken = new HttpRequestMessage(HttpMethod.Post, _spotifyOptions?.tokenHost + _spotifyOptions?.getToken)
        { Content = new FormUrlEncodedContent(tokenRequestContent) };
        getSpotifyAccessToken.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);

        HttpResponseMessage getTokenResponse = await _httpClient.SendAsync(getSpotifyAccessToken);
        getTokenResponse.EnsureSuccessStatusCode();

        var response = getTokenResponse.Content.ReadFromJsonAsync<SpotifyAuthResponse>();

        _spotifyAccessToken = response.Result?.accessToken;

    }

    public async Task<ServiceResult> GetSpotifyTrack(string trackId)
    {
        ServiceResult result = new ServiceResult { result = true };

        try
        {
            if (_spotifyAccessToken == null)
            {
                await GetSpotifyTokenAsync();
            }

            HttpRequestMessage getSpotifyTrack = new HttpRequestMessage(HttpMethod.Get, _spotifyOptions?.apiHost + _spotifyOptions?.getTrack + trackId);
            getSpotifyTrack.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _spotifyAccessToken);

            HttpResponseMessage getTrackResponse = await _httpClient.SendAsync(getSpotifyTrack);
            getTrackResponse.EnsureSuccessStatusCode();

            var response = getTrackResponse.Content.ReadFromJsonAsync<SpotifyTrackResponse>();

            result.output = response.Result;

        }
        catch (HttpRequestException ex)
        {
            result.result = false;
            result.errorMessage = ex.Message;
        }

        return result;
    }

}