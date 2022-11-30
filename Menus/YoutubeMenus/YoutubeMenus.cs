using DJPPDL.Services;
namespace DJPPDL.Menus;


public class YoutubeMenus : IYoutubeMenus
{
    private readonly IYoutubeService _youtubeService;
    public YoutubeMenus(IYoutubeService youtubeService)
    {
        _youtubeService = youtubeService;
    }
    public async Task LinkDownload()
    {
        String location = "C:/Users/Public/Downloads";
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

