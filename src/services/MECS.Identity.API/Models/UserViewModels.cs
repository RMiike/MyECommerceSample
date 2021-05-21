using System.Collections.Generic;

namespace MECS.Identity.API.Models
{
    public class SignUpUserViewModel
    {
        public string Name { get;  set; }
        public string CPF { get;  set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class SignInUserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class SignInUserResponse
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
    }

    public class UserToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }
    }
    public class UserClaim
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
