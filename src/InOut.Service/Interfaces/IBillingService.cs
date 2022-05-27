namespace InOut.Service.Interfaces
{
    public interface IBillingService
    {
        void AddProductToBilling(long billingId, long productId);

        void RemoveProductFromBilling(long billingId, long productId);

        decimal GetBillingValue(long billingId);
    }
}