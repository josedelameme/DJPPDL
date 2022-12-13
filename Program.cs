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

        // DLFileOptions options = new();
        // configurationRoot.GetSection(nameof(DLFileOptions))
        //                  .Bind(options);

        // Console.WriteLine($"XD: {options.Formats}");
        // Console.WriteLine($"XD: {options.Locations}");
    })
    .ConfigureServices(
    (context, services) =>
    {
        var configurationRoot = context.Configuration;
        services
        .Configure<GeneralOptions>(
           configurationRoot.GetSection(nameof(GeneralOptions)))
        .AddSingleton<IStringFormatter, StringFormatter>()
        .AddSingleton<YoutubeClient>()
        .AddSingleton<IYoutubeService, YoutubeService>()
        .AddSingleton<IYoutubeMenus, YoutubeMenus>()
        .AddSingleton<MainMenu>();
    })
    .Build();

var mainMenu = host.Services.GetRequiredService<MainMenu>();

await mainMenu.ExecuteAsync();