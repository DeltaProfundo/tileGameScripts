using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StuffBox : UI
{
    [SerializeField] private Stuff _stuff;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _label;

    public override void Setup(UIData data)
    {
        DiscreteUpdate(data);
        _stuff = data.Stuff;
    }

    public override void DiscreteUpdate(UIData data)
    {
        base.DiscreteUpdate(data);
        _data = data;
        _stuff = data.Stuff;
        _iconImage.sprite = _stuff.Type.Sprite;
        _iconImage.color = _stuff.Type.Color;
        _label.SetText(_stuff.amount.ToString() + " / " + _stuff.Type.DefaultUpperThreshold.ToString());
    }

}
