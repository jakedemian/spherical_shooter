﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour {
    public float gravity = -10f;
    public float radius = 50f;

    public void Attract(Transform body) {
        Vector3 targetDir = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;
        
        body.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation;
        body.transform.position = body.transform.position.normalized * radius;
        //body.GetComponent<Rigidbody>().AddForce(targetDir * gravity);
    }
    void Start() {
    }

    void Update() {
    }
}