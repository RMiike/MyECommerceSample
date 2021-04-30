
using FluentValidation.Results;
using System.Linq;

namespace MECS.Identity.API.Models.BaseModel
{
    public abstract class BaseViewModel
    {
        public ValidationResult ValidationResult { get; protected set; }
        public string[] ErrorMessages => ValidationResult?.Errors?.Select(x => x.ErrorMessage).ToArray();
        public abstract bool IsValid();
    }
}
