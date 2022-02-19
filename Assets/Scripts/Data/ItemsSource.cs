using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = nameof(ItemsSource), menuName = "ItemsSource")]
    public class ItemsSource : BaseDataSource<ItemConfig>
    {

    }


    [CreateAssetMenu(fileName = nameof(UpgradeItemConfig), menuName = "UpgradesSource")]
    public class UpgradesSource : BaseDataSource<UpgradeItemConfig>
    {

    }

    [CreateAssetMenu(fileName = nameof(AbilityItemConfig), menuName = "AbilityItemsSource")]
    public class AbilityItemsSource : BaseDataSource<AbilityItemConfig>
    {

    }
}