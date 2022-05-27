namespace InOut.Domain.Entities
{
    public class BusinessBilling
    {
        public long BusinessId { get; set; }
        public Business Business { get; set; }

        public long BillingId { get; set; }
        public Billing Billing { get; set; }
    }
}