using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float radius;
    private List<Vector3> spawnPositions = new List<Vector3>();
    int waitingTime = 5, spawnCount;
    public List<GameObject> notPooledObjectsEnemy = new List<GameObject>();
    public static EnemySpawner instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        CreateSpawnPositions();
    }

    private void CreateSpawnPositions()
    {
        for (int i = 0; i < 10; i++)
        { 
            var angle = 2 * Mathf.PI / 10 * i;
            var vertical = Mathf.Sin(angle);
            var horizontal = Mathf.Cos(angle);
            var spawnDir = new Vector3(horizontal, 0, vertical);
            var spawnPos = spawnDir * radius;
            spawnPositions.Add(spawnPos);
        }
        //SpawnEnemy();
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitingTime);
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        int i = Random.Range(0, 10);
        if (PoolingManager.instance.pooledObjectsEnemy.Count == 0)
        {
            var obje=Instantiate(enemy, spawnPositions[Random.Range(0,10)], Quaternion.identity);
            notPooledObjectsEnemy.Add(obje);
        }
        else
        {
            var obj = PoolingManager.instance.pooledObjectsEnemy.Last();
            PoolingManager.instance.pooledObjectsEnemy.Remove(obj);
            obj.gameObject.SetActive(true);
            notPooledObjectsEnemy.Add(obj);
            obj.transform.position = spawnPositions[Random.Range(0, 10)];
            obj.GetComponent<Enemy>().MoveToCenter();
        }
        spawnCount++;
        if (spawnCount % 5 == 0 && waitingTime > 2)
        {
            waitingTime--;
        }
    }
}