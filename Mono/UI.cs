using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI : MonoBehaviour
{    
    public enum Categories { element, layout, window, overlay, uncategorized }
    public enum States { opening, open, closing, closed, hiding, hidden }

    [Header("State Vars")]
    [SerializeField] protected Categories _category;
    public Categories Category { get { return _category; } }
    [SerializeField] protected States _state;
    public States State { get { return _state; } }
    [SerializeField] protected UIData _data;
    public UIData Data { get { return _data;} }

    [Header("Settings")]
    [SerializeField] protected float _tweening;

    public virtual void Setup(UIData data)
    {
        _data = data;
    }
    public virtual void DiscreteUpdate()
    {

    }
    public virtual void DiscreteUpdate(UIData data)
    {
        
    }
    public virtual void Open()
    {
        _state = States.opening;
        LeanTween.scale(gameObject, Vector3.one, _tweening).setOnComplete(Opened);
    }
    public virtual void Opened()
    {
        _state = States.open;
    }
    public virtual void Close()
    {
        _state = States.closing;
        LeanTween.scale(gameObject, Vector3.zero, _tweening).setOnComplete(Closed);
    }
    public virtual void Closed()
    {
        _state = States.closed;
        SelfDestruct();
    }
    public virtual void Hide()
    {
        _state = States.hiding;
        LeanTween.scale(gameObject, Vector3.zero, _tweening).setOnComplete(Hidden);
    }
    public virtual void Hidden()
    {
        _state = States.hidden;
    }

    public virtual void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
[System.Serializable]
public class UIData
{
    public string TextEng;
    public string TextSpa;
    public Sprite Sprite;
    public Player Player;
    public Stuff Stuff;
    public Instruction Instruction;
}
