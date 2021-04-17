using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONDemo 
{
    static string jsonString;
    public JSONDemo(string fileName)
    {
        string path = Application.dataPath + "/data/" + "demo.json";
        jsonString = File.ReadAllText(path);
    }

    public string  getJson()
    {
                  return jsonString;
        
    }

}
/*
[System.Serializable]
class testClass
{
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;

}

// in the start method
//testClass t = JsonUtility.FromJson<testClass>(JSONDemo.getInstance());
*/