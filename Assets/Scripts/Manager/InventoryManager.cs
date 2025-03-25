using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public GameObject inventoryMenu;
    public GameObject equipmentMenu;
    public Transform equipmentMenuContent;
    public GameObject usableMenu;
    public Transform usableMenuContent;
    public ItemInfoBG infoBG;
    public EquipmentData equippedWeapon;
    public EquipmentData equippedArmor;
    
    
    public ItemEquipmentPrefab currentWeaponDisplay;
    public ItemEquipmentPrefab currentArmorDisplay;
    public ItemEquipmentPrefab itemEquipmentPrefab;
    
    public List<ItemEquipmentPrefab> equipmentInventory = new List<ItemEquipmentPrefab>();
    public List<ItemUsablePrefab> usableInventory = new List<ItemUsablePrefab>();
    
    public static Action<EquipmentData> OnWeaponChanged; // 무기 변경 이벤트

    // 무기 장착 UI
    // 무기 아이콘 프리팹

    public Weapon currentWeapon;
    // public List<Item> items = new List<Item>(); // 플레이어 인벤토리

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 변경 시에도 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AddEquipment(currentWeapon.data);
        equippedWeapon = currentWeapon.data;
    }



    public void EquipWeapon(EquipmentData newWeapon)
    {
        if (newWeapon.equipmentType == EquipmentType.Weapon && equippedWeapon != null)
        {
            UnequipWeapon();
        }

        if (CharacterManager.Instance != null)
        {
            GameObject tempIntance = Instantiate(newWeapon.equipmentPrefab, CharacterManager.Instance.player.weaponPos);
            Weapon weaponIntance = tempIntance.gameObject.GetComponent<Weapon>();
            if (weaponIntance != null)
                CharacterManager.Instance.player.currentWeapon = weaponIntance;
        }
        currentWeaponDisplay.SetData(newWeapon);
        OnWeaponChanged?.Invoke(newWeapon); // 이벤트 호출
    }

    public void AddItem(ItemData item)
    {
        if (item.itemType == ItemType.Equipment)
        {
            ItemEquipmentPrefab equipmentItem = Instantiate(itemEquipmentPrefab, equipmentMenuContent);
            equipmentItem.SetData(item as EquipmentData);
            equipmentInventory.Add(equipmentItem);
        }
        else if (item.itemType == ItemType.Usable)
        {   
            ItemEquipmentPrefab equipmentItem = Instantiate(itemEquipmentPrefab, equipmentMenuContent);
            equipmentItem.SetData(item);
            usableInventory.Add(item as UsableData);
        }

        Debug.Log($"인벤토리에 {item.itemName} 추가됨");
    }
    
    //--------------------------------------------------------------------
    //Utility
    
    // 장비 아이템 셀렉트시
    public void SelectItem(EquipmentData equipmentData)
    {
        infoBG.SetItemData(equipmentData);
    }

    
    // 소비 아이템 셀렉트시
    public void SelectItem(ItemData itemData)
    {
        infoBG.SetItemData(itemData);
    }
    
    public void UnequipWeapon()
    {
        currentWeaponDisplay.ClearData();
        equippedWeapon = null;
    }

    public void InventoryToggle()
    {
        inventoryMenu.SetActive(!inventoryMenu.activeSelf);
        infoBG.ClearItemData();
    }

    public void EquipMenuToggle()
    {
        equipmentMenu.SetActive(true);
        usableMenu.SetActive(false);
    }

    public void UsableMenuToggle()
    {
        equipmentMenu.SetActive(false);
        usableMenu.SetActive(true);
    }

}