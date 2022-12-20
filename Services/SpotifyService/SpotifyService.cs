using System;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using DJPPDL.Options;

namespace DJPPDL.Services;

public class SpotifyService : ISpotifyService
{

    private readonly HttpClient _httpClient;
    private readonly SpotifyOptions? _spotifyOptions;
    private String? _spotifyAccessToken;
    public SpotifyService(HttpClient httpClient, IOptionsMonitor<ServiceOptions> optionsMonitor)
    {
        _spotifyOptions = optionsMonitor.CurrentValue?.SpotifyOptions;
        _httpClient = httpClient;
        _spotifyAccessToken = GetSpotifyToken();
    }

    private String GetSpotifyToken()
    {
        string result = "XDXDXD";

        var tokenRequestContent = new List<KeyValuePair<string, string>>();
        tokenRequestContent.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

        string authenticationString = $"{_spotifyOptions?.clientID}:{_spotifyOptions?.clientSecret}";
        string base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));


        HttpRequestMessage getSpotifyAccessToken = new HttpRequestMessage(HttpMethod.Post, _spotifyOptions?.host + _spotifyOptions?.getToken)
        { Content = new FormUrlEncodedContent(tokenRequestContent) };
        getSpotifyAccessToken.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);

        try
        {
            HttpResponseMessage getTokenResponse = _httpClient.Send(getSpotifyAccessToken);
            Console.Write("");

        }
        catch (HttpRequestException ex)
        {
            Console.Write(ex.Message);
        }

        return result;
    }

    public async Task<bool> GetSpotifyTrack()
    {
        bool result = true;




        return result;
    }

}