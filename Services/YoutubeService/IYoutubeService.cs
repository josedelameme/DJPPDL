﻿namespace DJPPDL.Services
{
    public interface IYoutubeService
    {
        Task<bool> DownloadVideoWithUri(String uri, String location, String format);
    }
}
