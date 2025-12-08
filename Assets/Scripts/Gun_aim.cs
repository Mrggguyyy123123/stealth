using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System.Numerics;
using System;


public class Gun_aim : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public UnityEngine.Vector3 gunEndPointPosition;
        public UnityEngine.Vector3 shootPosition;
    }
    private Transform aimTransform;
    private Transform aimGunEndPointTransform;
    

    // Start is called onc e before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
         aimTransform= transform.Find("Pivot");
         aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition");
    }

    // Update is called once per frame
    void Update()
    {
        HandleAiming();
        HandleShooting();

    }
    private void HandleAiming()
    {
        UnityEngine.Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

        UnityEngine.Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new UnityEngine.Vector3(0,0,angle);
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UnityEngine.Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            OnShoot? .Invoke(this, new OnShootEventArgs {
                gunEndPointPosition = aimGunEndPointTransform.position,
                shootPosition = mousePosition,
            });
        }
    }
}
