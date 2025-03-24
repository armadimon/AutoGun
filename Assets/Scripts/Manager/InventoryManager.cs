using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public static Action<WeaponData> OnWeaponChanged; // 무기 변경 이벤트
    
    // 무기 장착 UI
    // 무기 아이콘 프리팹
    
    private WeaponData currentWeapon;
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


    public void EquipWeapon()
    {
        OnWeaponChanged?.Invoke(currentWeapon); // 이벤트 호출
    }
    public void EquipWeapon(WeaponData newWeapon)
    {
        currentWeapon = newWeapon;
        OnWeaponChanged?.Invoke(newWeapon); // 이벤트 호출
    }
    
}