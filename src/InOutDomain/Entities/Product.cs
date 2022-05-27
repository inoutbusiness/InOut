using InOut.Domain.Enums;

namespace InOut.Domain.Entities
{
    public class Product : Base
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public EUnitOfMeasurement UnitOfMeasurement { get; set; }

        public long BillingId { get; set; }
        public Billing Billing { get; set; }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}