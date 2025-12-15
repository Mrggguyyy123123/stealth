using System.Numerics;
using UnityEngine;

public class BulletPhysics : MonoBehaviour
{
    public void Setup(UnityEngine.Vector3 shootDir)
    {
        Rigidbody2D rigidbody2d = GetComponent<Rigidbody2D>();
        float movementSpeed = 100f;
        rigidbody2d.AddForce(shootDir * movementSpeed, ForceMode2D.Impulse);
    }

}
