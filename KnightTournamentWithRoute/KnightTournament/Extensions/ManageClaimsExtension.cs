
using KnightTournament.Helpers;
using System.Security.Claims;

namespace KnightTournament.Extensions
{
    public static class ManageClaimsExtension
    {
        public static Result<Guid> GetUserIdFromPrincipal(this ClaimsPrincipal user) 
        {
            if (user != null) 
            {
                return new Result<Guid>(true, Guid.Parse(user.Identities.First().Claims.First().Value)) ;
            }

            return new Result<Guid>(false, "User is not authenticated");
        }
    }
}
