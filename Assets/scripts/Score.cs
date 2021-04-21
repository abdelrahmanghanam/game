using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int bottlesScore;
    public static int boxScore;
    public static int lifes;
    public static int currentHealth;
    void Start()
    {
        bottlesScore = 0;
        boxScore = 0;
        lifes = 4;
        currentHealth = 100;
    }


}
