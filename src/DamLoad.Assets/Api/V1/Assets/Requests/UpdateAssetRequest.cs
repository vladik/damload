

namespace DamLoad.Assets.Api.V1.Assets.Requests;

public class UpdateAssetRequest
{
    public Guid Id { get; set; }
    public string Status { get; set; } = default!;
}

