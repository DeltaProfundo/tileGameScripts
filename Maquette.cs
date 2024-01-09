using UnityEngine;

public class Maquette : MonoBehaviour
{
    public enum States { opening, open, closing, closed }

    [SerializeField] private string _invoke;
    public string Invoke { get { return _invoke; } }
    [SerializeField] private States _state;
    public States State { get { return _state; } }
    [SerializeField] private float _tweeningTime;

    public void Open()
    {
        _state = States.opening;
        LeanTween.scale(gameObject, Vector3.one, _tweeningTime).setOnComplete(Opened);
    }
    public void Opened()
    {
        _state = States.open;
    }
    public void Close()
    {
        _state = States.closing;
        LeanTween.scale(gameObject, Vector2.zero, _tweeningTime).setOnComplete(Closed);
    }
    public void Closed()
    {
        _state = States.closed;
        SelfDestruct();
    }
    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
