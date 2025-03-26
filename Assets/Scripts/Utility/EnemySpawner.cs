using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public StageDifficulty difficulty;

    [System.Serializable]
    public class EnemyList
    {
        public List<GameObject> enemies;
    }

    public EnemyList easyEnemies;
    public EnemyList normalEnemies;
    public EnemyList hardEnemies;

    private List<GameObject> activeEnemies = new List<GameObject>();
    private int totalSpawnedEnemies = 0;
    private int maxEnemies;

    private float spawnInterval = 3f;
    
    
    public void SpawnEnemies(int enemyCount)
    {   
        StartCoroutine(SpawnEnemiesCoroutine(enemyCount));
    }
    private IEnumerator SpawnEnemiesCoroutine(int enemyCount)
    {
        maxEnemies = enemyCount;
        List<GameObject> selectedEnemies = GetEnemiesByDifficulty();
        if (selectedEnemies.Count == 0) yield break;

        while (totalSpawnedEnemies < maxEnemies)
        {
            int spawnAmount = Mathf.Min(5, maxEnemies - totalSpawnedEnemies); // 한 번에 5마리씩 소환
            for (int i = 0; i < spawnAmount; i++)
            {
                int enemyIndex = Random.Range(0, selectedEnemies.Count);
                int spawnIndex = Random.Range(0, spawnPoints.Length);

                GameObject enemy = Instantiate(selectedEnemies[enemyIndex], spawnPoints[spawnIndex].position, Quaternion.identity);
                activeEnemies.Add(enemy);
                totalSpawnedEnemies++;

                EnemyController enemyController = enemy.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    enemyController.OnDeath += () => OnEnemyDeath(enemy);
                }
            }

            yield return new WaitForSeconds(spawnInterval);
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

    private void OnEnemyDeath(GameObject enemy)
    {
        activeEnemies.Remove(enemy);

        if (activeEnemies.Count == 0 && totalSpawnedEnemies >= maxEnemies)
        {
            StageManager.Instance.ClearStage();
        }
    }
}