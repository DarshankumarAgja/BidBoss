using Microsoft.AspNetCore.Identity;

namespace BidBoss.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string UserType { get; set; } = default!;  // ✅ Ensure this exists if used in `IsBuyer()`
    }
}
