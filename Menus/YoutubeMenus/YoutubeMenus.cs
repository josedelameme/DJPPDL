using Microsoft.Extensions.Options;
using DJPPDL.Services;
using DJPPDL.Options;
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
    public async Task LinkDownload()
    {
        string? format = _optionsMenus.ChooseFromOptionsList(OptionsConstants.DL_FILE_OPTIONS, OptionsConstants.FORMATS);
        string? location = _optionsMenus.ChooseFromOptionsList(OptionsConstants.DL_FILE_OPTIONS, OptionsConstants.LOCATIONS);

        if (format != null && location != null)
        {
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
        else
        {
            Console.WriteLine("\n\nError on retrieving Configuration values...\n");
        }
    }

    public async Task LinkDownloadDefaults()
    {
        string? format = _optionsMonitor.CurrentValue?.DLFileOptions?[0].defaultValue;
        string? location = _optionsMonitor.CurrentValue?.DLFileOptions?[1].defaultValue;

        if (format != null && location != null)
        {
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
        else
        {
            Console.WriteLine("\n\nError on retrieving Configuration values...\n");
        }
    }
}

