namespace DamLoad.Assets.Api.V1.Collections.Requests
{
    public class UpdateSortOrderRequest
    {
        public Guid CollectionId { get; set; }
        public int NewSortOrder { get; set; }
    }

}
