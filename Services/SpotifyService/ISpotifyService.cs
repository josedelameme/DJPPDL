namespace DJPPDL.Services;

public interface ISpotifyService
{
    Task<bool> GetSpotifyTrack();
}