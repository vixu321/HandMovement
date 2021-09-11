using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class GM : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 playerPosition;
    float x;
    float y;
    int z;
    string line;

    System.IO.StreamReader file;

    void Start()
    {
        playerPosition = GameObject.Find("Cube").transform.position;
            
    }

    // Update is called once per frame
    void Update()
    {
        file = new System.IO.StreamReader(@"c:\temp\handMovementData.txt");
        x = float.Parse(file.ReadLine());
        print("X: "+x);
        y = -float.Parse(file.ReadLine());
        print("Y: "+y);
        GameObject.Find("Cube").transform.position = new Vector3(x, 1, y);
        file.Close();
    }
}
