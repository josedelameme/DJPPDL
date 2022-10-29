using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJPPDL.Services
{
    public interface IYoutubeService
    {
        Task<bool> DownloadVideoWithUri(String uri, String location, String format);
    }
}
