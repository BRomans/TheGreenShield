using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateActivate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DelayedActivate(float delay)
    {
        Invoke("Activate", delay);
    }

    private void Activate()
    {
        gameObject.SetActive(true);
    }   
}
