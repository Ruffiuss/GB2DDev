using System;
using Tools;
using UnityEngine.Purchasing;

namespace Model.Shop
{
    public interface IShop
    {
        IReadOnlySubscriptionAction OnSuccessPurchase { get; }
        IReadOnlySubscriptionAction OnFailedPurchase { get; }

        void Buy(string id);
        string GetCost(string productID);
        void RestorePurchase();
        void RegisterShopButton(IAPButton button);
    }
}