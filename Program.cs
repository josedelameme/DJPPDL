using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YoutubeExplode;
using DJPPDL.Services;
using DJPPDL.Menus;
using DJPPDL.Utils;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, configuration) =>
    {
        configuration.Sources.Clear();

        IHostEnvironment env = hostingContext.HostingEnvironment;

        configuration
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

        IConfigurationRoot configurationRoot = configuration.Build();

        TransientFaultHandlingOptions options = new();
        configurationRoot.GetSection(nameof(TransientFaultHandlingOptions))
                         .Bind(options);

        Console.WriteLine($"TransientFaultHandlingOptions.Enabled={options.Enabled}");
        Console.WriteLine($"TransientFaultHandlingOptions.AutoRetryDelay={options.AutoRetryDelay}");
    })
    .ConfigureServices(
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