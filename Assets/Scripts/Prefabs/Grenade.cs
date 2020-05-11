using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {
    public LayerMask planetLayer;
    public float initialSpeed = 10f;
    public float fuseTime = 2.5f;
    public float grenadeInaccuracy = 2f;
    public float groundMinDistance = 0.1f;
    public GameObject explosionPrefab;
    //public float gravity;
    public float explosionRadius = 3f;
    public int explosionDamage = 15;
    public float knockbackPower = 20f;
    
    private float fuseTimer = 0f;
    private float speed;
    private bool grounded = false;

    private Rigidbody rb;
    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * initialSpeed;
    }
    void Update() {
        //rb.AddForce(-transform.position.normalized * gravity, ForceMode.Acceleration);

        fuseTimer += Time.deltaTime;
        if (fuseTimer > fuseTime) {
            Explode();
            Destroy(gameObject);
        }
    }

    private void Explode() {
        AudioManager.Instance.Play("grenadeExplosion");
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        if (colliders.Length > 0) {
            foreach (Collider col in colliders) {
                if (col.GetComponent<Enemy>()) {
                    col.GetComponent<Enemy>().Damage(explosionDamage);
                    col.GetComponent<Enemy>().Knockback(col.transform.position - transform.position, knockbackPower);
                }
            }
        }
    }
    
    public void Init(Vector3 dir) {
        transform.forward = dir;
        float inaccuracy = Random.Range(-grenadeInaccuracy, grenadeInaccuracy);
        transform.Rotate(new Vector3(0, inaccuracy, 0), Space.Self);
        speed = initialSpeed;
    }
}