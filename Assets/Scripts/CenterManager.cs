using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterManager : MonoBehaviour
{
    public static CenterManager instance;
    private void Awake()
    {
        instance = this;
    }

    public List<Center> centers = new List<Center>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
