using MECS.Core.Data.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MECS.Client.API.Interfaces
{
    public interface IClientRepository : IRepository<Core.Domain.Entities.Client>
    {
        void Adicionar(Core.Domain.Entities.Client client);
        Task<IEnumerable<Core.Domain.Entities.Client>> Get();
        Task<Core.Domain.Entities.Client> GetByCPF(string CPF);
    }
}
