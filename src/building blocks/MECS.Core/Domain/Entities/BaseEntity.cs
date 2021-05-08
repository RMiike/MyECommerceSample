
using FluentValidation.Results;
using System;
using System.Linq;

namespace MECS.Core.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public ValidationResult ValidationResult { get; protected set; }
        public string[] ErrorMessages => ValidationResult?.Errors?.Select(x => x.ErrorMessage).ToArray();
        public abstract bool IsValid();
    }
}
