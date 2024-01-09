using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StuffType : Invokable
{
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite { get { return _sprite; } }
    [SerializeField] private Color _color;
    public Color Color { get { return _color; } }

    [SerializeField] private int _defaultUpperThreshold;
    public int DefaultUpperThreshold { get { return _defaultUpperThreshold; } }

    [SerializeField] private Instruction _lowerThresholdBreachInstruction;
    public Instruction LowerThresholdBreachInstruction { get {  return _lowerThresholdBreachInstruction; } }
    [SerializeField] private Instruction _upperThresholdBreachInstruction;
    public Instruction UpperThresholdBreachInstruction { get { return _upperThresholdBreachInstruction; } }
}
