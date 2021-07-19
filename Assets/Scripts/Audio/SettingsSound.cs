using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsSound : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string mixerGroupName;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.minValue = -80f;
        slider.maxValue = 20f;
    }

    private void Start()
    {
        audioMixer.GetFloat(mixerGroupName, out var value);
        slider.value = value;
    }

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(SliderValueChange);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(SliderValueChange);
    }

    void SliderValueChange(float value)
    {
        audioMixer.SetFloat(mixerGroupName, value);
    }
}
