using MECS.Core.Data.Messages;
using System;

namespace MECS.Order.API.Application.Events
{
    public class PedidoRealizadoEvent : Event
    {
        public PedidoRealizadoEvent(Guid idOrder, Guid idClient)
        {
            IdOrder = idOrder;
            IdClient = idClient;
        }
        public Guid IdOrder { get; private set; }
        public Guid IdClient { get; private set; }
    }
}
