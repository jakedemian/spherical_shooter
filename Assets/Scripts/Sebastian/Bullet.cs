using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float bulletSpeed = 10f;
    public float bulletInaccuracy = 0.5f;
    public float lifeTime = 1f;

    private float timer;

    public void Init(Vector3 dir) {
        transform.forward = dir;
        float inaccuracy = Random.Range(-bulletInaccuracy, bulletInaccuracy);
        transform.Rotate(new Vector3(0, inaccuracy, 0), Space.Self);
    }

    void Update() {
        transform.RotateAround(Vector3.zero, transform.right, bulletSpeed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer > lifeTime) {
            Destroy(gameObject);
        }
    }
}