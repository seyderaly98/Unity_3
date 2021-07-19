using System;
using UnityEngine;

internal sealed class Health : MonoBehaviour
{
    public float current;
    private AudioSource audioSource;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ApplyDamage(float damage )
    {
        audioSource.Play();
        current -= damage;

        if (current < 0.0f)
            current = 0.0f;
    }
}