using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenericButton : UI
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _label;

    public override void Setup(UIData data)
    {
        base.Setup(data);
        DiscreteUpdate(data);
    }

    public override void DiscreteUpdate(UIData data)
    {
        base.DiscreteUpdate(data);
        _data = data;
    }
    public override void DiscreteUpdate()
    {
        base.DiscreteUpdate();
        if (_data.TextEng != null && _data.TextSpa != null && _label != null)
        {
            _label.SetText(Parsing.Text(_data.TextEng, _data.TextSpa));
        }
        if (_data.Sprite != null && _iconImage != null)
        {
            _iconImage.sprite = _data.Sprite;
        }        
    }


    public void Pressed()
    {
        Context context = new Context();
        context.UIs = new UI[] { this };
        if (Main.Instance.SelectedBlock != null)
        {
            context.Tiles = new Tile[] { Main.Instance.SelectedBlock.Tile };
        }
        if (Parsing.ExecuteInstructionsRequirements(_data.Instruction, context))
        {
            Parsing.ExecuteInstruction(_data.Instruction, context);
        }
        DiscreteUpdate();
    }
}
