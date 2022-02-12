using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shop
{
    public class PurchaseController : BaseController
    {
        #region Fields

        private readonly IShop _shop;

        private readonly ProfilePlayer _profilePlayer;
        private readonly List<ShopProduct> _products;

        #endregion

        #region ClassLifeCycles

        public PurchaseController(IShop shop, ProfilePlayer profilePlayer, List<ShopProduct> products)
        {
            _profilePlayer = profilePlayer;
            _shop = shop;
            _products = products;

            _shop.OnSuccessPurchase.SubscribeOnChangeT(OnSuccsessPurchase);
        }

        #endregion

        #region Methods

        private void OnSuccsessPurchase(string id)
        {
            var product = _products.Find(p => p.Id == id);
            if (product == null)
                Debug.LogError("Product unknown");

            HandlePurchase(product.ProductModification);
        }

        private void HandlePurchase(ProductModification productModification)
        {
            switch (productModification.ResourceType)
            {
                case ResourceType.None:
                    break;
                case ResourceType.Gold:
                    _profilePlayer.Gold.Value += productModification.Count;
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}