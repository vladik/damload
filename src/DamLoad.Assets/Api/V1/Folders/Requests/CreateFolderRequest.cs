namespace DamLoad.Assets.Api.V1.Folders.Requests;

public class CreateFolderRequest
{
    public string Name { get; set; } = string.Empty;
    public Guid? ParentId { get; set; }
    public int SortOrder { get; set; } = 0;
}
