namespace DamLoad.Assets.Api.V1.Collections.Responses
{
    public class CollectionResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SortOrder { get; set; }
    }

}
