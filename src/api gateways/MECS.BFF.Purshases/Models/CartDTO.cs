﻿using System.Collections.Generic;

namespace MECS.BFF.Purshases.Models
{
    public class CartDTO
    {
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public List<ItemCartDTO> Itens { get; set; } = new List<ItemCartDTO>();
    }
}
