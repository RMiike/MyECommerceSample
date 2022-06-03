using MECS.Core.Domain.Entities;
using MECS.Core.Domain.Interfaces;
using MECS.Order.Domain.Vouchers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MECS.Order.Domain.Orders
{
    public class Orders : BaseEntity, IAggregateRoot
    {
        protected Orders() { }
        public Orders(
            Guid idClient,
            decimal total,
            List<OrderItem> orderItems,
            bool isUsedVoucher = false,
            decimal descount = 0,
            Guid? idVoucher = null)
        {
            IdClient = idClient;
            Total = total;
            _orderItems = orderItems;
            Descount = descount;
            IsUsedVoucher = isUsedVoucher;
            IdVoucher = idVoucher;
        }

        public int Codigo { get; private set; }
        public Guid IdClient { get; private set; }
        public Guid? IdVoucher { get; private set; }
        public bool IsUsedVoucher { get; private set; }
        public decimal Descount { get; private set; }
        public decimal Total { get; private set; }
        public DateTime InitialDate { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
        public Address Address { get; private set; }
        public Voucher Voucher { get; private set; }

        public void AutorizarPedido()
        {
            OrderStatus = OrderStatus.Autorizado;
        }
        public void AtribuirVoucher(Voucher voucher)
        {
            IsUsedVoucher = true;
            IdVoucher = voucher.Id;
            Voucher = voucher;
        }
        public void AtribuirEndereco(Address address)
        {
            Address = address;
        }
        public void CalcularValorCarrinho()
        {
            Total = OrderItems.Sum(p => p.CalcularValor());
            CalcularValorTotalDesconto();
        }
        public void CalcularValorTotalDesconto()
        {
            if (!IsUsedVoucher)
                return;
            decimal desconto = 0;
            var valor = Total;
            if (Voucher.TipoDesconto == TipoDescontoVoucher.Porcentagem)
            {
                if (Voucher.Percentual.HasValue)
                {
                    desconto = (valor * Voucher.Percentual.Value) / 100;
                    valor -= desconto;
                }
            }
            else
            {
                if (Voucher.ValorDesconto.HasValue)
                {
                    desconto = Voucher.ValorDesconto.Value;
                    valor -= desconto;
                }
            }

            Total = valor < 0 ? 0 : valor;
            Descount = desconto;
        }
        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
