namespace DamLoad.Assets.Api.V1.Tags.Responses;

public class RenameTagResponse
{
    public Guid TagId { get; set; }
    public string NewName { get; set; } = string.Empty;
}
