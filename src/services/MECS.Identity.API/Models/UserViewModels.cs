namespace MECS.Identity.API.Models
{
    public class SignUpUserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class SignInUserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
