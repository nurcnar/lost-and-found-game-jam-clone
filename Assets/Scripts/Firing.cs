using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Firing : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    public float cooldown, cooldownTime = .75f;
    public static Firing instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        cooldown = Mathf.Clamp(cooldown, 0, cooldown);

        if (cooldown == 0 && Input.GetMouseButtonDown(0))
        {
            Fire();
        }   
    }

    void Fire()
    {
        FindObjectOfType<PlayerMovement>().animator.SetTrigger("Shoot");
        if (PoolingManager.instance.pooledObjectsBullet.Count < 2)
        {
            Instantiate(bulletPrefab, transform.position - transform.right * .1f, Quaternion.identity).GetComponent<Rigidbody>().AddForce(transform.forward * 1500);       
            Instantiate(bulletPrefab, transform.position + transform.right * .1f, Quaternion.identity).GetComponent<Rigidbody>().AddForce(transform.forward * 1500);
        }
        else
        {
            var obje = PoolingManager.instance.pooledObjectsBullet.Last();
            PoolingManager.instance.pooledObjectsBullet.Remove(obje);
            var obje2 = PoolingManager.instance.pooledObjectsBullet.Last();
            PoolingManager.instance.pooledObjectsBullet.Remove(obje2);

            obje.GetComponent<Rigidbody>().velocity = Vector3.zero;
            obje.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            obje2.GetComponent<Rigidbody>().velocity = Vector3.zero;
            obje2.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            obje.transform.position = transform.position - transform.right * .1f;
            obje2.transform.position = transform.position + transform.right * .1f;

            obje.transform.rotation = Quaternion.identity;
            obje2.transform.rotation = Quaternion.identity;

            obje.gameObject.SetActive(true);
            obje2.gameObject.SetActive(true);

            obje.GetComponent<Rigidbody>().AddForce(transform.forward * 1500);
            obje2.GetComponent<Rigidbody>().AddForce(transform.forward * 1500);
            obje.GetComponent<Bullet>().InvokeAddToPool();
            obje2.GetComponent<Bullet>().InvokeAddToPool();

            obje.GetComponent<TrailRenderer>().enabled=(true);
            obje2.GetComponent<TrailRenderer>().enabled = (true);
        }
        cooldown = cooldownTime;
    }
}