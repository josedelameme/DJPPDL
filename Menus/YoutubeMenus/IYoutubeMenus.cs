public interface IYoutubeMenus
{
    Task LinkDownload(bool useDefaults);
    Task LinkDownloadPlaylist(bool useDefaults);
    Task SearchAndDownload(bool useDefaults);
}