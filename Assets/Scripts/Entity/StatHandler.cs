using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{

    [SerializeField]
    private float health = 100;
    public float Health
    {
        get => health;
        set => health = value;
    }

    [Range(0, 100)][SerializeField] private int exp = 0;

    [SerializeField] private int level = 1;
    [SerializeField] private int requiredExp = 100;

    public int Level
    {
        get => level;
        private set
        {
            level = value;
            Debug.Log("Current Level: " + level);
        }
    }
    
    public int RequiredExp
    {
        get => requiredExp;
        private set => requiredExp = value;
    }


    public int Exp
    {
        get => exp;
        set
        {
            exp = Mathf.Clamp(value, 0, 10000);

            if (exp >= requiredExp)
            {
                LevelUp();
            }
        }
    }

    private void LevelUp()
    {
        exp -= requiredExp;
        Level++;
        requiredExp = level * requiredExp;
        Debug.Log("New Level: " + level + ", Exp: " + requiredExp);
    }


    [Range(1f, 20f)][SerializeField]
    private float speed = 3f;

    public float Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(value, 0, 20);
    }

    [SerializeField] private int gold = 0;

    public int Gold
    {
        get => gold;
        set => gold = value;
    }
}