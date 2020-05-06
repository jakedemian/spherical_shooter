using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    public Transform camLock;
    public GameObject bulletPrefab;
    public float bulletFireSpacingTime;


    private float bulletTimer = 0f;
    private float depth;
    
    #region GunMode
    enum GunMode {
        BULLET
    };
    private GunMode gunMode = GunMode.BULLET;
    #endregion
    
    void Start() {
        depth = Vector3.Distance(camLock.position, transform.position);
    }

    void Update() {
        if (gunMode == GunMode.BULLET && Input.GetMouseButton(0)) {
            bulletTimer += Time.deltaTime;
            if (bulletTimer > bulletFireSpacingTime) {
                bulletTimer = 0f;
                FireBullet();
            }
        }
    }

    private void FireBullet() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = depth;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 initBulletDirection = mouseWorldPos - transform.position;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Init(initBulletDirection);
        
        AudioManager.Instance.Play("laserShoot");
    }
}