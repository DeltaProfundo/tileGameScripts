using UnityEngine;

[CreateAssetMenu]
public class TileType : Invokable
{
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite { get { return _sprite; } }
    [SerializeField] private Color _color;
    public Color Color { get { return _color; } }

    [SerializeField] private Instruction[] _availableInstructions;
    public Instruction[] AvailableInstructions { get { return _availableInstructions; } }
    [SerializeField] private Instruction _tickInstruction;
    public Instruction TickInstruction { get { return _tickInstruction; } }
    [SerializeField] private GameObject _maquettePrefab;
    public GameObject MaquettePrefab { get { return _maquettePrefab; } }
}
