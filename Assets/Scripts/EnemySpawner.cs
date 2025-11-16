using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyHigh;
    public GameObject enemyMid;
    public GameObject enemyLow;

    public Transform spawnHigh;
    public Transform spawnMid;
    public Transform spawnLow;

    public float spawnInterval = 2f;
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        int type = Random.Range(0, 3); // 0 = High, 1 = Mid, 2 = Low
        GameObject enemyToSpawn = null;
        Transform spawnPoint = null;

        switch (type)
        {
            case 0:
                enemyToSpawn = enemyHigh;
                spawnPoint = spawnHigh;
                break;
            case 1:
                enemyToSpawn = enemyMid;
                spawnPoint = spawnMid;
                break;
            case 2:
                enemyToSpawn = enemyLow;
                spawnPoint = spawnLow;
                break;
        }

        Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);
    }
}
