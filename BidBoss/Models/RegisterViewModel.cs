namespace BidBoss.Models
{
    public class RegisterViewModel
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
        public string Role { get; set; } = default!;
    }

}
