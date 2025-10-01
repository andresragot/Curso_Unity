using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private ObjectPool enemyPool;
    [SerializeField] private Transform target;
    [SerializeField] private float spawnRate = 3f;
    [SerializeField] private float spawnRadius = 10f;

    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * spawnRate;

        while (timer >= 1f)
        {
            timer -= 1f;
            SpawnOne();
        }
    }

    private void SpawnOne()
    {
        Vector2 offset = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 pos = (target != null ? target.position : transform.position) + new Vector3(offset.x, offset.y, 0);

        GameObject go = enemyPool.Get(pos, Quaternion.identity);
        if (go == null) return; // Pool cerrada sin expansión.

        var enemy = go.GetComponent<EnemyFollower2D>();
        if (enemy != null) enemy.Init(target);
    }
}
