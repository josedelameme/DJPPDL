using DJPPDL.Models.YTModels;

namespace DJPPDL.Services
{
    public interface IYoutubeService
    {
        Task<bool> DownloadVideoWithUri(String uri, String location, String format);
        Task<bool> DownloadPlaylistWithUri(String uri, String location, String format);
        Task<SearchResults> SearchVideo(String searchQuery);
    }
}
