using MECS.Core.Data.Interface;
using MECS.Order.Domain.Vouchers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MECS.Order.Infra.Data.Repository
{
    public class VoucherRepository : IVoucherRepository
    {

        private readonly OrderContext _context;

        public VoucherRepository(OrderContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
        public async Task<Voucher> ObterVoucherPorCodigo(string codigo)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(p => p.Codigo == codigo);
        }
        public void Update(Voucher voucher)
        {
            _context.Vouchers.Update(voucher);
        }
        public void Dispose()
        {
            _context?.Dispose();
        }


    }
}
