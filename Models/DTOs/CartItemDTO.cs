﻿namespace FlowerShop_BackEnd.Models.DTOs
{
    public class CartItemDTO
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
