using Tools;
namespace Shop
{
    public interface IShop
    {
        void Buy(string id);
        string GetCost(string productID);
        void RestorePurchase();
        IReadOnlySubscriptionActionT<string> OnSuccessPurchase { get; }
        IReadOnlySubscriptionAction OnFailedPurchase { get; }
    }
}