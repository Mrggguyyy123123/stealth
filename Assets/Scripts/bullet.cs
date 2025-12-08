using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class bullet : MonoBehaviour
{   
    private UnityEngine.Vector3 shootDir;
    public void Setup(UnityEngine.Vector3 shootDir)
    {
        this.shootDir = shootDir;
       
    }

    private void Update()
    {
        float moveSpeed = 100f;
        transform.position += shootDir * moveSpeed *Time.deltaTime; 
    }
}
