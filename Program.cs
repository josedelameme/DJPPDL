using DJPPDL.Services;


IYoutubeService _youtubeService = new YoutubeService();

String location = "C:/Users/josem/Music/SETS/DL";
String format = "wav";

Console.WriteLine("URI:");
string? uri = Console.ReadLine();

bool result = false;

if (uri != null)
{
    result = await _youtubeService.DownloadVideoWithUri(uri, location, format);
}

Console.WriteLine(result.ToString());
