using System.Collections.Generic;
using UnityEngine;

public class ShedController : BaseController, IShedController
{
    private readonly IReadOnlyList<UpgradeItemConfig> _upgradeItems;
    private readonly IInventoryModel _model;
    private readonly UpgradeHandlerRepository _upgradeRepository;
    private readonly InventoryController _inventoryController;
    private readonly Transform _placeForUI;
    private readonly Car _car;
    private readonly ResourcePath _shedUIPrefabPath = new ResourcePath() { PathResource = "Prefabs/Inventory"};
    private readonly ResourcePath _shedItemPrefabPath = new ResourcePath() { PathResource = "Prefabs/InventoryItem"};

    public ShedController(IInventoryModel model, IReadOnlyList<UpgradeItemConfig> upgradeItems, List<ItemConfig> items, Car car, Transform placeForUI)
    {
        _upgradeItems = upgradeItems;
        _car = car;
        _model = model;
        _model.IsInShed = true; 
        _placeForUI = placeForUI;

        _upgradeRepository = new UpgradeHandlerRepository(upgradeItems);
        AddController(_upgradeRepository);

        _inventoryController = new InventoryController(items, _model);
        _inventoryController.InitShedUI(_placeForUI, _shedUIPrefabPath, _shedItemPrefabPath);
        AddController(_inventoryController);
    }

    public void Enter()
    {
        _inventoryController.ShowInventory();
        Debug.Log($"Enter, car speed = {_car.Speed}");
    }

    public void Exit()
    {
        UpgradeCarWithEquipedItems(_car, _model.GetEquippedItems(), _upgradeRepository.UpgradeItems);
        Debug.Log($"Exit, car speed = {_car.Speed}");
    }

    protected override void OnDispose()
    {
        _model.IsInShed = false;
        _upgradeRepository?.Dispose();
        _inventoryController?.Dispose();
    }

    private void UpgradeCarWithEquipedItems(IUpgradeableCar car,
        IReadOnlyList<IItem> equiped,
        IReadOnlyDictionary<int, IUpgradeCarHandler> upgradeHandlers)
    {
        foreach (var item in equiped)
        {
            if (upgradeHandlers.TryGetValue(item.Id, out var handler))
                handler.Upgrade(car);
        }
    }
}