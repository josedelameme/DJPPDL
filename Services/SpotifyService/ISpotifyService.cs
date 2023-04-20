using DJPPDL.Models.ServiceModels;

namespace DJPPDL.Services;

public interface ISpotifyService
{
    Task<ServiceResult> GetSpotifyTrack(string trackId);
}