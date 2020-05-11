using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float bulletSpeed = 10f;
    public float bulletInaccuracy = 0.5f;
    public float lifeTime = 1f;
    public float collisionRadius = 0.2f;
    public int damage = 1;
    public float knockbackPower = 1f;

    private float timer;

    public void Init(Vector3 dir) {
        transform.forward = dir;
        float inaccuracy = Random.Range(-bulletInaccuracy, bulletInaccuracy);
        transform.Rotate(new Vector3(0, inaccuracy, 0), Space.Self);
    }

    void Update() {
        transform.RotateAround(Vector3.zero, transform.right, bulletSpeed * Time.deltaTime);

        Collider[] colliders = Physics.OverlapSphere(transform.position, collisionRadius);
        if (colliders.Length > 0) {
            foreach (Collider col in colliders) {
                if (col.GetComponent<Enemy>()) {
                    col.GetComponent<Enemy>().Damage(damage);
                    col.GetComponent<Enemy>().Knockback(transform.forward, knockbackPower);
                    DestroySelf();
                    break;
                }
            }
        }

        timer += Time.deltaTime;
        if (timer > lifeTime) {
            DestroySelf();
        }
    }

    private void DestroySelf() {
        Destroy(gameObject);
    }
}