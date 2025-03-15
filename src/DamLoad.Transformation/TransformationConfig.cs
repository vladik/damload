namespace DamLoad.Transformation
{
    public class TransformationConfig
    {
        public List<PredefinedTransformation> CreateVariantsOnUpload { get; set; } = new();
        public List<string> AllowedTransformations { get; set; } = new();
    }

    public class PredefinedTransformation
    {
        public string Name { get; set; } = string.Empty;
        public int Width { get; set; }
        public int Height { get; set; }
        public string Format { get; set; } = string.Empty;
        public int? Quality { get; set; }
        public string Transformation { get; set; } = string.Empty;
    }
}
