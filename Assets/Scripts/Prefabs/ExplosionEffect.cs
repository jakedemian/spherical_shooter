using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour {
    public float expansionSpeed = 1f;
    public float lifeTime = 0.4f;

    private float timer = 0f;
    void Update() {
        float newScale = transform.localScale.x + (expansionSpeed * Time.deltaTime);
        transform.localScale = new Vector3(newScale, newScale, newScale);

        timer += Time.deltaTime;
        if (timer > lifeTime) {
            Destroy(gameObject);
        }
    }
}