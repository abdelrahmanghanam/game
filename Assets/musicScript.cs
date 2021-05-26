using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicScript : MonoBehaviour
{
    private AudioManager am;

    private void Start()
    {
        am = FindObjectOfType<AudioManager>();
        am.Play("base", true);
    }
}
