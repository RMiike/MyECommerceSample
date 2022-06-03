using System;
using System.Collections.Generic;

namespace MECS.WebApp.MVC.Models
{
    public class CartViewModel
    {
        public decimal Total { get; set; }
        public bool IsUsedVoucher { get; set; }
        public VoucherViewModel Voucher { get; set; }
        public decimal Descount { get; set; }
        public List<ItemProductViewModel> Itens { get; set; } = new List<ItemProductViewModel>();
    }

    public class ItemProductViewModel
    {
        public Guid IdProduct { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
