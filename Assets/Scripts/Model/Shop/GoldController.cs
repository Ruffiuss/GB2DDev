using Model.Shop;
using System;
using UnityEngine;


namespace Shop
{
    public class GoldController : BaseController
    {
        #region Fields

        private ProfilePlayer _profilePlayer;
        private GoldBalanceView _view;
        private IShop _shop;

        #endregion

        #region Properties

        public event Action<ushort> OnGoldCountChange;
        public Action<GoldBalanceView> OnViewLoaded;
        public Action OnFailedPurchase;
        public Action OnSuccessfulPurchase;

        #endregion


        #region ClassLifeCycles

        public GoldController(ProfilePlayer profile, IShop shop)
        {
            _profilePlayer = profile;
            _profilePlayer.RegisterGoldController(this);
            OnViewLoaded += ViewLoaded;
            _shop = shop;
        }

        #endregion

        #region Methods

        private void ViewLoaded(GoldBalanceView view)
        {
            _view = view;
            OnViewLoaded -= ViewLoaded;
            _view.Init("Gold balance: ", _profilePlayer.CurrentGoldCount);

            OnFailedPurchase += FailedPurchaseActionHandle;
            OnSuccessfulPurchase += SuccessfullPurchaseActionHandle;

            _shop.OnSuccessPurchase.SubscribeOnChange(OnSuccessfulPurchase);
            _shop.OnFailedPurchase.SubscribeOnChange(OnFailedPurchase);
        }

        public void FailedPurchaseActionHandle()
        {
            Debug.Log("Failed purchase");
        }
        private void SuccessfullPurchaseActionHandle()
        {
            OnGoldCountChange.Invoke((ushort)10);
        }

        public void HandleGoldTransaction(GoldTransaction type, ushort count)
        {

        }

        protected override void OnDispose()
        {
            _shop.OnFailedPurchase.UnSubscriptionOnChange(OnSuccessfulPurchase);
            _shop.OnFailedPurchase.UnSubscriptionOnChange(OnFailedPurchase);
            OnFailedPurchase -= FailedPurchaseActionHandle;
            OnSuccessfulPurchase -= SuccessfullPurchaseActionHandle;
            _profilePlayer.UnregisterGoldController(this);
            GameObject.Destroy(_view);
            base.OnDispose();
        }

        #endregion
    }
}
