namespace DamLoad.Assets.Api.V1.Folders.Requests;

public class RenameFolderRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
