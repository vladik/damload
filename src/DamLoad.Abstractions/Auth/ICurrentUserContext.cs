namespace DamLoad.Abstractions.Auth
{
    public interface ICurrentUserContext
    {
        bool IsAuthenticated { get; }
        string? Id { get; }
        string? Email { get; }
        string? Name { get; }
        string[] Roles { get; }
    }
}
