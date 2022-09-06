using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolingManager : MonoBehaviour
{
    public List<GameObject> pooledObjectsEnemy = new List<GameObject>();
    public List<GameObject> pooledObjectsBullet = new List<GameObject>();
    public static PoolingManager instance;
    private void Awake()
    {
        instance = this;
    }
    public void AddObjectToPool(GameObject obje)
    {
        obje.gameObject.SetActive(false);
        if (!pooledObjectsEnemy.Contains(obje))
        {
            pooledObjectsEnemy.Add(obje);
        }
    }
    public void AddObjectToPoolBullet(GameObject obje)
    {
        obje.GetComponent<TrailRenderer>().enabled = (false);

        obje.gameObject.SetActive(false);
        if (!pooledObjectsBullet.Contains(obje))
        {
            pooledObjectsBullet.Add(obje);
        }
    }

    void Update()
    {
        
    }
}
