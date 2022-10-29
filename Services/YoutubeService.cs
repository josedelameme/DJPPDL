using YoutubeExplode;
using YoutubeExplode.Converter;

namespace DJPPDL.Services
{
    public class YoutubeService : IYoutubeService
    {
        private readonly YoutubeClient _ytclient;
        public YoutubeService()
        {
            _ytclient = new YoutubeClient();
        }

        public async Task<bool> DownloadVideoWithUri(String uri, String location, String format)
        {
            bool result = true;
            try
            {
                var video = await _ytclient.Videos.GetAsync(uri);

                await _ytclient.Videos.DownloadAsync(uri, $"{location}/{video.Title}.{format}");
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
