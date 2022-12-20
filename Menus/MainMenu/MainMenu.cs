using DJPPDL.Services;

namespace DJPPDL.Menus;

public class MainMenu
{
    private readonly IYoutubeMenus _youtubeMenus;
    private readonly IOptionsMenus _optionsMenus;
    private readonly ISpotifyService _spotifyService;
    public MainMenu(IYoutubeMenus youtubeMenus, IOptionsMenus optionsMenus, ISpotifyService spotifyService)
    {
        _youtubeMenus = youtubeMenus;
        _optionsMenus = optionsMenus;
        _spotifyService = spotifyService;
    }


    public async Task ExecuteAsync()
    {
        bool exit = false;

        Console.Clear();
        Console.WriteLine("DJPPDL");
        System.Threading.Thread.Sleep(2000);
        do
        {
            Console.Clear();
            Console.WriteLine("Menu:");
            Console.Write("\n1: Download video with link");
            Console.Write("\n2: Download video with link (using defaults)");
            Console.Write("\n3: Download playlist with link");
            Console.Write("\n4: Download playlist with link (using defaults)");
            Console.Write("\n5: Search and download");
            Console.Write("\n6: Search and download (using defaults)");
            Console.Write("\n7: TEST");
            Console.Write("\n9: Config");
            Console.Write("\n0: Exit");
            Console.Write("\n\n");

            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.Clear();
                    await _youtubeMenus.LinkDownload(false);
                    break;
                case "2":
                    Console.Clear();
                    await _youtubeMenus.LinkDownload(true);
                    break;
                case "3":
                    Console.Clear();
                    await _youtubeMenus.LinkDownloadPlaylist(false);
                    break;
                case "4":
                    Console.Clear();
                    await _youtubeMenus.LinkDownloadPlaylist(true);
                    break;
                case "5":
                    Console.Clear();
                    await _youtubeMenus.SearchAndDownload(false);
                    break;
                case "6":
                    Console.Clear();
                    await _youtubeMenus.SearchAndDownload(true);
                    break;
                case "7":
                    Console.Clear();
                    await _spotifyService.GetSpotifyTrack();
                    break;
                case "9":
                    Console.Clear();
                    _optionsMenus.GeneralConfigMenu();
                    break;
                case "0":
                    Console.Clear();
                    Console.Write("Bye");
                    System.Threading.Thread.Sleep(1000);
                    exit = true;
                    Console.Clear();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Please choose a valid option");
                    System.Threading.Thread.Sleep(1000);
                    break;
            }
        } while (exit != true);
    }

}
