using UnityEngine;

[CreateAssetMenu]
public class ActionType : Invokable
{
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite { get { return _sprite; } }
    [SerializeField] private Instruction _instruction;
    public Instruction Instruction { get { return _instruction; } }
}
