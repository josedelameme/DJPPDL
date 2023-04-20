using DJPPDL.Services;
using DJPPDL.Models.ServiceModels;

namespace DJPPDL.Menus;

public class MainMenu
{
    private readonly IDownloadMenus _downloadMenus;
    private readonly IOptionsMenus _optionsMenus;
    public MainMenu(IDownloadMenus downloadMenus, IOptionsMenus optionsMenus)
    {
        _downloadMenus = downloadMenus;
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
            Console.Write("\n1: Download song or playlist with link");
            Console.Write("\n2: Download song or playlist with link (using defaults)");
            Console.Write("\n4: Search and download");
            Console.Write("\n5: Search and download (using defaults)");
            Console.Write("\n6: TEST");
            Console.Write("\n7: Config");
            Console.Write("\n0: Exit");
            Console.Write("\n\n");

            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.Clear();
                    await _downloadMenus.LinkDownload(false);
                    break;
                case "2":
                    Console.Clear();
                    await _downloadMenus.LinkDownload(true);
                    break;
                case "4":
                    Console.Clear();
                    await _downloadMenus.SearchAndDownload(false);
                    break;
                case "5":
                    Console.Clear();
                    await _downloadMenus.SearchAndDownload(true);
                    break;
                case "6":
                    Console.Clear();
                    Console.WriteLine("TESTING OPTION");
                    break;
                case "7":
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
