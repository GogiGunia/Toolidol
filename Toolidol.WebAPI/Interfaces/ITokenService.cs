using Toolidol.Model.Model;
using Toolidol.WebAPI.Middleware;

namespace Toolidol.WebAPI.Interfaces
{
    public interface ITokenService
    {
        /// <summary>
        /// Creates a JWT for the specified token type and user.
        /// </summary>
        /// <param name="tokenType">The type of token to create (AccessToken, RefreshToken, PasswordResetToken).</param>
        /// <param name="user">The user for whom the token is being created.</param>
        /// <returns>A JWT string.</returns>
        string CreateToken(TokenType tokenType, User user);
    }
}
