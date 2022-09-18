using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject PredatorPrefab;
    public GameObject PreyPrefab;

    public Transform borderTop;
    public Transform borderBot;
    public Transform borderRight;
    public Transform borderLeft;

    private int border = 2;


    void SpawnPred() {
        int x = (int)Random.Range(borderLeft.transform.position.x + border, borderRight.transform.position.x - border);
        int y = (int)Random.Range(borderBot.transform.position.y + border, borderTop.transform.position.y - border);
        Instantiate(PredatorPrefab,
            new Vector3(x, y, 1),
            Quaternion.identity);
    }

    void SpawnPrey()
    {
        int x = (int)Random.Range(borderLeft.transform.position.x + border, borderRight.transform.position.x - border);
        int y = (int)Random.Range(borderBot.transform.position.y + border, borderTop.transform.position.y - border);
        Instantiate(PreyPrefab,
            new Vector3(x, y, 1),
            Quaternion.identity);
    }



    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 20; i++)
        {
            SpawnPred();
            SpawnPrey();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
