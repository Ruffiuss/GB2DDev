using System;


namespace Shop
{
    [Serializable]
    public class ProductModification
    {
        #region Fields

        public ResourceType ResourceType;
        public int Count;

        #endregion
    }

    public enum ResourceType
    {
        None = 0,
        Gold = 1
    }
}
