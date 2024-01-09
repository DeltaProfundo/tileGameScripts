using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Instance;

    private AudioSource _traxSource;
    private AudioSource[] _fxSources;

    [SerializeField] private float _traxVolume;
    public float TraxVolume { get { return _traxVolume; } }
    [SerializeField] private float _fxVolume;
    public float FXVolume { get { return _fxVolume; } }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        _traxSource = GetComponent<AudioSource>();
        List<AudioSource> audioList = new List<AudioSource>();
        foreach(Transform child in transform)
        {
            AudioSource selSource = child.gameObject.GetComponent<AudioSource>();
            if (selSource != null) { audioList.Add(selSource); }
        }
        _fxSources = audioList.ToArray();
    }

    public void DiscreteUpdate(float traxVolume, float fxVolume)
    {
        _traxVolume = traxVolume;
        _fxVolume = fxVolume;
        _traxSource.volume = _traxVolume;
        foreach(AudioSource fxSource in _fxSources)
        {
            fxSource.volume = _fxVolume;
        }
    }

    public void PlayTrack(AudioClip track)
    {
        _traxSource.clip = track;
        _traxSource.Play();
    }
    public void PlayFX(AudioClip fx)
    {
        for (int i = 0; i < _fxSources.Length; i++)
        {
            AudioSource fxSource = _fxSources[i];
            if (fxSource.isPlaying) 
            {
                continue;
            }
            fxSource.clip = fx;
            fxSource.Play();
        }
    }
}
