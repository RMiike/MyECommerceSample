using MECS.Core.Domain.Validatiors;
using System.Collections.Generic;

namespace MECS.Core.Domain.Entities
{
    public class SignUpUser : BaseEntity
    {
        public SignUpUser(string email, string password, string confirmPassword)
        {
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public string Email { get; private set; }
        public string Password { get; private set; }
        public string ConfirmPassword { get; private set; }
        public override bool IsValid()
        {
            ValidationResult = new SignUpUserValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class SignInUser : BaseEntity
    {
        public SignInUser(string email, string password)
        {
            Email = email;
            Password = password;
        }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public override bool IsValid()
        {
            ValidationResult = new SignInUserUserValidation().Validate(this);
            return ValidationResult.IsValid;
        }
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
