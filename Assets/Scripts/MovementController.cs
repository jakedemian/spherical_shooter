using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
    public Transform sphere;
    public float moveSpeed;
    public float sphereSurfaceOffset = 0.5f;

    private Vector2 moveInput = Vector2.zero;
    private float sphereRadius;

    private void ReadInput() {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void SnapToSphere() {
        transform.position = transform.position.normalized * (sphereRadius + sphereSurfaceOffset);
    }

    void Start() {
        sphereRadius = sphere.GetComponent<SphereCollider>().radius * sphere.transform.localScale.x;
        Debug.Log(sphereRadius);
    }

    void Update() {
        ReadInput();
        
        if (Util.Vector2IsFull(moveInput)) {
            moveInput.x /= Mathf.Sqrt(2);
            moveInput.y /= Mathf.Sqrt(2);
        }
        //transform.RotateAround(sphere.position, Vector3.back, moveInput.x * moveSpeed * Time.deltaTime);
        //transform.RotateAround(sphere.position, Vector3.right, moveInput.y * moveSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * moveInput.x * moveSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * moveInput.y * moveSpeed * Time.deltaTime);
        
        SnapToSphere();
        // transform.rotation = Quaternion.LookRotation (sphere.transform.position - transform.position, transform.up);
        // Transform.rotation = Quat
        Vector3 dir = transform.position - sphere.transform.position;
        transform.up = dir;
    }
}
