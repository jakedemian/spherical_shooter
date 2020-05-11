using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraController : MonoBehaviour {
    public Transform camLock;
    public float followSpeed;
    public float distanceFactor;
    public bool useDistanceFactor;

    private Vector3 targetPos;

    void Update() {
        targetPos = camLock.position;
        float distanceFromTarget = Vector3.Distance(targetPos, transform.position);
        float appliedDistanceFactor = distanceFromTarget > 1f ? distanceFromTarget * distanceFactor : 0f;

        float moveSpeed = useDistanceFactor ? followSpeed * appliedDistanceFactor : followSpeed;
        
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        transform.rotation = camLock.rotation;
    }
}
