using Core;
using Data;
using Features.InventoryFeature;
using ItemFeature;
using Model;
using System.Collections.Generic;
using System.Linq;
using Tools.ResourceManagement;
using UnityEngine;

namespace Features.ShedFeature
{
    public class ShedController : BaseController, IShedController
    {

        private readonly List<UpgradeItemConfig> _upgradesConfig;
        private readonly Car _car;
        private readonly UpgradeHandlerRepository _upgradeRepository;
        private readonly InventoryController _inventoryController;
        private readonly InventoryModel _model;

        public ShedController(List<ItemConfig> items, Car car)
        {
            _upgradesConfig = ResourceLoader.LoadDataSource<UpgradeItemConfig>(
                new ResourcePath(){
                    PathResource = "Data/UpgradesSource" })
                .ToList();

            _car = car;
            _upgradeRepository = new UpgradeHandlerRepository(_upgradesConfig);

            _model = new InventoryModel();
            AddController(_upgradeRepository);
            _inventoryController = new InventoryController(items, _model);
            AddController(_inventoryController);
        }

        public void Enter()
        {
            _inventoryController.ShowInventory();
            Debug.Log($"Enter, car speed = {_car.Speed}");
        }

        public void Exit()
        {
            UpgradeCarWithEquipedItems(_car, _model.GetEquippedItems(), _upgradeRepository.Content);
            Debug.Log($"Exit, car speed = {_car.Speed}");
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
}