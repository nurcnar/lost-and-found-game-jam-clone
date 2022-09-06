using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Disables { WASD, stun, quickness }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    public int killedEnemyCount;
    public void IncrementKilledEnemyCount()
    {
        killedEnemyCount++;
        if (killedEnemyCount % 1 == 0)
        {
            ApplyDisable();
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    private void ApplyDisable()
    {
        print("asd");
        StartCoroutine(ApplyKiiledEnemy());
        //Burada random disable seçtirt, gelen disable'a göre, aşağıdaki metotlardan birini çağır

        var selectedDisable = (Disables)Random.Range(0,2);
        /*switch (selectedDisable)
        {
            case Disables.WASD:
                StartCoroutine(ApplyWASD());
                break;
            case Disables.stun:
                StartCoroutine(ApplyStun());
                break;
            case Disables.quickness:
                StartCoroutine(ApplyQuickness());
                break;
            default:
                break;
        }*/
    }

    public IEnumerator ApplyWASD()
    {
        PlayerMovement.instance.isWASDDisableActivatet = false;
        yield return new WaitForSeconds(5);
        PlayerMovement.instance.isWASDDisableActivatet = true;
    }

    public IEnumerator ApplyStun()
    {
        float oldSpeed = PlayerMovement.instance.currentMovementSpeed;
        PlayerMovement.instance.currentMovementSpeed = 0;

        yield return new WaitForSeconds(5);

        PlayerMovement.instance.currentMovementSpeed = oldSpeed;
    }

    public IEnumerator ApplyQuickness()
    {
        float oldSpeed = PlayerMovement.instance.currentMovementSpeed;
        Firing.instance.cooldownTime = 1.5f;
        PlayerMovement.instance.currentMovementSpeed /= 3;
        yield return new WaitForSeconds(5);
        Firing.instance.cooldown = 0.75f;
        PlayerMovement.instance.currentMovementSpeed = oldSpeed;
    }

    public IEnumerator ApplyBulletDisable()
    {
        float oldSpeed = Firing.instance.cooldown;
        Firing.instance.cooldown = 1000f;
        yield return new WaitForSeconds(5);
        Firing.instance.cooldown = oldSpeed;
    }

    public IEnumerator ApplyKiiledEnemy()
    {
        for (int i = 0; i < EnemySpawner.instance.notPooledObjectsEnemy.Count; i++)
        {
            PoolingManager.instance.AddObjectToPool(EnemySpawner.instance.notPooledObjectsEnemy[i]);
        }
        yield return new WaitForSeconds(5);       
    }
}