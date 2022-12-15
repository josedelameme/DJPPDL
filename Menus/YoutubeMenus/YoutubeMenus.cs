using Microsoft.Extensions.Options;
using DJPPDL.Services;
using DJPPDL.Options;
using DJPPDL.Models;
namespace DJPPDL.Menus;


public class YoutubeMenus : IYoutubeMenus
{
    private readonly IYoutubeService _youtubeService;
    private readonly IOptionsMenus _optionsMenus;

    private readonly IOptionsMonitor<GeneralOptions> _optionsMonitor;
    public YoutubeMenus(IYoutubeService youtubeService, IOptionsMenus optionsMenus, IOptionsMonitor<GeneralOptions> optionsMonitor)
    {
        _youtubeService = youtubeService;
        _optionsMenus = optionsMenus;
        _optionsMonitor = optionsMonitor;
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
            Console.WriteLine("Youtube video URL:\n");
            string? uri = Console.ReadLine();
            Console.Clear();
            bool result = false;
            if (uri != null)
            {
                result = await _youtubeService.DownloadVideoWithUri(uri, location, format);
            }
            Console.Clear();
            if (result)
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
    public async Task LinkDownloadPlaylist(bool useDefaults)
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
            Console.WriteLine("Youtube Playlist URL:\n");
            string? uri = Console.ReadLine();
            Console.Clear();
            bool result = false;
            if (uri != null)
            {
                result = await _youtubeService.DownloadPlaylistWithUri(uri, location, format);
            }
            Console.Clear();
            if (result)
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
        if (searchQuery != null)
        {
            SearchResults searchResults = await _youtubeService.SearchVideo(searchQuery);

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
                YTVideoDTO selectedVideo = new YTVideoDTO();

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
                Console.Clear();
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
                Console.WriteLine("\n\nPress any key to continue...\n");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("\n\nFailed on doing YT search\n");
                System.Threading.Thread.Sleep(1500);
            }
        }
        else
        {
            Console.WriteLine("\n\nPlease input a value...\n");
            System.Threading.Thread.Sleep(1500);
        }
    }
}

