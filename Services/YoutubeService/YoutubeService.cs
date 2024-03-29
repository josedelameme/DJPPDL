﻿using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Search;
using DJPPDL.Models.YTModels;
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

                await _ytclient.Videos.DownloadAsync(video.Url, $"{location}/{fileName}.{format}");
            }
            catch 
            {
                result = false;
            }
            return result;
        }

        public async Task<bool> DownloadPlaylistWithUri(String uri, String location, String format)
        {
            bool result = true;
            try
            {
                await foreach (var video in _ytclient.Playlists.GetVideosAsync(uri))
                {
                    string fileName = _stringFormatter.RemoveSpecialCharacters(video.Title);
                    string videoUrl = video.Url;
                    Console.WriteLine($"Downloading {fileName} ...");

                    await _ytclient.Videos.DownloadAsync(videoUrl, $"{location}/{fileName}.{format}");
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public async Task<SearchResults> SearchVideo(String searchQuery)
        {
            bool result = true;
            SearchResults searchResults = new SearchResults();
            IList<YTVideoDTO> videoList = new List<YTVideoDTO>();
            try
            {
                await foreach (var batch in _ytclient.Search.GetResultBatchesAsync(searchQuery))
                {
                    foreach (var searchResult in batch.Items)
                    {
                        switch (searchResult)
                        {
                            case VideoSearchResult videoSearchResult:
                                videoList = videoList.Append<YTVideoDTO>(
                                    new YTVideoDTO
                                    {
                                        author = videoSearchResult.Author.ChannelTitle,
                                        title = videoSearchResult.Title,
                                        duration = videoSearchResult.Duration,
                                        ytUrl = videoSearchResult.Url
                                    })
                                    .ToList<YTVideoDTO>();
                                break;
                        }
                    }
                    break;
                }
                searchResults.videos = videoList;
            }
            catch
            {
                result = false;
            }
            searchResults.result = result;
            return searchResults;
        }
    }
}
