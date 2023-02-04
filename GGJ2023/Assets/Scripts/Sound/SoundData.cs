using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Database/Engine/SoundData")]
public class SoundData : ScriptableObject
{

    [SerializeField] private string _key = string.Empty;

    [SerializeField] private AudioClip _clip = null;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float _volume = 1;

    [Range(0f, 2f)]
    [SerializeField] private float _pitch = 1;

    [SerializeField] private bool _loop = false;


    [Space]
    [Header("Pitch Variation")]
    [SerializeField] private bool _pitchVariation = false;
    [Range(0f, 2f)]
    [SerializeField] private float _pitchVariationMin = 1;

    [Range(0f, 2f)]
    [SerializeField] private float _pitchVariationMax = 1;


    public string Key => _key;

    public AudioClip Clip => _clip;

    public float Volume  => _volume;

    public float Pitch => _pitch;

    public bool Loop => _loop;

    public bool PitchVariation  => _pitchVariation;
    public float PitchVariationMin  => _pitchVariationMin;
    public float PitchVariationMax => _pitchVariationMax; 
}
