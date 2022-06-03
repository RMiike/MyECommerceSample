using System.Collections.Generic;

namespace MECS.BFF.Purshases.Models
{
    public class CartDTO
    {
        public decimal Total { get; set; }
        public bool IsUsedVoucher { get; set; }
        public VoucherDTO Voucher { get; set; }
        public decimal Descount { get; set; }
        public List<ItemCartDTO> Itens { get; set; } = new List<ItemCartDTO>();
    }
}
