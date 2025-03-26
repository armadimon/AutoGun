using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropItem
{
    public ItemData item;
    public float dropChance; // 0 ~ 100;
}

[CreateAssetMenu(fileName = "EnemyDropTable", menuName = "Enemies/DropTable")]
public class EnemyDropTable : ScriptableObject
{
    public List<DropItem> dropItems;
    public int dropGoldAmount;
    public ItemData GetDroppedItem()
    {
        float totalChance = 0f;
        foreach (var drop in dropItems)
        {
            totalChance += drop.dropChance;
        }

        float roll = Random.Range(0f, totalChance);
        foreach (var drop in dropItems)
        {
            roll -= drop.dropChance;
            if (roll <= 0)
            {
                return drop.item;
            }
        }
        return null;
    }
    
}