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
    public Button selectButton;
    public EquipmentData equipmentData;
    // public BuildObject buildObject;
    public int index;

    private void Start()
    {
        // infoBG = GameObject.Find("BuildInfoBG").gameObject;
        infoBG = InventoryManager.Instance.infoBG;
    }
    
    public void SetData(EquipmentData newEquipment)
    {
        icon.sprite = newEquipment.icon;
        equipmentData = newEquipment;
        value.text = newEquipment.baseValue.ToString();
        selectButton.onClick.AddListener(() => InventoryManager.Instance.SelectItem(newEquipment));
    }

    public void ClearData()
    {
        icon.sprite = null;
        equipmentData = null;
        value.text = "";
    }
    
    // public void SetInfoBG()
    // {
        // SetItemDiscriptionPosition();
        // infoBG.SetItemData(equipmentData);
    // }
    // void SetItemDiscriptionPosition()
    // {
    //     Vector3 mousePos = Input.mousePosition;
    //     RectTransform rectDescrition = infoBG.GetComponent<RectTransform>();
    //
    //     float offsetX = rectDescrition.rect.width / 2;
    //     float offsetY = rectDescrition.rect.height / 2;
    //
    //     mousePos.x += offsetX + 20;
    //     for (int i = 0; i < 3; i++)
    //     {
    //         if (mousePos.y - offsetY < 0)
    //         {
    //             mousePos.y += offsetY / 2;
    //         }
    //         else if (mousePos.y + offsetY > Screen.height)
    //         {
    //             mousePos.y -= offsetY / 2;
    //         }
    //     }
    //     infoBG.transform.position = mousePos;
    // }
    //
    //
    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     infoBG.gameObject.SetActive(true);
    //     SetInfoBG();
    // }
    //
    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     infoBG.gameObject.SetActive(false);
    // }
}
