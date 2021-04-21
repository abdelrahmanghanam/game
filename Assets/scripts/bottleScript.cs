using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottleScript : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Score.bottlesScore++;
            Destroy(gameObject);
        }
    }
}
