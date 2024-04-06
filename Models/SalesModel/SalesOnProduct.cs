﻿using WebStore.Data.Entities;

namespace WebStore.Models.SalesModel
{
    public class SalesOnProduct
    {
        public string Title { get; set; }
        public Product Product { get; set; }
        public decimal Discount { get; set; }
    }
}