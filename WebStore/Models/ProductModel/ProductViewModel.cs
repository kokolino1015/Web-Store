﻿using WebStore.Data.Entities;

namespace WebStore.Models.ProductModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<Photo> Photos { get; set; } = new List<Photo>();
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
