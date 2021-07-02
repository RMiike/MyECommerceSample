using MECS.Core.Data.Interface;
using System.Threading.Tasks;

namespace MECS.Order.Domain.Vouchers
{
    public interface IVoucherRepository : IRepository<Voucher>
    {
        Task<Voucher> ObterVoucherPorCodigo(string codigo);
    }
}
