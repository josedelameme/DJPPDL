namespace DJPPDL.Menus;


public class MainMenu
{
    private readonly IYoutubeMenus _youtubeMenus;
    public MainMenu(IYoutubeMenus youtubeMenus)
    {
        _youtubeMenus = youtubeMenus;
    }


    public async Task ExecuteAsync()
    {
        bool exit = false;

        Console.WriteLine("DJPPDL");
        System.Threading.Thread.Sleep(2000);
        do
        {
            Console.Clear();
            Console.WriteLine("Menu:");
            Console.Write("\n1: Download with link");
            Console.Write("\n9: Config");
            Console.Write("\n0: Exit");
            Console.Write("\n");

            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.Clear();
                    await _youtubeMenus.LinkDownload();

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
