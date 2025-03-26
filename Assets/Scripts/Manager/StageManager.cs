using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class StageManager : MonoBehaviour
    {
        private static StageManager _instance;
        public static StageManager Instance { get { return _instance; } }

        public PlayerController playerPrefab;
        public EnemySpawner enemySpawner;
        public StageData currentStage;
        public GameObject clearPanel;
        public GameObject enemyPrefab;
        public Vector3 center; // 박스 중심 위치
        public Vector3 size;   // 박스 크기

        
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
        
        public void ClearStage()
        {
            clearPanel.SetActive(true);
        }
        
        private void Start()
        {
            if (SceneLoader.SelectedStage != null)
            {
                currentStage = SceneLoader.SelectedStage;
                ApplyStageSettings();
                playerPrefab.transform.position = new Vector3(2, 0, 0);
            }
            else
            {
                Debug.LogError("StageData is missing!");
            }
        }

        private void ApplyStageSettings()
        {
            // 난이도에 따라 적 종류, 목표 수, 보상을 설정
            Debug.Log($"Stage: {currentStage.stageName}, Difficulty: {currentStage.difficulty}");
            enemySpawner.SpawnEnemies(currentStage.enemyCount);
            enemySpawner.difficulty = currentStage.difficulty;
            // SetRewards(currentStage.rewards);
        }
        
    }


