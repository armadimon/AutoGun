using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : MonoBehaviour, Usable
{
    [SerializeField]
    private ItemData itemData;
    public ItemData ItemData { get => itemData; }

    public void UseItem()
    {
        if (CharacterManager.Instance != null)
        {
            CharacterManager.Instance.player.resourceController.ChangeHealth(10);
            Debug.Log("체력 회복!");
        }
        else
        {
            Debug.LogWarning("No Character Manager");
        }
    }
}
