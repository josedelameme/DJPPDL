using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YoutubeExplode;
using DJPPDL.Services;
using DJPPDL.Menus;
using DJPPDL.Utils;

using IHost host = Host.CreateDefaultBuilder(args).ConfigureServices(
    (_, services) =>
    services
        .AddSingleton<IStringFormatter, StringFormatter>()
        .AddSingleton<YoutubeClient>()
        .AddSingleton<IYoutubeService, YoutubeService>()
        .AddSingleton<IYoutubeMenus, YoutubeMenus>()
        .AddSingleton<MainMenu>())
    .Build();

var mainMenu = host.Services.GetRequiredService<MainMenu>();

await mainMenu.ExecuteAsync();