using UnityEngine;

public class Invokable : ScriptableObject
{
    [SerializeField] private string _invoke;
    public string Invoke { get { return _invoke; } }
}
