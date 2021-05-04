using MECS.Core.Domain.Validatiors;

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

}
