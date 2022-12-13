using Microsoft.Extensions.Options;
using DJPPDL.Services;
using DJPPDL.Options;
namespace DJPPDL.Menus;


public class YoutubeMenus : IYoutubeMenus
{
    private readonly IYoutubeService _youtubeService;
    private readonly IOptionsMonitor<GeneralOptions> _dlMonitor;
    public YoutubeMenus(IYoutubeService youtubeService, IOptionsMonitor<GeneralOptions> dlMonitor)
    {
        _youtubeService = youtubeService;
        _dlMonitor = dlMonitor;
    }
    public async Task LinkDownload()
    {
        GeneralOptions options = _dlMonitor.CurrentValue;

        String location = "C:/Users/josem/Music/SETS/IDM";
        String format = "wav";
        Console.WriteLine("Youtube URL:\n");
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
}

