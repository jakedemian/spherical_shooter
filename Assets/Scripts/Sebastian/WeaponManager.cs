using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    
    private Weapon weapon;

    public static WeaponManager instance;
    private void Awake() {
        instance = this;
    }

    void Start() {
        weapon = Weapon.Laser;
        SetWeaponUI();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            weapon = Weapon.Laser;
            SetWeaponUI();
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            weapon = Weapon.GrenadeLauncher;
            SetWeaponUI();
        }
    }

    private void SetWeaponUI() {
        WeaponUI.instance.SetWeaponUI(weapon);
    }

    public Weapon GetWeapon() {
        return weapon;
    }
}

public enum Weapon {
    Laser,
    GrenadeLauncher
};