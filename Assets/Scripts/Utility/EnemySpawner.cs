using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints; // 적이 생성될 위치들
    public StageDifficulty difficulty; // 현재 난이도

    [System.Serializable]
    public class EnemyList
    {
        public List<GameObject> enemies; // 적 프리팹 리스트
    }

    public EnemyList easyEnemies;   // Easy 난이도 적 리스트
    public EnemyList normalEnemies; // Normal 난이도 적 리스트
    public EnemyList hardEnemies;   // Hard 난이도 적 리스트

    public void SpawnEnemies(int enemyCount)
    {
        List<GameObject> selectedEnemies = GetEnemiesByDifficulty();
        Debug.Log($"Selected enemies: {selectedEnemies.Count}");
        Debug.Log($"enemies count: {enemyCount}");
        for (int i = 0; i < enemyCount; i++)
        {
            if (selectedEnemies.Count == 0) return;

            int enemyIndex = Random.Range(0, selectedEnemies.Count); // 랜덤한 적 선택
            int spawnIndex = Random.Range(0, spawnPoints.Length);    // 랜덤한 스폰 위치 선택

            Instantiate(selectedEnemies[enemyIndex], spawnPoints[spawnIndex].position, Quaternion.identity);
        }
    }

    private List<GameObject> GetEnemiesByDifficulty()
    {
        switch (difficulty)
        {
            case StageDifficulty.Easy:
                return easyEnemies.enemies;
            case StageDifficulty.Normal:
                return normalEnemies.enemies;
            case StageDifficulty.Hard:
                return hardEnemies.enemies;
            default:
                return new List<GameObject>();
        }
    }
}