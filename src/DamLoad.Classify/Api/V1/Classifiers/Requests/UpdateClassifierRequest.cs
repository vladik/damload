namespace DamLoad.Classify.Api.V1.Classifiers.Requests
{
    public class UpdateClassifierRequest
    {
        public string Slug { get; set; } = string.Empty;
        public string? Label { get; set; }
        public string Properties { get; set; } = "{}";
        public int SortOrder { get; set; } = 0;
    }
}
