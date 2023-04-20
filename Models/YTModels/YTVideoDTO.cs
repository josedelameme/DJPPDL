using System;
namespace DJPPDL.Models.YTModels;

public class YTVideoDTO
{
    public String? title { get; set; }
    public String? author { get; set; }
    public TimeSpan? duration { get; set; }
    public String? ytUrl { get; set; }
}