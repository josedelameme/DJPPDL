using YoutubeExplode;
using YoutubeExplode.Converter;
using DJPPDL.Utils;

namespace DJPPDL.Services
{
    public class YoutubeService : IYoutubeService
    {
        private readonly YoutubeClient _ytclient;
        private readonly IStringFormatter _stringFormatter;

        public YoutubeService(YoutubeClient ytclient, IStringFormatter stringFormatter)
        {
            _ytclient = ytclient;
            _stringFormatter = stringFormatter;
        }

        public async Task<bool> DownloadVideoWithUri(String uri, String location, String format)
        {
            bool result = true;
            try
            {
                var video = await _ytclient.Videos.GetAsync(uri);

                string fileName = _stringFormatter.RemoveSpecialCharacters(video.Title);

                Console.WriteLine($"Downloading {fileName} ...");

                await _ytclient.Videos.DownloadAsync(uri, $"{location}/{fileName}.{format}");
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
