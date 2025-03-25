using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUsablePrefab : MonoBehaviour
{
    public Image icon;
    public ItemInfoBG infoBG;
    public Button selectButton;
    public ItemData UsableItem;
    public int index;

    private void Start()
    {
        infoBG = InventoryManager.Instance.infoBG;
    }
    
    public void SetData(ItemData newEquipment)
    {
        icon.sprite = newEquipment.icon;
        UsableItem = newEquipment;
        selectButton.onClick.AddListener(() => InventoryManager.Instance.SelectItem(newEquipment));
    }

    public void ClearData()
    {
        icon.sprite = null;
        UsableItem = null;
    }
    
}
