using API.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Utilities.Handler;

public class TokenHandler : ITokenHandler
{
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(IEnumerable<Claim> claims)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTService:Key"]));

        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokenOptions = new JwtSecurityToken(issuer: _configuration["JWTService:Issuer"], audience: _configuration["JWTService:Audience"], claims: claims,
            expires: DateTime.Now.AddMinutes(100),
            signingCredentials: signinCredentials);
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return tokenString;
    }

    public string GetTokenFromHeader(HttpRequest request)
    {
        string authorizationHeader = request.Headers["Authorization"].FirstOrDefault();

        if (authorizationHeader?.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) == true)
        {
            return authorizationHeader.Substring("Bearer ".Length).Trim();
        }

        return null; // No valid token found in the header
    }

    public JwtPayload DecodeJwtToken(string token)
    {
        var secretKey = _configuration["JWTService:Key"];
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(secretKey);

        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true,
                ValidateAudience = false, // Set to true if you want to validate the audience
                ValidateIssuer = false,  // Set to true if you want to validate the issuer
            };

            // Validate and decode the token
            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

            // Convert the decoded token to its payload (claims)
            return ((JwtSecurityToken)validatedToken).Payload;
        }
        catch (Exception ex)
        {
            // Token validation failed, handle the exception as needed
            Console.WriteLine("JWT token validation failed: " + ex.Message);
            return null;
        }
    }
}