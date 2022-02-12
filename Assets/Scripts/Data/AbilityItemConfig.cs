using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityItem", menuName = "AbilityItem", order = 0)]
public class AbilityItemConfig : ScriptableObject
{
    public ItemConfig Item;
    public GameObject View;
    public AbilityType Type;
    public Sprite Sprite;
    public float value;
    public int Id => Item.Id;
}