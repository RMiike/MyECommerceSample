using MECS.Core.Domain.Entities;
using MECS.Core.Domain.Interfaces;
using System;

namespace MECS.Order.Domain.Vouchers
{
    public class Voucher : BaseEntity, IAggregateRoot
    {
        public string Codigo { get; private set; }
        public decimal? Percentual { get; private set; }
        public decimal? ValorDesconto { get; set; }
        public int Quantidade { get; private set; }
        public TypeDescountVoucher TipoDesconto { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataUtilizacao { get; private set; }
        public DateTime DataValidade { get; private set; }
        public bool Ativo { get; private set; }
        public bool Usado { get; private set; }
        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}
