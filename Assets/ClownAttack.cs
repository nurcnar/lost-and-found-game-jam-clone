using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownAttack : MonoBehaviour
{
    public void Attack()
    {
        transform.parent.GetComponent<Enemy>().myTarget.centerHealth--;
        if (transform.parent.GetComponent<Enemy>().myTarget.centerHealth <= 0)
        {
            transform.parent.GetComponent<Enemy>().myTarget.Death();
        }
    }
}
