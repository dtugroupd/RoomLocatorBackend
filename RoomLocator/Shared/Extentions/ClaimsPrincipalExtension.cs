using System.Linq;
using System.Security.Claims;

namespace Shared.Extentions
{
    /// <summary>
    /// Extention method for ClaimsPrincipal (JWT Token)
    ///     <author>Anders Wiberg Olsen, s165241</author>
    /// </summary>
    public static class ClaimsPrincipalExtension
    {
        public static string StudentId(this ClaimsPrincipal user) =>
            user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}