namespace DJPPDL.Models.ServiceModels;

public class ServiceResult
{
    public bool? result { get; set; }
    public dynamic? output { get; set; }
    public string? errorMessage { get; set; }
}