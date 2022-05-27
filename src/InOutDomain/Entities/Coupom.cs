namespace InOut.Domain.Entities
{
    public class Coupom : Base
    {
        public string Name { get; set; }
        public decimal DiscountPercentage { get; set; }

        public ICollection<Billing> Billings { get; set; }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}