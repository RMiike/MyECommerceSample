using MECS.Core.Domain.Entities;
using MECS.Identity.API.Models;

namespace MECS.Identity.API.Extensions
{
    public static class ConvertModelToEntity
    {
        public static SignUpUser ConvertToEntity(this SignUpUserViewModel model)
            => new SignUpUser(model.Email, model.Password, model.ConfirmPassword);

        public static SignInUser ConvertToEntity(this SignInUserViewModel model)
            => new SignInUser(model.Email, model.Password);
    }
}
