using System;
using System.Collections.Generic;
using Tools;
using UnityEngine.Purchasing;


namespace Model.Shop
{

    public class ShopTools : IShop, IStoreListener
    {
        #region Fields

        private readonly SubscriptionAction _onSuccessPurchase;
        private readonly SubscriptionAction _onFailedPurchase;
        private readonly UnityPurchasing _unityPurchasing;

        private IStoreController _controller;
        private IExtensionProvider _extensionProvider;
        private List<ShopProduct> _products = new List<ShopProduct>();

        private bool _isInitialized;

        public Action<IAPButton> OnButtonRegister;

        #endregion

        #region Properties

        public IReadOnlySubscriptionAction OnSuccessPurchase => _onSuccessPurchase;
        public IReadOnlySubscriptionAction OnFailedPurchase => _onFailedPurchase;

        #endregion

        #region ClassLifeCycles

        public ShopTools(List<ShopProduct> products)
        {
            _onSuccessPurchase = new SubscriptionAction();
            _onFailedPurchase = new SubscriptionAction();
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            foreach (ShopProduct product in products)
            {
                _products.Add(product);
                builder.AddProduct(product.Id, product.CurrentProductType);
            }
            UnityPurchasing.Initialize(this, builder);

            OnButtonRegister += RegisterShopButton;
        }

        ~ShopTools()
        {
            OnButtonRegister -= RegisterShopButton;
        }

        #endregion

        #region Methods

        public void Buy(string id)
        {
            if (!_isInitialized)
                return;
            _controller.InitiatePurchase(id);
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            _isInitialized = false;
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            bool validPurchase = false;
            //#if UNITY_ANDROID || UNITY_IOS
            //            CrossPlatformValidator validator = new CrossPlatformValidator(GooglePlayTangle.Data(),
            //                AppleTangle.Data(), Application.identifier);
            //            try
            //            {
            //                IPurchaseReceipt[] result = validator.Validate(purchaseEvent.purchasedProduct.receipt);
            //                validPurchase = true;
            //                foreach (IPurchaseReceipt productReceipt in result)
            //                {
            //                    validPurchase &= productReceipt.purchaseDate == DateTime.UtcNow;
            //                }

            //            }
            //            catch (IAPSecurityException)
            //            {
            //                Debug.Log("Invalid receipt, not unlocking content");
            //                validPurchase = false;
            //            }
            //#endif
            if (validPurchase)
                _onSuccessPurchase.Invoke();
            return PurchaseProcessingResult.Complete;

        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            _onFailedPurchase.Invoke();
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _controller = controller;
            _extensionProvider = extensions;
            _isInitialized = true;
        }

        public string GetCost(string productID)
        {
            Product product = _controller.products.WithID(productID);

            if (product != null)
                return product.metadata.localizedPriceString;

            return "N/A";
        }

        public void RestorePurchase()
        {
            if (!_isInitialized)
            {
                return;
            }

#if UNITY_IOS
           _extensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions(OnRestoreTransactionFinished);
#else
            _extensionProvider.GetExtension<IGooglePlayStoreExtensions>().RestoreTransactions(OnRestoreFinished);
#endif
        }

        private void OnRestoreFinished(bool isSuccess)
        {

        }

        public void RegisterShopButton(IAPButton button)
        {
            button.onPurchaseFailed.AddListener(OnPurchaseFailed);
            button.onPurchaseComplete.AddListener(PurchaseComplete);
        }

        private void PurchaseComplete(Product product)
        {
            foreach (var p in _products)
            {
                if (product.definition.id.Equals(p.Id))
                {
                    _onSuccessPurchase.Invoke();
                }
            }
        }

        #endregion
    }
}