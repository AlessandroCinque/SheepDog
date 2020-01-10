using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    public GameObject sheep;
    static int numSheep = 5;
    public static int praireSize = 15; 
    public static GameObject[] allSheep = new GameObject[numSheep];

    public static Vector3 goalPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numSheep; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-praireSize, praireSize), 0.0f, Random.Range(-praireSize, praireSize));
            allSheep[i] = (GameObject)Instantiate(sheep, pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 10000) < 50)
        {
            goalPos= new Vector3(Random.Range(-praireSize, praireSize), 0.0f, Random.Range(-praireSize, praireSize));
        }
    }
}
