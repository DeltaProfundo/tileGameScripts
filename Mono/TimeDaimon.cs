using System.Collections.Generic;
using UnityEngine;

public class TimeDaimon : MonoBehaviour
{
    [Header("State Vars")]
    [SerializeField] private float _tickDelay = 2.5f;
    [SerializeField] private float _tickTimer;

    [Header("Settings")]
    [SerializeField, Range(0.1f, 10f)] private float _factor;
    public float Factor 
    { 
        get { return _factor; } 
        set { _factor = value; }
    }

    private Main _main;

    
    void Start()
    {
        _tickTimer = _tickDelay;
        _main = GetComponent<Main>();
    }

    void Update()
    {
        if (Ubik.Instance.GameState == Ubik.GameStates.paused)
        {
            return;
        }
        _tickTimer -= Time.deltaTime * _factor;
        if (_tickTimer < 0)
        {
            _tickTimer = _tickDelay;
            Tick();
        }
    }

    public void Tick()
    {
        _main.Tick();
    }

}
