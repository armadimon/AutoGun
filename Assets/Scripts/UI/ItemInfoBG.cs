using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoBG : MonoBehaviour
{
    // public Image selectedItemIcon;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    
    public Button upgradeButton;
    public Button useButton;
    public Button equipButton;
    
    // BuildObject 데이터를 받아 UI를 업데이트하는 함수
    public void SetItemData(EquipmentData itemData)
    {
        // 나머지 정보 업데이트
        // selectedItemIcon.sprite = itemData.icon;
        selectedItemName.text = itemData.itemName;
        selectedItemDescription.text = itemData.description;

        upgradeButton.enabled = true;
        equipButton.enabled = true;
        useButton.enabled = false;
    }
    
    public void SetItemData(ItemData itemData)
    {
        // 나머지 정보 업데이트
        // selectedItemIcon.sprite = itemData.icon;
        selectedItemName.text = itemData.name;
        selectedItemDescription.text = itemData.description;
        
        upgradeButton.enabled = false;
        equipButton.enabled = false;
        useButton.enabled = true;
    }

    public void ClearItemData()
    {
        // selectedItemIcon.sprite = null;
        selectedItemName.text = "";
        selectedItemDescription.text = "";
        upgradeButton.enabled = false;
        equipButton.enabled = false;
        useButton.enabled = false;
    }
}
