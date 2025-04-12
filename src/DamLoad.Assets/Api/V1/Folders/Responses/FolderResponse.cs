namespace DamLoad.Assets.Api.V1.Folders.Responses;

public class FolderResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid? ParentId { get; set; }
    public int SortOrder { get; set; }
}
