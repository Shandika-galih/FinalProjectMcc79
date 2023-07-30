using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Contracts;

public interface ITokenHandler
{
    public string GenerateToken(IEnumerable<Claim> claims);
    public string GetTokenFromHeader(HttpRequest request);
    public JwtPayload DecodeJwtToken(string jwtToken);
}
