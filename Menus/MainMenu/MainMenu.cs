namespace DJPPDL.Menus;

public class MainMenu
{
    private readonly IYoutubeMenus _youtubeMenus;
    private readonly IOptionsMenus _optionsMenus;
    public MainMenu(IYoutubeMenus youtubeMenus, IOptionsMenus optionsMenus)
    {
        _youtubeMenus = youtubeMenus;
        _optionsMenus = optionsMenus;
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
            Console.Write("\n9: Config");
            Console.Write("\n0: Exit");
            Console.Write("\n\n");

            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.Clear();
                    await _youtubeMenus.LinkDownload();
                    break;
                case "2":
                    Console.Clear();
                    await _youtubeMenus.LinkDownloadDefaults();
                    break;
                case "3":
                    Console.Clear();
                    await _youtubeMenus.LinkDownloadPlaylist();
                    break;
                case "4":
                    Console.Clear();
                    await _youtubeMenus.LinkDownloadPlaylistDefaults();
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
