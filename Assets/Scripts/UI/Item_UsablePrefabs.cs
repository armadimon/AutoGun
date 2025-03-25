using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUsablePrefab : MonoBehaviour
{
    public Image icon;
    public int quantity = 0;
    public TextMeshProUGUI quantityText;
    public ItemInfoBG infoBG;
    public Button selectButton;
    public ItemData UsableItem;
    public string itemName;

    private void Start()
    {
        infoBG = InventoryManager.Instance.infoBG;
    }

    public void UpdateUI()
    {
        if (quantity <= 0)
        {
            quantityText.gameObject.SetActive(false);
            return ;
        }
        quantityText.gameObject.SetActive(true);
        quantityText.text = quantity.ToString();
    }
    
    public void SetData(ItemData newItem)
    {
        itemName = newItem.itemName;
        icon.sprite = newItem.icon;
        UsableItem = newItem;
        selectButton.onClick.AddListener(() => InventoryManager.Instance.SelectItem(newItem));
    }

    public void ClearData()
    {
        itemName = null;
        icon.sprite = null;
        UsableItem = null;
    }
    
}
