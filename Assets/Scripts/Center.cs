using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Center : MonoBehaviour
{
    public int enemy;
    public int centerHealth = 10, maxHealth = 10;

    public Image healthBar;

    private void Update()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (float)centerHealth / (float)maxHealth, Time.deltaTime * 20);

    }

    public void Death()
    {

    }
}
