namespace DamLoad.Abstractions.Models
{
    public class AuthUserModel
    {
        public string Id { get; set; } = default!;
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string[] Roles { get; set; } = Array.Empty<string>();
    }
}
