using MECS.Core.Domain.Entities;
using MECS.Core.Domain.Interfaces;
using MECS.Order.Domain.Vouchers.Specs;
using System;

namespace MECS.Order.Domain.Vouchers
{
    public class Voucher : BaseEntity, IAggregateRoot
    {
        public string Codigo { get; private set; }
        public decimal? Percentual { get; private set; }
        public decimal? ValorDesconto { get; private set; }
        public int Quantidade { get; private set; }
        public TipoDescontoVoucher TipoDesconto { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataUtilizacao { get; private set; }
        public DateTime DataValidade { get; private set; }
        public bool Ativo { get; private set; }
        public bool Usado { get; private set; }
        public override bool IsValid()
        {
            return new VoucherAtivoSpecification()
                .And(new VoucherDataSpecification())
                .And(new VoucherQuantitySpecification())
                .IsSatisfiedBy(this);
        }
        public void MarcarComoUtilizado()
        {
            Ativo = false;
            Usado = true;
            Quantidade = 0;
            DataUtilizacao = DateTime.Now;
        }
        public void DebitarQuantidade()
        {
            Quantidade -= 1;
            if (Quantidade >= 1)
                return;
            MarcarComoUtilizado();
        }

    }
}
