using InOut.Domain.Enums;

namespace InOut.Domain.Entities
{
    public class Product : Base
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal UnitPrice { get; set; }
        public EColor Color { get; set; }
        public int Quantity { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public EUnitOfMeasurement UnitOfMeasurement { get; set; }
        public EProductType Type { get; set; }


        public long InventoryId { get; set; }
        public Inventory Inventory { get; set; }


        public ICollection<ProductProvider> ProductProviders { get; set; }
        public ICollection<Movement> Movements { get; set; }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}