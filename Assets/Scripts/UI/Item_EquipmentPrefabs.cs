using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemEquipmentPrefab : MonoBehaviour
{
    public Image icon;
    public ItemInfoBG infoBG;
    public TextMeshProUGUI value;
    public TextMeshProUGUI level;
    public Button selectButton;
    public EquipmentData equipmentData;
    public int index;

    private void Start()
    {
        infoBG = InventoryManager.Instance.infoBG;
    }
    
    public void SetData(EquipmentData newEquipment)
    {
        Debug.Log("newEq" + newEquipment);
        icon.sprite = newEquipment.icon;
        equipmentData = newEquipment;
        value.text = newEquipment.baseValue.ToString();
        level.text = newEquipment.level.ToString();
        if (selectButton != null)
            selectButton.onClick.AddListener(() => InventoryManager.Instance.SelectItem(this, newEquipment));
    }

    public void ClearData()
    {
        icon.sprite = null;
        equipmentData = null;
        value.text = "";
    }
    

}
