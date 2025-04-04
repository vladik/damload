namespace DamLoad.Assets.Api.V1.Tags.Requests;

public class RenameTagRequest
{
    public Guid TagId { get; set; }
    public string Name { get; set; } = string.Empty;
}
