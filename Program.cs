using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YoutubeExplode;
using DJPPDL.Services;
using DJPPDL.Menus;
using DJPPDL.Utils;
using DJPPDL.Options;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, configuration) =>
    {
        configuration.Sources.Clear();
        IHostEnvironment env = hostingContext.HostingEnvironment;
        configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot configurationRoot = configuration.Build();
    })
    .ConfigureServices(
    (context, services) =>
    {
        var configurationRoot = context.Configuration;
        services
        .Configure<GeneralOptions>(
           configurationRoot.GetSection(nameof(GeneralOptions)))
        .Configure<ServiceOptions>(
           configurationRoot.GetSection(nameof(ServiceOptions)))
        .AddSingleton<HttpClient>()
        .AddSingleton<IStringFormatter, StringFormatter>()
        .AddSingleton<IStringParser, StringParser>()
        .AddSingleton<YoutubeClient>()
        .AddSingleton<IYoutubeService, YoutubeService>()
        .AddSingleton<ISpotifyService, SpotifyService>()
        .AddSingleton<IDownloadMenus, DownloadMenus>()
        .AddSingleton<IOptionsMenus, OptionsMenus>()
        .AddSingleton<MainMenu>();
    })
    .Build();

var mainMenu = host.Services.GetRequiredService<MainMenu>();

await mainMenu.ExecuteAsync();