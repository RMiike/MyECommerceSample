using MECS.Client.API.Interfaces;
using MECS.Core.Data.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MECS.Client.API.Data.Repository
{
    public class ClientRepository : IClientRepository
    {

        private readonly ClientContext _context;
        public ClientRepository(ClientContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(Core.Domain.Entities.Client client)
        {
            _context.Clients.Add(client);
        }
        public async Task<IEnumerable<Core.Domain.Entities.Client>> Get()
        {
            return await _context.Clients.AsNoTracking().ToListAsync();
        }

        public async Task<Core.Domain.Entities.Client> GetByCPF(string CPF)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.CPF.Numero == CPF);
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
