namespace DamLoad.Assets.Api.V1.Assets.Requests;

public class ListAssetsRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 100;
}