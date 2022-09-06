using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        InvokeAddToPool();
    }

    private void OnTriggerEnter(Collider other)
    {      
        if(other.tag!="player" && other.tag != "bullet")
        {
            PoolingManager.instance.AddObjectToPoolBullet(gameObject);
        }
        print(other.tag);
    }

    public void InvokeAddToPool()
    {
        Invoke("AddToPool", 3f);

    }

    public void AddToPool()
    {
        PoolingManager.instance.AddObjectToPoolBullet(gameObject);
    }

}
