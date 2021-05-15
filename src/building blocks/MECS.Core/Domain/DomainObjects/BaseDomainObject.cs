using FluentValidation.Results;
using System.Linq;

namespace MECS.Core.Domain.DomainObjects
{
    public abstract class BaseDomainObject
    {
        public ValidationResult ValidationResult { get; protected set; }
        public string[] ErrorMessages => ValidationResult?.Errors?.Select(x => x.ErrorMessage).ToArray();
        public abstract bool IsValid();
    }
}
