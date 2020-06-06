﻿using System;

namespace DAL
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
