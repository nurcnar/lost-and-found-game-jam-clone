using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image cooldownImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       /* if (Firing.instance.cooldown != 0)
        {
            cooldownImage.fillAmount = 1 - Firing.instance.cooldown / Firing.instance.cooldownTime;
        }
        else
        {
            cooldownImage.fillAmount = 0;
        }*/
    }
}
