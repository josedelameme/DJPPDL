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
            Console.Write("\n1: Download with link");
            Console.Write("\n2: Download with link (using defaults)");
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
                    Console.WriteLine("TBD");
                    System.Threading.Thread.Sleep(1000);
                    break;
            }
        } while (exit != true);
    }

}
