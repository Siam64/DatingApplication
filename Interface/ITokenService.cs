using WebApplication1.Entity;

namespace WebApplication1.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
