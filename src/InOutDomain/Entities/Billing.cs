namespace InOut.Domain.Entities
{
    public class Billing : Base
    {
        public decimal Value { get; set; }

        public long CustomerId { get; set; }
        public User Customer { get; set; }

        public ICollection<Product> Products { get; set; }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}