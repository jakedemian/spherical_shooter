using System;
using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class GravityBody : MonoBehaviour {
     public LayerMask planetLayer;

     public float adjustmentMin = 0.05f;
     public float adjustmentMax = 1f;

     public bool usePhysicsGravity;
     public float physicsGravity;

     private GravityAttractor planet;
     
     [HideInInspector] public bool adjusted = false;
     [HideInInspector] public float adjustment;
     private Rigidbody rb;
     
     
     void Awake() {
         planet = GameObject.FindGameObjectWithTag("planet").GetComponent<GravityAttractor>();
         rb = GetComponent<Rigidbody>();
     }

     void Update() {
         if (usePhysicsGravity) {
             return;
         };
         
         planet.Attract(transform, adjustment);

         if (!adjusted) {
             float bodyHeight = GetComponent<Collider>().bounds.size.y;
             float halfHeight = bodyHeight / 2;
             RaycastHit hit;
             Vector3 origin = transform.position;
             if (Physics.Raycast(origin, -transform.up.normalized, out hit, Mathf.Infinity, planetLayer)) {
                 float distanceToAdjust = Vector3.Distance(origin - (transform.up.normalized * halfHeight), hit.point);
                 if (distanceToAdjust > adjustmentMin && distanceToAdjust < adjustmentMax) {
                     adjustment = distanceToAdjust;
                     Adjust(adjustment);
                     Debug.Log("adjusting " + transform.name + " by " + adjustment);
                 }
             }
         }
     }

     private void FixedUpdate() {
         if (usePhysicsGravity) {
             rb.AddForce(-transform.position.normalized * physicsGravity, ForceMode.Force);
         }
     }

     public void Adjust(float a) {
         adjusted = true;
         adjustment = a;
     }
 }