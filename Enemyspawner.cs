using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Spawn();
            timer = spawnInterval;
        }
    }

    void Spawn()
    {
        if (enemyPrefab == null) return;
        float camH = Camera.main.orthographicSize + 1f;
        float camW = Camera.main.orthographicSize * Camera.main.aspect + 1f;
        Vector2[] edges = {
            new Vector2(Random.Range(-camW, camW), camH),
            new Vector2(Random.Range(-camW, camW), -camH),
            new Vector2(-camW, Random.Range(-camH, camH)),
            new Vector2(camW, Random.Range(-camH, camH))
        };
        Instantiate(enemyPrefab, edges[Random.Range(0, 4)], Quaternion.identity);
    }
}