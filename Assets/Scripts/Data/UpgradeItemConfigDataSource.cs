using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeItemConfigDataSource", menuName = "UpgradeItemConfigDataSource")]
public class UpgradeItemConfigDataSource : ScriptableObject
{
    [SerializeField]
    private UpgradeItemConfig[] _itemConfigs;

    public UpgradeItemConfig[] ItemConfigs => _itemConfigs;
}

public class BaseDataSource<T> : ScriptableObject where T:ScriptableObject
{
    [SerializeField] private T[] _content;

    public T[] Content => _content;

}