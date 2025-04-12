using Microsoft.AspNetCore.Http;

namespace DamLoad.Assets.Api.V1.Assets.Requests;

public class UploadAssetRequest
{    
    public string Extension { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public IFormFile File { get; set; } = null!;
}
