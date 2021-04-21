using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinsController : MonoBehaviour
{
    public GameObject bottlePrefab;
    public GameObject boxPrefab;
    public float boxTimer=10;
    public float bottleTimer = 10;
    private float targetTimeForBox;
    private float targetTimeForbottle;
    public static GameObject currentBottle;
    public static GameObject currentBox;

    void Start()
    {
        targetTimeForBox = boxTimer;
        targetTimeForbottle = bottleTimer;
        currentBottle = Instantiate(bottlePrefab, new Vector3(Random.Range(-16, 16), -0.431f, Random.Range(-16, 16)), Quaternion.identity);
        currentBox = Instantiate(boxPrefab, new Vector3(Random.Range(-16, 16), -4.71248e-18f, Random.Range(-16, 16)), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        targetTimeForBox -= Time.deltaTime;
        targetTimeForbottle -= Time.deltaTime;
        if (targetTimeForbottle<=0)
        {
            timeForBottleEnded();
        }
        if (targetTimeForBox<=0)
        {
            timeForBoxEnded();
        }
        
    }
    public void timeForBottleEnded()
    {
        targetTimeForbottle = bottleTimer;
        if(currentBottle!=null)
            Destroy(currentBottle);
        currentBottle = Instantiate(bottlePrefab, new Vector3(Random.Range(-16, 16), -0.431f, Random.Range(-16, 16)), Quaternion.identity);

    }

    public void timeForBoxEnded()
    {
        targetTimeForBox = boxTimer;
        if (currentBox!=null)
            Destroy(currentBox);
        currentBox = Instantiate(boxPrefab, new Vector3(Random.Range(-16, 16), -4.71248e-18f, Random.Range(-16, 16)), Quaternion.identity);

    }

}
