using Features.AbilitiesFeature;
using System.Collections;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "AbilityItem", menuName = "AbilityItem", order = 0)]
    public class AbilityItemConfig : ScriptableObject, IConfig
    {
        public ItemConfig Item;
        public GameObject View;
        public AbilityType Type;
        public float value;
        public int Id => Item.Id;
    }
}