namespace FlowerShop_BackEnd.Services
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user);
    }
}
