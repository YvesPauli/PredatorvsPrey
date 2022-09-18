using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public float timeScaleFactor = 1;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)){
            Time.timeScale = timeScaleFactor;
        }
        else {
            Time.timeScale = 1;
        }
    }
}
