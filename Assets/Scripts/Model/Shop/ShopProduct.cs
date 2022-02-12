using System;
using UnityEngine.Purchasing;

namespace Shop
{
    [Serializable]
    public class ShopProduct
    {
        public string Id;
        public ProductType CurrentProductType;
        public ProductModification ProductModification;
    }
}