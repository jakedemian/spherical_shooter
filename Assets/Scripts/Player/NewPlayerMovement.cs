using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f;

    private float hInput;
    private float vInput;
    private float moveModifier = 1f;

    void Update() {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        if (Util.IsZero(hInput) && Util.IsZero(vInput)) return;

        var frameMoveSpeed = Util.NotZero(hInput) && Util.NotZero(vInput) ? 
            moveSpeed / Mathf.Sqrt(2) : 
            moveSpeed;
        frameMoveSpeed *= moveModifier;

        if (Util.NotZero(vInput)) {
            //transform.Translate(transform.forward * vInput * moveSpeed * Time.deltaTime);
            transform.RotateAround(Vector3.zero, transform.right * vInput, frameMoveSpeed * Time.deltaTime);
        }

        if (Util.NotZero(hInput)) {
            //transform.Translate(transform.right * hInput * moveSpeed * Time.deltaTime);
            transform.RotateAround(Vector3.zero, -transform.forward * hInput, frameMoveSpeed * Time.deltaTime);
        }


        Debug.DrawLine(transform.position, transform.position + (transform.forward * vInput), Color.blue);
        Debug.DrawLine(transform.position, transform.position + (transform.right * hInput), Color.red);
    }

    public void SetMoveModifier(float m) {
        moveModifier = m;
    }
}