using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public int maxHealth = 10;
    public float damageFlashTime = 0.1f;
    public Material damageFlashMaterial;
    public MeshRenderer meshRenderer;
    public float knockbackTime = 0.5f;
    public AnimationCurve knockbackCurve;
    public int minDeathParticles = 5;
    public int maxDeathParticles = 12;
    public float deathParticleSpawnVariation = 0.3f;
    public GameObject deathParticlePrefab;

    public Material startingMaterial;
    private int health;

    private struct knockback_data {
        public Vector3 dir;
        public float power;

        public knockback_data(Vector3 d, float p) {
            dir = d;
            power = p;
        }
    }
    
    void Start() {
        startingMaterial = meshRenderer.sharedMaterial;
        health = maxHealth;
    }

    void Update() {
    }

    public void Damage(int dmg) {
        health -= dmg;
        StartCoroutine(nameof(DamageFlash));
        AudioManager.Instance.Play("enemyHit");
        if (health < 0) {
            Kill();
        }
    }

    public void Knockback(Vector3 hitDir, float power) {
        StartCoroutine(nameof(DoKnockback), new knockback_data(hitDir, power));
    }

    private IEnumerator DamageFlash() {
        float timer = 0f;
        Material dmgFlash = new Material(damageFlashMaterial);
        meshRenderer.sharedMaterial = dmgFlash;
        float percentComplete = 0f;
        while (timer < damageFlashTime) {
            timer += Time.deltaTime;
            yield return null;
        }

        meshRenderer.sharedMaterial = startingMaterial;
    }

    private IEnumerator DoKnockback(knockback_data data) {
        float timer = 0f;
        while (timer < knockbackTime) {
            timer += Time.deltaTime;
            float percentLeft = 1 - (timer / knockbackTime);
            float knockbackAmount = knockbackCurve.Evaluate(percentLeft) * data.power;
            transform.RotateAround(Vector3.zero, Vector3.Cross(data.dir, -transform.up), knockbackAmount * Time.deltaTime);
            yield return null;
        }
    }

    private void Kill() {
        int deathParticles = Random.Range(minDeathParticles, maxDeathParticles + 1);
        for (int i = 0; i < deathParticles; i++) {
            var x = Random.Range(-deathParticleSpawnVariation, deathParticleSpawnVariation);
            var y = Random.Range(-deathParticleSpawnVariation, deathParticleSpawnVariation);
            var z = Random.Range(-deathParticleSpawnVariation, deathParticleSpawnVariation);
            Vector3 spawnPos = transform.position + new Vector3(x, y, z);
            Instantiate(deathParticlePrefab, spawnPos, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}