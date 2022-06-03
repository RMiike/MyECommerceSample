using FluentValidation.Results;
using MECS.Core.Data.Messages;
using MECS.Order.API.Application.DTO;
using MECS.Order.API.Application.Events;
using MECS.Order.Domain.Orders;
using MECS.Order.Domain.Vouchers;
using MECS.Order.Domain.Vouchers.Specs;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MECS.Order.API.Application.Commands
{
    public class OrderCommandHandler : CommandHandler,
        IRequestHandler<AddOrderCommand, ValidationResult>
    {

        private readonly IVoucherRepository _voucherRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderCommandHandler(IVoucherRepository voucherRepository, IOrderRepository orderRepository)
        {
            _voucherRepository = voucherRepository;
            _orderRepository = orderRepository;
        }
        public async Task<ValidationResult> Handle(AddOrderCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
                return message.ValidationResult;

            var order = MapearPedido(message);

            if (!await AplicarVoucher(message, order))
                return ValidationResult;

            if (!ValidarPedido(order))
                return ValidationResult;

            if (!ProcessarPagamento(order))
                return ValidationResult;

            order.AutorizarPedido();

            order.AddEvent(new PedidoRealizadoEvent(order.Id, order.IdClient));

            _orderRepository.Add(order);

            return await PersistData(_orderRepository.UnitOfWork);
        }
        private Orders MapearPedido(AddOrderCommand message)
        {
            var address = new Address
            {
                Logradouro = message.Address.Logradouro,
                Numero = message.Address.Numero,
                Complemento = message.Address.Complemento,
                Bairro = message.Address.Bairro,
                CEP = message.Address.CEP,
                Cidade = message.Address.Cidade,
                Estado = message.Address.Estado,
            };

            var order = new Orders(
                message.IdClient,
                message.Total,
                message.OrderItems.Select(OrderItemDTO.ParaOrderItem).ToList(),
                message.IsUsedVoucher,
                message.Descount);

            order.AtribuirEndereco(address);
            return order;
        }
        private async Task<bool> AplicarVoucher(AddOrderCommand message, Orders order)
        {
            if (!message.IsUsedVoucher)
                return true;

            var voucher = await _voucherRepository.ObterVoucherPorCodigo(message.VoucherCodigo);

            if (voucher == null)
            {
                AdicionarErro("O voucher informado não existe");
                return false;
            }

            var voucherValidation = new VoucherValidation().Validate(voucher);

            if (!voucherValidation.IsValid)
            {
                voucherValidation.Errors.ToList().ForEach(error => AdicionarErro(error.ErrorMessage));
                return false;
            }

            order.AtribuirVoucher(voucher);
            voucher.DebitarQuantidade();

            _voucherRepository.Update(voucher);

            return true;
        }
        private bool ValidarPedido(Orders order)
        {
            var pedidovalorOriginal = order.Total;
            var pedidoDesconto = order.Descount;

            order.CalcularValorCarrinho();
            if (order.Total != pedidovalorOriginal)
            {
                AdicionarErro("O valor total do pedido não confere com o cálculo do pedido.");
                return false;
            }
            if (order.Descount != pedidoDesconto)
            {
                AdicionarErro("O valor total não confere com o cálculo do pedido.");
                return false;
            }
            return true;
        }
        private bool ProcessarPagamento(Orders order)
        {
            //TODO
            return true;
        }
    }
}
