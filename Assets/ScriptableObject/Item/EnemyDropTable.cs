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
        float roll = Random.Range(0f, 100f);
        float cumulativeChance = 0f;
        Debug.Log(roll);
        foreach (var drop in dropItems)
        {
            cumulativeChance += drop.dropChance;
            if (roll <= cumulativeChance)
            {
                return drop.item;
            }
        }
        return null;
    }
    
}