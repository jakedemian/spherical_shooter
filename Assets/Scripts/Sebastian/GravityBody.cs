using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class GravityBody : MonoBehaviour {
     private GravityAttractor planet;

     //private Rigidbody rigidbody;
     
     void Awake() {
         planet = GameObject.FindGameObjectWithTag("planet").GetComponent<GravityAttractor>();
         //rigidbody = GetComponent<Rigidbody>();

         // lock these two fields because we are controlling those from GravityAttractor
         //rigidbody.useGravity = false;
         //rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
     }

     void Update() {
         planet.Attract(transform);
     }
 
 }