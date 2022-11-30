using System.Text;
namespace DJPPDL.Utils;
public class StringFormatter : IStringFormatter
{
    public string RemoveSpecialCharacters(string str)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in str)
        {
            if (c == '|' || c == '>' || c == '<' || c == '/' || c == ':' || c == '?' || c == '*')
            {
            }
            else{
                sb.Append(c);
            }
        }
        return sb.ToString();
    }

}

