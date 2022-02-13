using Assets.Scripts.Data;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class GameController : BaseController
{
    public GameController(ProfilePlayer profilePlayer, IReadOnlyList<AbilityItemConfig> configs, InventoryModel inventoryModel, Transform placeForUI)
    {
        var leftMoveDiff = new SubscriptionProperty<float>();
        var rightMoveDiff = new SubscriptionProperty<float>();
        
        var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
        AddController(tapeBackgroundController);
        
        var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
        AddController(inputGameController);
            
        var carController = new CarController();
        AddController(carController);

        var abilityRepository = new AbilityRepository(configs, profilePlayer.CurrentCar.AbilityListener);
        var abilityViewPrefab = ResourceLoader.LoadPrefab(new ResourcePath() { PathResource = "Prefabs/AbilitiesGroupView" });
        var abilityGroupView = GameObject.Instantiate(abilityViewPrefab, placeForUI).GetComponent<AbilitiesGroupView>();
        var abilitiesController = new AbilitiesController(carController, inventoryModel, abilityRepository,
            abilityGroupView);
        AddController(abilitiesController);

    }
}

