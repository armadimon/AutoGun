using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    
    [Header("UI component")]
    public GameObject backgroundPanel;
    public GameObject inventoryMenu;
    public GameObject equipmentMenu;
    public Transform equipmentMenuContent;
    public GameObject usableMenu;
    public Transform usableMenuContent;
    public ItemInfoBG infoBG;
    public GoldInfo GoldInfo;
    public int GoldAmount;
    
    
    [Header("Equipment UI")]
    public EquipmentData equippedWeapon;
    public EquipmentData equippedArmor;
    public ItemEquipmentPrefab currentWeaponDisplay;
    public ItemEquipmentPrefab currentArmorDisplay;
    
    public ItemEquipmentPrefab itemEquipmentPrefab;
    public ItemUsablePrefab itemUsablePrefab;

    public HealPotion tempItem;
    
    public ItemData selectedItem;
    
    public List<ItemEquipmentPrefab> equipmentInventory = new List<ItemEquipmentPrefab>();
    public List<ItemUsablePrefab> usableInventory = new List<ItemUsablePrefab>();
    
    
    private ItemEquipmentPrefab _selectedItemUI;
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
        AddItem(currentWeapon.data);
        // AddItem(tempItem.ItemData);
        equippedWeapon = currentWeapon.data;
    }



    public void EquipWeapon(EquipmentData newWeapon)
    {
        if (CharacterManager.Instance != null)
        {
            GameObject tempIntance = Instantiate(newWeapon.equipmentPrefab, CharacterManager.Instance.player.weaponPos);
            Weapon weaponIntance = tempIntance.gameObject.GetComponent<Weapon>();
            if (weaponIntance != null)
            {
                Debug.Log("Check");
                if (CharacterManager.Instance.player.currentWeapon != null)
                    Destroy(CharacterManager.Instance.player.currentWeapon.gameObject);
                currentWeapon = weaponIntance;
                CharacterManager.Instance.player.currentWeapon = weaponIntance;
            }
        }
        equippedWeapon = newWeapon;
        currentWeaponDisplay.SetData(newWeapon);
        OnWeaponChanged?.Invoke(newWeapon);
    }

    public void AddItem(ItemData item)
    {
        if (item.itemType == ItemType.Equipment)
        {
            ItemEquipmentPrefab equipmentItem = Instantiate(itemEquipmentPrefab, equipmentMenuContent);
            equipmentItem.SetData(item as EquipmentData);
            equipmentInventory.Add(equipmentItem);
            Debug.Log($"Equipment 인벤토리에 {item.itemName} 추가됨");
        }
        else if (item.itemType == ItemType.Usable)
        {   
            ItemUsablePrefab existingItem = usableInventory.Find(u => u.itemName == item.itemName);

            if (existingItem != null)
            {
                Debug.Log($"이미 인벤토리에 존재: {item.itemName}, 수량 증가");
                existingItem.quantity++;
                existingItem.UpdateUI();
            }
            else
            {
                ItemUsablePrefab usableItem = Instantiate(itemUsablePrefab, usableMenuContent);
                usableItem.SetData(item);
                usableInventory.Add(usableItem);
                Debug.Log($"Usable 인벤토리에 {item.itemName} 추가됨");
            }
        }
    }
    
    public void AddGold(int amount)
    {
        GoldAmount += amount;
        GoldInfo.UpdateGoldUI(GoldAmount); // UI 업데이트
    }
    
    public void OnEquipButton()
    {
        if (selectedItem is EquipmentData equippedItemData)
        {
            if (currentWeapon != null)
            {
                UnequipWeapon();
                EquipWeapon(equippedItemData);
            }
            else
            {
                EquipWeapon(equippedItemData);
            }
        }
    }

    public void OnUpgradeButton()
    {
        if (selectedItem is EquipmentData equippedItemData)
        {
            if (GoldAmount >= 100)
            {
                _selectedItemUI.SetData(equippedItemData);
                GoldAmount -= 100;
                equippedItemData.level++;
            }
            else
            {
                Debug.Log("골드가 부족합니다! : " + GoldAmount);
            }
        }
    }
    
    public void OnUseButton()
    {
        Usable usable = selectedItem.usablePrefab.gameObject.GetComponent<Usable>();
        if (usable != null)
        {
            usable.UseItem();
        }
    }
    
    //--------------------------------------------------------------------
    //Utility
    
    // 장비 아이템 셀렉트시


    
    // 소비 아이템 셀렉트시
    public void SelectItem(ItemEquipmentPrefab equipmentUIPrefab, ItemData itemData)
    {
        if (itemData.itemType == ItemType.Equipment)
        {
            infoBG.SetEquipItemData(itemData as EquipmentData);
            
        }
        else if (itemData.itemType == ItemType.Usable)
        {
            infoBG.SetUsableItemData(itemData);
        }
        _selectedItemUI = equipmentUIPrefab;
        selectedItem = itemData;
    }
    
    public void UnequipWeapon()
    {
        currentWeaponDisplay.ClearData();
        currentWeapon = null;
        equippedWeapon = null;
    }

    public void InventoryToggle()
    {
        inventoryMenu.SetActive(!inventoryMenu.activeSelf);
        backgroundPanel.SetActive(!backgroundPanel.activeSelf);
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