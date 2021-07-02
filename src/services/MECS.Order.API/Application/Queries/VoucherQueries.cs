using MECS.Order.API.Application.DTO;
using MECS.Order.Domain.Vouchers;
using System.Threading.Tasks;

namespace MECS.Order.API.Application.Queries
{
    public interface IVoucherQueries
    {
        Task<VoucherDTO> GetVoucherByCode(string code);
    }
    public class VoucherQueries : IVoucherQueries
    {

        private readonly IVoucherRepository _repository;

        public VoucherQueries(IVoucherRepository repository)
        {
            _repository = repository;
        }

        public async Task<VoucherDTO> GetVoucherByCode(string code)
        {
            var voucher = await _repository.ObterVoucherPorCodigo(code);
            if (voucher == null)
                return null;

            return new VoucherDTO
            {

                Codigo = voucher.Codigo,
                TipoDesconto = (int) voucher.TipoDesconto,
                Percentual = voucher.Percentual,
                ValorDesconto = voucher.ValorDesconto
            };
        }
    }
}
