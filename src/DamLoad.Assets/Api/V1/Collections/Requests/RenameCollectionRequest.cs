namespace DamLoad.Assets.Api.V1.Collections.Requests
{
    public class RenameCollectionRequest
    {
        public Guid CollectionId { get; set; }
        public string NewName { get; set; } = string.Empty;
    }

}
