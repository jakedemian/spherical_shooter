using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound {
    public AudioClip clip;
    public string name;

    [Range(0f,1f)]
    public float volume;
    
    [Range(0.1f,3f)]
    public float pitch;
    public bool loop;

    [HideInInspector]
    public AudioSource source;

    public bool randomizePitch;
    public float pitchMin;
    public float pitchMax;

    public void Play() {
        if (randomizePitch) {
            source.pitch = Random.Range(pitchMin, pitchMax);
        }

        source.Play();
    }

    public void Stop() {
        source.Stop();
    }
}