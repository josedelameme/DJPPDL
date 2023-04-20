using DJPPDL.Models.ServiceModels;
namespace DJPPDL.Utils;
public interface IStringParser
{
    UrlParserResult ParseUrl(string url);
}