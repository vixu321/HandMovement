using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GM : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 playerPosition;
    int x;
    int y;
    int z;



    void Start()
    {
        playerPosition = GameObject.Find("Cube").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        int breakPoint;
        string text = System.IO.File.ReadAllText(@"c:\temp\handMovementData.txt");
        //print(playerPosition);
        int length =System.IO.File.ReadAllText(@"c:\temp\handMovementData.txt").Length-2;
        for(int i =0; i<length; i++){
            if(text[i]=';'){
                breakPoint=i;
                for(int j=breakPoint; j<length;j++){
            for(int i =0; i<length-breakPoint;i++){
            y[i]=text[j];
        }
        }
            }
            x[i]=text[i];
        }
        
        print(breakPoint);

    }
}
