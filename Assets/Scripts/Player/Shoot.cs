using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    public Transform camLock;
    private NewPlayerMovement movement;
    private WeaponManager weaponManager;

    public GameObject bulletPrefab;
    public float bulletFireSpacingTime;
    public float laserMoveModifier;

    public GameObject grenadePrefab;
    public float grenadeFireSpacingTime;
    public float grenadeChargeCooldownTime;
    public int maxGrenades;
    public float grenadeLauncherMoveModifier;

    // timers
    private float bulletTimer = 0f;
    private float grenadeTimer = 0f;
    private float grenadeChargeCooldownTimer = 0f;

    private float depth;

    private int grenadeCount;

    #region GunMode

    enum GunMode {
        BULLET,
        GRENADE
    };

    private GunMode gunMode = GunMode.GRENADE;

    #endregion

    void Start() {
        movement = GetComponent<NewPlayerMovement>();
        depth = Vector3.Distance(camLock.position, transform.position);
        grenadeCount = maxGrenades;

        bulletTimer = bulletFireSpacingTime;
        grenadeTimer = grenadeFireSpacingTime;
        weaponManager = WeaponManager.instance;
    }

    void Update() {
        UpdateCooldowns();
        bulletTimer = bulletTimer > bulletFireSpacingTime ? bulletTimer : bulletTimer + Time.deltaTime;
        grenadeTimer = grenadeTimer > grenadeFireSpacingTime ? grenadeTimer : grenadeTimer + Time.deltaTime;
        //Debug.Log(grenadeTimer);

        if (weaponManager.GetWeapon() == Weapon.Laser && Input.GetMouseButton(0)) {
            movement.SetMoveModifier(laserMoveModifier); // TODO make this better than a hardcoded value
            if (bulletTimer > bulletFireSpacingTime) {
                bulletTimer = 0f;
                FireBullet();
            }
        }
        else if (weaponManager.GetWeapon() == Weapon.GrenadeLauncher && Input.GetMouseButton(0)) {
            movement.SetMoveModifier(grenadeLauncherMoveModifier); // TODO make this better than a hardcoded value
            if (grenadeCount == maxGrenades) {
                FireGrenade();
                grenadeCount--;
                GrenadeUI.instance.SetGrenadeCount(grenadeCount);
                grenadeTimer = 0f;
            }
            else if (grenadeCount > 0 && grenadeTimer > grenadeFireSpacingTime) {
                grenadeTimer = 0f;
                FireGrenade();
                grenadeCount--;
                GrenadeUI.instance.SetGrenadeCount(grenadeCount);
            }
        }
        else {
            movement.SetMoveModifier(1f);
        }
    }

    private void FireBullet() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = depth;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 fireDirection = (mouseWorldPos - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Init(fireDirection);

        AudioManager.Instance.Play("laserShoot");
    }

    private void FireGrenade() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = depth;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        // FIXME pukers
        Vector3 fireDirection = (mouseWorldPos - transform.position).normalized;
        GameObject grenade = Instantiate(grenadePrefab, transform.position, Quaternion.identity);
        grenade.GetComponent<Grenade>().Init(fireDirection);
        
        AudioManager.Instance.Play("grenadeShoot");
    }

    private void UpdateCooldowns() {
        if (grenadeCount < maxGrenades) {
            grenadeChargeCooldownTimer += Time.deltaTime;
            if (grenadeChargeCooldownTimer > grenadeChargeCooldownTime) {
                grenadeChargeCooldownTimer = 0f;
                grenadeCount++;
                GrenadeUI.instance.SetGrenadeCount(grenadeCount);
            }
        }
    }
}