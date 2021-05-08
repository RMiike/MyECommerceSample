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
        public static bool operator ==(BaseEntity a, BaseEntity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return true;
            return a.Equals(b);
        }
        public static bool operator !=(BaseEntity a, BaseEntity b)
            => !(a == b);
        public override bool Equals(object obj)
        {
            var compareTo = obj as BaseEntity;
            if (ReferenceEquals(this, compareTo))
                return true;
            if (ReferenceEquals(null, compareTo))
                return false;

            return Id.Equals(compareTo.Id);
        }
        public override int GetHashCode()
            => (GetType().GetHashCode() * 907) + Id.GetHashCode();
        public override string ToString()
            => $"{GetType().Name} [Id={Id}]";
    }
}
