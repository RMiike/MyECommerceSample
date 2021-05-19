using FluentValidation.Results;
using MECS.Core.Data.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MECS.Core.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public ValidationResult ValidationResult { get; protected set; }
        public string[] ErrorMessages => ValidationResult?.Errors?.Select(x => x.ErrorMessage).ToArray();

        private List<Event> _notifications;
        public IReadOnlyCollection<Event> Notifications => _notifications?.AsReadOnly();

        public void AddEvent(Event eventItem)
        {
            _notifications = _notifications ?? new List<Event>();
            _notifications.Add(eventItem);
        }
        public void RemoveEvent(Event eventItem)
        {
            _notifications?.Remove(eventItem);
        }
        public void ClearEvents()
        {
            _notifications?.Clear();
        }
        #region actions
        public abstract bool IsValid();
        public static bool operator ==(BaseEntity a, BaseEntity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
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
        #endregion
    }
}
