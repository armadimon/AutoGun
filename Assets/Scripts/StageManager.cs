using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static StageManager _instance;
    public static StageManager Instance { get { return _instance; } }

    public PlayerController playerPrefab;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    // 각각의 에어리어마다 적이 등장할 수 있는 영역이 정해져 있고, 플레이어가 일정 이상의 적을 처치하기 전까지 적을 계속 스폰함.
    void StartStage()
    {
        
    }
}
