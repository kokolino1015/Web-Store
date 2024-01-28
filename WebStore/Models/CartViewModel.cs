﻿using WebStore.Data.Entities;

namespace WebStore.Models
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
