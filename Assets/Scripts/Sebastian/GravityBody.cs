using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class GravityBody : MonoBehaviour {
     public LayerMask planetLayer;

     public float adjustmentMin = 0.05f;
     public float adjustmentMax = 1f;
     
     private GravityAttractor planet;
     
     [HideInInspector] public bool adjusted = false;
     [HideInInspector] public float adjustment;
     
     void Awake() {
         planet = GameObject.FindGameObjectWithTag("planet").GetComponent<GravityAttractor>();
     }

     void Update() {
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

     public void Adjust(float a) {
         adjusted = true;
         adjustment = a;
     }
 }