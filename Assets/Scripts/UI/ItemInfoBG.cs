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
    public void SetEquipItemData(EquipmentData itemData)
    {
        // 나머지 정보 업데이트
        // selectedItemIcon.sprite = itemData.icon;
        selectedItemName.text = itemData.itemName;
        selectedItemDescription.text = itemData.description;

        upgradeButton.gameObject.SetActive(true);
        equipButton.gameObject.SetActive(true);
        useButton.gameObject.SetActive(false);
    }
    
    public void SetUsableItemData(ItemData itemData)
    {
        // 나머지 정보 업데이트
        // selectedItemIcon.sprite = itemData.icon;
        Debug.Log("Usable Set");
        selectedItemName.text = itemData.name;
        selectedItemDescription.text = itemData.description;
        
        upgradeButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
        useButton.gameObject.SetActive(true);
    }

    public void ClearItemData()
    {
        // selectedItemIcon.sprite = null;
        selectedItemName.text = "";
        selectedItemDescription.text = "";
        upgradeButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
        useButton.gameObject.SetActive(false);
    }
}
