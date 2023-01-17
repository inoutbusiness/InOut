using InOut.Domain.Enums;

namespace InOut.Domain.DTOs
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public EColor Color { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public EUnitOfMeasurement UnitOfMeasurement { get; set; }
        public EProductType Type { get; set; }
        public long BrandId { get; set; }
        public long InventoryId { get; set; }
    }
}
