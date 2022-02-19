using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "UpgradeItem", menuName = "UpgradeItem")]
    public class UpgradeItemConfig : ScriptableObject, IConfig
    {
        [SerializeField] private ItemConfig _itemConfig;
        [SerializeField] private UpgradeType _upgradeType;
        [SerializeField] private int _valueUpgrade;
        public int Id => _itemConfig.Id;
        public UpgradeType UpgradeType => _upgradeType;
        public int ValueUpgrade => _valueUpgrade;
    }

    public enum UpgradeType
    {
        None,
        Speed,
        Control
    }
}
