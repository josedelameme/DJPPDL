using Microsoft.Extensions.Options;
using DJPPDL.Options;

namespace DJPPDL.Menus;

public class OptionsMenus
{
    private readonly IOptionsMonitor<GeneralOptions> _optionsMonitor;

    public OptionsMenus(IOptionsMonitor<GeneralOptions> optionsMonitor)
    {
        _optionsMonitor = optionsMonitor;
    }

    public string ChooseFromOptionsList(string optionsSectionName, string optionsName)
    {
        IList<UserOptions> optionsSection = GetCurrentOptionsBySectionName(optionsSectionName);

        UserOptions userOptions = GetCurrentOptionsByName(optionsSection, optionsName);






        return "xd";
    }

    private IList<UserOptions> GetCurrentOptionsBySectionName(string optionsSectionName)
    {
        IList<UserOptions> currentOptionsSection = new List<UserOptions>();

        switch (optionsSectionName)
        {
            case "DLFileOptions":
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