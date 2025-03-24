using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.1f;
    public BaseController baseController;
    public StatHandler _statHandler;
    
    private float timeSinceLastChange = float.MaxValue;
    
    public float CurrentHealth { get; set; }
    public float MaxHealth => _statHandler.Health;
    

    private void Start()
    {
        CurrentHealth = _statHandler.Health;
    }

    private void Update()
    {
        timeSinceLastChange += Time.deltaTime;
    }
    
    public bool ChangeHealth(float change)
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }
        
        timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;
        
        if (CurrentHealth <= 0f)
        {
            Death();
        }
        return true;
    }

    public void GetExp(int exp)
    {
        _statHandler.Exp += exp;
    }

    private void Death()
    {
        baseController.Death();
    }
}