using InOut.Service.Interfaces;

namespace InOut.Service.Entities
{
    public class BillingService : IBillingService
    {
        public void AddProductToBilling(long billingId, long productId)
        {
            throw new NotImplementedException();
        }

        public void RemoveProductFromBilling(long billingId, long productId)
        {
            throw new NotImplementedException();
        }

        public decimal GetBillingValue(long billingId)
        {
            throw new NotImplementedException();
        }
    }
}