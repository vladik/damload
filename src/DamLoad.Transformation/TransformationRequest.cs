using DamLoad.Core.Enums;

namespace DamLoad.Transformation
{
    public class TransformationRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty; // For remote transformations
        public Stream? InputStream { get; set; } // For local file transformations
        public int Width { get; set; }
        public int Height { get; set; }
        public string Format { get; set; } = string.Empty;
        public int? Quality { get; set; }
        public string Transformation { get; set; } = string.Empty;
        public AssetType AssetType { get; set; }
    }
}
