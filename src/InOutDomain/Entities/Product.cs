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

        public long ProductGroupId { get; set; }
        public ProductGroup ProductGroup { get; set; }

        public long BillingId { get; set; }
        public Billing Billing { get; set; }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}