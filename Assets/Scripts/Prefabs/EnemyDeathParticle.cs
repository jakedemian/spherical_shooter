using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyDeathParticle : MonoBehaviour {
    public float minScale;
    public float maxScale;
    public float minUpSpeed;
    public float maxUpSpeed;
    public float maxLateralSpeed;
    public float maxTorque;

    public float lifeTime;
    public float shrinkTime;
    
    public Rigidbody rb;
    void Start() {
        float scale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale, scale, scale);
        
        // velocity
        float x = Random.Range(-maxLateralSpeed, maxLateralSpeed);
        float y= Random.Range(minUpSpeed, maxUpSpeed);
        float z = Random.Range(-maxLateralSpeed, maxLateralSpeed);
        rb.AddForce(new Vector3(x,y,z));

        // torque
        float torque = Random.Range(-maxTorque, maxTorque);
        rb.AddTorque(new Vector3(torque, torque, torque));
    }

    void Update() {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0f) {
            StartCoroutine(nameof(StartDestroyAnimation));
        }
    }

    IEnumerator StartDestroyAnimation() {
        float startScale = transform.localScale.x;
        float timer = 0f;
        while (timer < shrinkTime) {
            timer += Time.deltaTime;
            float percentSize = 1 - (timer / shrinkTime);
            transform.localScale = new Vector3(startScale*percentSize, startScale*percentSize, startScale*percentSize);
            yield return null;
        }
        
        Destroy(gameObject);
    }

}