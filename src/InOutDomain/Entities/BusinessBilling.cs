namespace InOut.Domain.Entities
{
    public class BusinessBilling : Base
    {
        public long BusinessId { get; set; }
        public Business Business { get; set; }

        public long BillingId { get; set; }
        public Billing Billing { get; set; }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}