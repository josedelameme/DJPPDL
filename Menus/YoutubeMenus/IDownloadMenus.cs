public interface IDownloadMenus
{
    Task LinkDownload(bool useDefaults);
    Task SearchAndDownload(bool useDefaults);
}