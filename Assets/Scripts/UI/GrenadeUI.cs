using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeUI : MonoBehaviour {
    #region Instance
    public static GrenadeUI instance;
    private void Awake() {
        instance = this;
    }
    #endregion

    private int grenadeCount;

    public void SetGrenadeCount(int gc) {
        grenadeCount = gc;
        
        for (int i = 0; i < transform.childCount; i++) {
            if (i < gc) {
                transform.GetChild(i).gameObject.SetActive(true);
                continue;
            }
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

}