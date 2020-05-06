using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform player;
    public Transform sphere;

    public float verticalOffset;
    void Start() {
        SnapCameraAbovePlayer();
    }

    void Update() {
        SnapCameraAbovePlayer();
    }

    private void SnapCameraAbovePlayer() {
        transform.position = player.transform.position + (player.transform.up * verticalOffset);
        //transform.LookAt(new Vector3(0,0,0));
        // if (transform.eulerAngles.y != 0) {
        //     transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 0f, transform.eulerAngles.z);
        // }
        transform.rotation = player.transform.rotation;
        transform.Rotate(new Vector3(90, 0, 0));
    }
}
