using MECS.Core.Domain.Interfaces;
using System;

namespace MECS.Core.Data.Interface
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
