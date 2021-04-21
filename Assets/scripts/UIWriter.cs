using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWriter : MonoBehaviour
{
    public Text bottles;
    public Text lives;
    public Text boxes;
    public Text health;
    // Update is called once per frame
    void Update()
    {
        bottles.text = Score.bottlesScore.ToString();
        lives.text = Score.lifes.ToString();
        boxes.text = Score.boxScore.ToString();
        health.text = "HEALTH: "+ Score.currentHealth.ToString()+"%";
    }
}
