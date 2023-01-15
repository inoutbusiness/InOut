﻿namespace InOut.Domain.Models.Product
{
    public class ProductModel
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string Type { get; set; }
        public string BrandName { get; set; }
        public long InventoryId { get; set; }
    }
}
