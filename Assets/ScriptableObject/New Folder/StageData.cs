using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStageData", menuName = "Stage/Stage Data")]
public class StageData : ScriptableObject
{
    public string stageName;
    public StageDifficulty difficulty; 
    public GameObject[] enemyTypes;
    public int enemyCount;
    public int rewardGold;
    public int rewardExp;
}

public enum StageDifficulty
{
    Easy,
    Normal,
    Hard
}
