using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = nameof(ItemsSource), menuName = "ItemsSource")]
    public class ItemsSource : BaseDataSource<ItemConfig>
    {

    }


    [CreateAssetMenu(fileName = nameof(UpgradesSource), menuName = "UpgradesSource")]
    public class UpgradesSource : BaseDataSource<UpgradeItemConfig>
    {

    }
}