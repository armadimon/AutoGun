using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface Usable
{
    ItemData ItemData { get; }
    void UseItem();
}
public enum ItemType
{
    Equipment,
    Usable,
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public float value;
    public Sprite icon;
    public string description;
    public GameObject usablePrefab;
}