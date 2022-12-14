using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using DJPPDL.Options;
using System.Reflection;

namespace DJPPDL.Menus;

public class OptionsMenus : IOptionsMenus
{
    private readonly IOptionsMonitor<GeneralOptions> _optionsMonitor;

    public OptionsMenus(IOptionsMonitor<GeneralOptions> optionsMonitor)
    {
        _optionsMonitor = optionsMonitor;
    }

    public void GeneralConfigMenu()
    {
        bool exit = false;
        do
        {
            Console.Clear();
            Console.WriteLine("Config Menu:");
            Console.Write("\n1: Display all options");
            Console.Write("\n2: Display default options");
            Console.Write("\n3: Add an options value");
            Console.Write("\n4: Set default options");
            Console.Write("\n0: Return to Main Menu");
            Console.Write("\n\n");

            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.Clear();
                    DisplayCurrentOptions();
                    break;
                case "2":
                    Console.Clear();
                    DisplayCurrentDefaults();
                    break;
                case "3":
                    Console.Clear();
                    AddOptionsMenu();
                    break;
                case "4":
                    Console.Clear();
                    SetDefaultsMenu();
                    break;
                case "0":
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

    public string? ChooseFromOptionsList(string optionsSectionName, string optionsName)
    {
        string? selectedValue = null;

        IList<UserOptions> optionsSection = GetCurrentOptionsBySectionName(optionsSectionName);

        UserOptions userOptions = GetCurrentOptionsByName(optionsSection, optionsName);

        if (userOptions.values != null)
        {
            Console.WriteLine("\nSelect from the following " + optionsName + ":\n");

            int index = 1;
            foreach (string optionValue in userOptions.values)
            {
                Console.WriteLine(index + ": " + optionValue);
                index++;
            }
            Console.WriteLine();

            string? input = Console.ReadLine();

            if (input != null && int.TryParse(input, out int selection) && selection <= userOptions.values.Count())
            {
                selectedValue = userOptions.values[selection - 1];
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Unavailable option " + input + ", using default value of " + userOptions.defaultValue);
                selectedValue = userOptions.defaultValue;
                System.Threading.Thread.Sleep(1500);
            }
        }
        Console.Clear();
        return selectedValue;
    }

    private void AddOptionsMenu()
    {
        bool exit = false;
        do
        {
            Console.Clear();
            Console.WriteLine("Choose Options to add value to:");
            Console.Write("\n1: Locations");
            Console.Write("\n2: Formats");
            Console.Write("\n0: Return to Config Menu");
            Console.Write("\n\n");

            string? option = Console.ReadLine();
            int result = -1;

            switch (option)
            {
                case "1":
                    Console.Clear();
                    result = AddOptions(OptionsConstants.DL_FILE_OPTIONS, OptionsConstants.LOCATIONS, OptionsConstants.LOCATIONS_EXAMPLE);
                    break;
                case "2":
                    Console.Clear();
                    result = AddOptions(OptionsConstants.DL_FILE_OPTIONS, OptionsConstants.FORMATS, OptionsConstants.FORMATS_EXAMPLE);
                    break;
                case "0":
                    exit = true;
                    Console.Clear();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("TBD");
                    System.Threading.Thread.Sleep(1000);
                    break;
            }
            if (result == 1)
            {
                Console.Clear();
                Console.WriteLine("Added value succesfully!");
                System.Threading.Thread.Sleep(1000);
            }
            else if (result == 0)
            {
                Console.Clear();
                Console.WriteLine("Error while updating value");
                System.Threading.Thread.Sleep(1000);
            }

        } while (exit != true);
    }

    private void SetDefaultsMenu()
    {
        bool exit = false;
        do
        {
            Console.Clear();
            Console.WriteLine("Choose Options to set default to:");
            Console.Write("\n1: Locations");
            Console.Write("\n2: Formats");
            Console.Write("\n0: Return to Config Menu");
            Console.Write("\n\n");

            string? option = Console.ReadLine();
            int result = -1;

            switch (option)
            {
                case "1":
                    Console.Clear();
                    result = SetDefaults(OptionsConstants.DL_FILE_OPTIONS, OptionsConstants.LOCATIONS);
                    break;
                case "2":
                    Console.Clear();
                    result = SetDefaults(OptionsConstants.DL_FILE_OPTIONS, OptionsConstants.FORMATS);
                    break;
                case "0":
                    exit = true;
                    Console.Clear();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("TBD");
                    System.Threading.Thread.Sleep(1000);
                    break;
            }
            if (result == 1)
            {
                Console.Clear();
                Console.WriteLine("Set default value succesfully!");
                System.Threading.Thread.Sleep(1000);
            }
            else if (result == 0)
            {
                Console.Clear();
                Console.WriteLine("Error while updating default value");
                System.Threading.Thread.Sleep(1000);
            }

        } while (exit != true);
    }

    private int AddOptions(string optionsSection, string optionsName, string example)
    {
        Console.Write("\nType new value to add to " + optionsName + " (Ex:" + example + "): ");
        string? newValue = Console.ReadLine();
        int result = 0;

        Console.Clear();
        if (newValue != null && newValue != "")
        {
            Console.Write("\nUpdating values... ");
            System.Threading.Thread.Sleep(1000);

            RootOptions rootOptions = new RootOptions();
            string? json = File.ReadAllText("appsettings.json");

            int optionsIndex = -1;

            switch (optionsName)
            {
                case OptionsConstants.FORMATS:
                    optionsIndex = 0;
                    break;
                case OptionsConstants.LOCATIONS:
                    optionsIndex = 1;
                    break;
            }
            if (json != null && optionsIndex != -1)
            {
                JsonConvert.PopulateObject(json, rootOptions);
                switch (optionsSection)
                {
                    case OptionsConstants.DL_FILE_OPTIONS:
                        rootOptions.GeneralOptions?.DLFileOptions?[optionsIndex]?.values?.Add(newValue);
                        break;
                }
                string output = JsonConvert.SerializeObject(rootOptions, Formatting.Indented);
                File.WriteAllText("appsettings.json", output);
                result = 1;
            }
        }
        else
        {
            Console.Write("\nPlease enter a value..");
            System.Threading.Thread.Sleep(1000);
        }
        return result;
    }

    private int SetDefaults(string optionsSection, string optionsName)
    {
        string? newValue = ChooseFromOptionsList(optionsSection, optionsName);
        int result = 0;

        Console.Clear();
        if (newValue != null && newValue != "")
        {
            Console.Write("\nUpdating values... ");
            System.Threading.Thread.Sleep(1000);

            RootOptions rootOptions = new RootOptions();
            string? json = File.ReadAllText("appsettings.json");

            int optionsIndex = -1;

            switch (optionsName)
            {
                case OptionsConstants.FORMATS:
                    optionsIndex = 0;
                    break;
                case OptionsConstants.LOCATIONS:
                    optionsIndex = 1;
                    break;
            }
            if (json != null && optionsIndex != -1)
            {
                JsonConvert.PopulateObject(json, rootOptions);
                switch (optionsSection)
                {
                    case OptionsConstants.DL_FILE_OPTIONS:
                        PropertyInfo? propertyInfo = rootOptions.GeneralOptions?.DLFileOptions?[optionsIndex].GetType().GetProperty("defaultValue");
                        if (propertyInfo != null)
                        {
                            propertyInfo.SetValue(rootOptions.GeneralOptions?.DLFileOptions?[optionsIndex], newValue, null);
                        }
                        break;
                }
                string output = JsonConvert.SerializeObject(rootOptions, Formatting.Indented);
                File.WriteAllText("appsettings.json", output);
                result = 1;
            }
        }
        else
        {
            Console.Write("\nPlease enter a value..");
            System.Threading.Thread.Sleep(1000);
        }
        return result;
    }

    private void DisplayCurrentOptions()
    {
        var dlFileOptionsSection = _optionsMonitor.CurrentValue.DLFileOptions;
        if (dlFileOptionsSection != null)
        {
            Console.WriteLine("\nCurrent Options: ");
            Console.WriteLine("\n  Download File Options: ");
            foreach (UserOptions options in dlFileOptionsSection)
            {
                Console.WriteLine("\n    " + options.optionName + ":");
                if (options.values != null)
                {
                    foreach (string value in options.values)
                    {
                        Console.WriteLine("     " + value);
                    }
                }
            }
        }
        Console.WriteLine("\n\nPress any key to continue...\n");
        Console.ReadLine();
    }
    private void DisplayCurrentDefaults()
    {
        var dlFileOptionsSection = _optionsMonitor.CurrentValue.DLFileOptions;
        if (dlFileOptionsSection != null)
        {
            Console.WriteLine("\nCurrent Default Options: ");
            Console.WriteLine("\n  Download File Options: ");
            foreach (UserOptions options in dlFileOptionsSection)
            {

                if (options.defaultValue != null)
                {
                    Console.WriteLine("\n    " + options.optionName + ": " + options.defaultValue);
                }
            }
        }
        Console.WriteLine("\n\nPress any key to continue...\n");
        Console.ReadLine();
    }
    private IList<UserOptions> GetCurrentOptionsBySectionName(string optionsSectionName)
    {
        IList<UserOptions> currentOptionsSection = new List<UserOptions>();

        switch (optionsSectionName)
        {
            case OptionsConstants.DL_FILE_OPTIONS:
                var dlFileOptionsSection = _optionsMonitor.CurrentValue.DLFileOptions;
                if (dlFileOptionsSection != null)
                {
                    currentOptionsSection = dlFileOptionsSection;
                }
                break;
        }

        return currentOptionsSection;
    }

    private UserOptions GetCurrentOptionsByName(IList<UserOptions> optionsSection, string optionsName)
    {
        UserOptions currentOptions = new UserOptions();

        foreach (UserOptions options in optionsSection)
        {
            if (options.optionName == optionsName)
            {
                currentOptions = options;
                break;
            }
        }
        return currentOptions;
    }
}