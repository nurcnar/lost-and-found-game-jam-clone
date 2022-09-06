using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Enemy : MonoBehaviour
{
    private float healthDecreasingAmount = 25;
    private float health = 100;
    [SerializeField] private Animator animator;
    public Vector3 velocity;
    public float m;

    public bool attacking, arrived;
    public Center myTarget;

    public static Enemy instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        MoveToCenter();
    }

    private void Update()
    {
        velocity = GetComponent<NavMeshAgent>().velocity;
        m = velocity.magnitude;
        m = Mathf.Clamp(m, 0, 1);
        animator.SetFloat("run", m);

        if (!arrived && GetComponent<NavMeshAgent>().remainingDistance < GetComponent<NavMeshAgent>().stoppingDistance + .2f)
        {
            arrived = true;
            CollidedWithCenter();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
        {
            health -= healthDecreasingAmount;
            if (health <= 0)
            {
                GetComponent<NavMeshAgent>().isStopped = true;
                animator.SetTrigger("die");
                if (attacking)
                {
                    attacking = false;
                    FindObjectOfType<Center>().enemy--;
                }
                StartCoroutine(waitUpKiddo(() => OnDead()));
            }
        }
    }

    IEnumerator waitUpKiddo(System.Action action)
    {
        yield return new WaitForSeconds(2.5f);
        action.Invoke();
    }

    public void MoveToCenter()
    {
        health = 100;
        myTarget = CenterManager.instance.centers[Random.Range(0, 3)];
        GetComponent<NavMeshAgent>().SetDestination(myTarget.transform.position);
    }

    public void OnDead()
    {
        PoolingManager.instance.AddObjectToPool(gameObject);
        GameManager.instance.IncrementKilledEnemyCount();
    }

    private void CollidedWithCenter()
    {
        attacking = true;
        animator.SetTrigger("attack");
        myTarget.enemy++;
    }
}
