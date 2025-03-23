using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum Uitype { EXP, HealthBar, BossHealthBar}

    public Uitype uitype;
    public StatHandler StatHandler;
    public ResourceController ResourceController;

    public Image foreGroundImage;
    private float MaxHP;
    private float CurrentHp;

    private void Start()
    {
        Debug.Log(MaxHP);
        Debug.Log(CurrentHp);
        Debug.Log(CurrentHp / MaxHP);
        // playerStatHandler = player.GetComponent<StatHandler>();
    }

    private void LateUpdate()
    {
        switch (uitype)
        {
            case Uitype.HealthBar:
                foreGroundImage.fillAmount = Mathf.Clamp01(ResourceController.CurrentHealth / StatHandler.Health);
                break;
        //     case Uitype.EXP:
        //         int level = playerStatHandler.Level;
        //         float MaxExp = playerStatHandler.RequiredExp;
        //         float current = playerStatHandler.Exp;
        //         
        //         if( current >= MaxExp)
        //         {
        //             current -= MaxExp;
        //         }
        //         foreGroundImage.fillAmount = current / MaxExp;
        //         break;
        }
    }
}
