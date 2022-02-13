using System.Collections.Generic;
using Tools;

public class ItemsRepository : BaseController, IRepository<int, IItem>
{
    public IReadOnlyDictionary<int, IItem> Content => _itemsMapById;

    private Dictionary<int, IItem> _itemsMapById = new Dictionary<int, IItem>();

    public ItemsRepository(List<ItemConfig> itemConfigs)
    {
        PopulateItems(itemConfigs);
    }

    protected override void OnDispose()
    {
        _itemsMapById.Clear();
    }

    private void PopulateItems(List<ItemConfig> configs)
    {
        foreach (var config in configs)
        {
            if (_itemsMapById.ContainsKey(config.Id))
                continue;

            _itemsMapById.Add(config.Id, CreateItem(config));
        }
    }

    private IItem CreateItem(ItemConfig itemConfig)
    {
        return new Item 
        { 
            Id = itemConfig.Id, 
            Info = new ItemInfo { Title = itemConfig.Title } 
        };
    }
}


