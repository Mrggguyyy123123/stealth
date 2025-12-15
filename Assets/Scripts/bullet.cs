using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class bullet : MonoBehaviour
{   
   
    private UnityEngine.Vector3 shootDir;
    private void Start()
    {
        Destroy(gameObject, 5f);
    }
    public void Setup(UnityEngine.Vector3 shootDir)
    {
        this.shootDir = shootDir;
       
    }

    private void Update()
    {
        float moveSpeed = 20f;
        transform.position += shootDir * moveSpeed *Time.deltaTime; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     GameHandle target = collision.GetComponent<GameHandle>();
     if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
            return;
        }
     if (target != null)
        {
            target.Damage(35);
            Destroy(gameObject);
        }   
    }
}
