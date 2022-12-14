namespace DJPPDL.Menus;

public interface IOptionsMenus
{
    public string? ChooseFromOptionsList(string optionsSectionName, string optionsName);
    public void GeneralConfigMenu();
}