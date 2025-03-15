namespace DamLoad.Transformation
{
    public class TransformationResult
    {
        public string? TransformedUrl { get; set; } // Cloud-based transformations
        public string? TransformedFilePath { get; set; } // Local file transformations
        public Stream? TransformedStream { get; set; } // For in-memory transformations
        public int Width { get; set; }
        public int Height { get; set; }
        public string Format { get; set; } = string.Empty;
        public int? Quality { get; set; }
    }
}
