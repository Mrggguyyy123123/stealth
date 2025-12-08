using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

using System.Numerics;public class shoot : MonoBehaviour
{
    [SerializeField] private Transform pfBullet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        GetComponent<Gun_aim>().OnShoot += PlayerShootProjectiles_OnShoot;
    }

    // Update is called once per frame
    private void PlayerShootProjectiles_OnShoot(object sender, Gun_aim.OnShootEventArgs e)
    {
        Transform bulletTransform = Instantiate(pfBullet, e.gunEndPointPosition, UnityEngine.Quaternion.identity);
        UnityEngine.Vector3 shootDir = (e.shootPosition - e.gunEndPointPosition).normalized;
        bulletTransform.GetComponent<bullet>().Setup(shootDir);
    }
}
