using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaySound : MonoBehaviour
{

    [SerializeField] private List<DataSound> _dataSounds = new List<DataSound>();
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play(string clipName)
    {
        var clip = _dataSounds.FirstOrDefault(s => s.Name == clipName)?.Clip;
        _audioSource.clip = clip;
        _audioSource.Play();
    }




    [Serializable]
    class DataSound
    {
        public string Name;
        public AudioClip Clip;
    }
}
