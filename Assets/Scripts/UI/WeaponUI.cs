using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUI : MonoBehaviour {
    public static WeaponUI instance;

    private void Awake() {
        instance = this;
    }

    private Dictionary<Weapon, string> mapping = new Dictionary<Weapon, string>() {
        {Weapon.Laser, "UI_WeaponLaser"},
        {Weapon.GrenadeLauncher, "UI_WeaponGrenadeLauncher"}
    };

    public void SetWeaponUI(Weapon weapon) {
        for (int i = 0; i < transform.childCount; i++) {
            if (transform.GetChild(i).gameObject.tag.Equals(mapping[weapon])) {
                transform.GetChild(i).gameObject.SetActive(true);
                continue;
            }
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
