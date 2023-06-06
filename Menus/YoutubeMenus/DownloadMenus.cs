using Microsoft.Extensions.Options;
using DJPPDL.Services;
using DJPPDL.Options;
using DJPPDL.Models.ServiceModels;
using DJPPDL.Models.YTModels;
using DJPPDL.Utils;

namespace DJPPDL.Menus;


public class DownloadMenus : IDownloadMenus
{
    private readonly IYoutubeService _youtubeService;
    private readonly IOptionsMenus _optionsMenus;
    private readonly IStringParser _stringParser;
    private readonly IOptionsMonitor<GeneralOptions> _optionsMonitor;
    private readonly ISpotifyService _spotifyService;
    public DownloadMenus(IYoutubeService youtubeService, IOptionsMenus optionsMenus, IOptionsMonitor<GeneralOptions> optionsMonitor, IStringParser stringParser, ISpotifyService spotifyService)
    {
        _youtubeService = youtubeService;
        _optionsMenus = optionsMenus;
        _stringParser = stringParser;
        _optionsMonitor = optionsMonitor;
        _spotifyService = spotifyService;
    }
    public async Task LinkDownload(bool useDefaults)
    {
        string? format = useDefaults ?
            _optionsMonitor.CurrentValue?.DLFileOptions?[0].defaultValue
            : _optionsMenus.ChooseFromOptionsList(OptionsConstants.DL_FILE_OPTIONS, OptionsConstants.FORMATS);
        string? location = useDefaults ?
            _optionsMonitor.CurrentValue?.DLFileOptions?[1].defaultValue
            : _optionsMenus.ChooseFromOptionsList(OptionsConstants.DL_FILE_OPTIONS, OptionsConstants.LOCATIONS);

        Console.Clear();

        if (format != null && location != null)
        {
            Console.WriteLine("Youtube (video or playlist) or Spotify (song or songs) URL:\n");
            string? uri = Console.ReadLine();
            Console.Clear();

            ServiceResult serviceResult = await SelectServiceAndDownload(uri, location, format);

            Console.Clear();

            if (serviceResult.result == true)
            {
                Console.WriteLine("Successful Download!");
            }
            else
            {
                Console.WriteLine("Failed to Download!");
            }
            Console.WriteLine("\n\nPress any key to continue...\n");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("\n\nError on retrieving Configuration values...\n");
        }
    }

    public async Task SearchAndDownload(bool useDefaults)
    {
        Console.WriteLine("Youtube search:\n");
        string? searchQuery = Console.ReadLine();
        Console.Clear();
        bool result = false;
        if (searchQuery == "")
        {
            SearchResults searchResults = await _youtubeService.SearchVideo(searchQuery);

            YTVideoDTO selectedVideo = SelectVideoFromSearch(searchResults);

            Console.Clear();

            if (selectedVideo.title != null)
            {
                string? format = useDefaults ?
                    _optionsMonitor.CurrentValue?.DLFileOptions?[0].defaultValue
                    : _optionsMenus.ChooseFromOptionsList(OptionsConstants.DL_FILE_OPTIONS, OptionsConstants.FORMATS);
                string? location = useDefaults ?
                    _optionsMonitor.CurrentValue?.DLFileOptions?[1].defaultValue
                    : _optionsMenus.ChooseFromOptionsList(OptionsConstants.DL_FILE_OPTIONS, OptionsConstants.LOCATIONS);

                if (format != null && location != null && selectedVideo.ytUrl != null)
                    result = await _youtubeService.DownloadVideoWithUri(selectedVideo.ytUrl, location, format);

                Console.Clear();
                if (result)
                {
                    Console.WriteLine("Successful Download!");
                }
                else
                {
                    Console.WriteLine("Failed to Download!");
                }
            }


            Console.WriteLine("\n\nPress any key to continue...\n");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("\n\nFailed on doing YT search\n");
            System.Threading.Thread.Sleep(1500);
        }
    }

    private YTVideoDTO SelectVideoFromSearch(SearchResults searchResults)
    {
        YTVideoDTO selectedVideo = new YTVideoDTO();
        if (searchResults.result == true && searchResults.videos != null)
        {
            int videosIndex = 1;

            Console.Write("\nChoose video to download:\n");

            foreach (YTVideoDTO video in searchResults.videos)
            {
                Console.Write("\n" + videosIndex.ToString() + ": " + video.title + " by " + video.author + ", duration: " + video.duration.ToString());
                videosIndex++;
            }

            Console.WriteLine("\n");
            string? input = Console.ReadLine();
            Console.Clear();

            if (input != null && int.TryParse(input, out int selection) && selection <= searchResults.videos.Count())
            {
                selectedVideo = searchResults.videos[selection - 1];
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Unavailable option " + input + ", using first video on list");
                selectedVideo = searchResults.videos[0];
                System.Threading.Thread.Sleep(1500);
            }
        }
        else
        {
            Console.WriteLine("\n\nPlease input a value...\n");
            System.Threading.Thread.Sleep(1500);
        }
        return selectedVideo;
    }

    private async Task<ServiceResult> SelectServiceAndDownload(string? uri, string location, string format)
    {
        ServiceResult result = new ServiceResult();
        string? parsedUrl = null;
        string? service = null;
        bool downloadResult = false;

        if (uri != null)
        {
            UrlParserResult parserResult = _stringParser.ParseUrl(uri);
            service = parserResult.service;
            parsedUrl = parserResult.output;
        }

        if (parsedUrl != null && service != null)
        {
            switch (service)
            {
                case StringParser.YOUTUBE_SONG:
                    downloadResult = await _youtubeService.DownloadVideoWithUri(parsedUrl, location, format);
                    break;
                case StringParser.YOUTUBE_PLAYLIST:
                    downloadResult = await _youtubeService.DownloadPlaylistWithUri(parsedUrl, location, format);
                    break;
                case StringParser.SPOTIFY_SONG:
                    ServiceResult spotifyTrack = await _spotifyService.GetSpotifyTrack(parsedUrl);
                    if (spotifyTrack.output != null)
                    {
                        string searchQuery = spotifyTrack.output.trackName + ' ' + spotifyTrack.output.artists[0].name;
                        SearchResults searchResults = await _youtubeService.SearchVideo(searchQuery);
                        YTVideoDTO selectedVideo = SelectVideoFromSearch(searchResults);
                        if (format != null && location != null && selectedVideo.ytUrl != null)
                            downloadResult = await _youtubeService.DownloadVideoWithUri(selectedVideo.ytUrl, location, format);
                    }
                    break;
                default:
                    break;

            }

            result.result = downloadResult;
        }

        return result;
    }
}

