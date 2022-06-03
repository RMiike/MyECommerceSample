using NetDevPack.Specification;
using System;
using System.Linq.Expressions;

namespace MECS.Order.Domain.Vouchers.Specs
{
    public class VoucherDataSpecification : Specification<Voucher>
    {
        public override Expression<Func<Voucher, bool>> ToExpression()
        {
            return voucher => voucher.DataValidade >= DateTime.Now;
        }
    }
    public class VoucherQuantitySpecification : Specification<Voucher>
    {
        public override Expression<Func<Voucher, bool>> ToExpression()
        {
            return voucher => voucher.Quantidade > 0;
        }
    }
    public class VoucherAtivoSpecification : Specification<Voucher>
    {
        public override Expression<Func<Voucher, bool>> ToExpression()
        {
            return voucher => voucher.Ativo;
        }
    }
    public class VoucherValidation : SpecValidator<Voucher>
    {
        public VoucherValidation()
        {
            var dataSpec = new VoucherDataSpecification();
            var qtSpec = new VoucherQuantitySpecification();
            var ativoSpec = new VoucherAtivoSpecification();


            Add("dataSpec", new Rule<Voucher>(dataSpec, "Voucher expirado."));
            Add("qtSpec", new Rule<Voucher>(qtSpec, "Voucher já utilizado."));
            Add("ativoSpec", new Rule<Voucher>(ativoSpec, "Voucher não está mais ativo."));
        }
    }
}
