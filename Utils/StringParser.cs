using DJPPDL.Models.ServiceModels;

namespace DJPPDL.Utils;
public class StringParser : IStringParser
{
    public const string YOUTUBE_PLAYLIST = "YoutubePlaylist";
    public const string YOUTUBE_SONG = "YoutubeSong";
    public UrlParserResult ParseUrl(string url)
    {
        string? service = null;
        string? id = null;

        if (url.Contains("youtube") || url.Contains("youtu"))
        {
            if (url.Contains("list"))
            {
                service = YOUTUBE_PLAYLIST;
                id = url;
            }
            else
            {
                service = YOUTUBE_SONG;
                id = url;
            }

        }

        if (url.Contains("spotify"))
        {
            service = "Spotify";
            id = url;
            if (id.Contains("track/"))
            {
                id = id.Split("track/", 32)[1];
            }
            if (id.Contains("?si="))
            {
                id = id.Split("?si=", 32)[0];
            }

        }

        return new UrlParserResult
        {
            service = service,
            output = id
        };
    }

}

