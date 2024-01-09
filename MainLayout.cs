using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainLayout : UI
{
    [SerializeField] private TextMeshProUGUI _textLabel;
    [SerializeField] private Transform _stuffBoxLayout;
    [SerializeField] private ActionBox _actionBox;
    [SerializeField] private Transform _actionBoxContainer;
    [SerializeField] private List<GenericButton> _actionButtons;
    [SerializeField] private List<StuffBox> _stuffBoxes;

    public override void Setup(UIData data)
    {        
        base.Setup(data);
        _stuffBoxes = new List<StuffBox>();
        _actionButtons = new List<GenericButton>();
        DiscreteUpdate();
    }

    public override void DiscreteUpdate()
    {
        base.DiscreteUpdate();
        _textLabel.SetText(Parsing.Text(_data.TextEng, _data.TextSpa));
        UpdateStuffBoxes();
        UpdateActionBox();
    }

    public override void DiscreteUpdate(UIData data)
    {
        base.DiscreteUpdate(data);
        _data = data;
        DiscreteUpdate();
    }

    public void UpdateStuffBoxes()
    {
        Stuff[] inventory = Main.Instance.Player.Inventory.ToArray();
        for (int i = 0; i < inventory.Length; i++)
        {
            Stuff selStuff = inventory[i];
            StuffBox stuffBox = StuffBoxFor(selStuff.Type);
            if (stuffBox == null)
            {
                GameObject newStuffBoxObj = Instantiate(Ubik.Instance.StuffBoxPrefab, _stuffBoxLayout);
                StuffBox newStuffBox = newStuffBoxObj.GetComponent<StuffBox>();
                UIData newStuffBoxData = new UIData();
                newStuffBoxData.Stuff = selStuff;
                newStuffBox.Setup(newStuffBoxData);
                _stuffBoxes.Add(newStuffBox);
            }
            else
            {
                UIData stuffBoxData = new UIData();
                stuffBoxData.Stuff = selStuff;
                stuffBox.DiscreteUpdate(stuffBoxData);
            }
        }
    }
    public void UpdateActionBox()
    {
        ClearActionBox();
        Block selBlock = Main.Instance.SelectedBlock;
        if (selBlock == null) { return; }
        for (int i = 0; i < selBlock.Tile.TileType.AvailableInstructions.Length; i++)
        {
            Instruction selInstruction = selBlock.Tile.TileType.AvailableInstructions[i];
            UIData buttonData = new UIData();
            buttonData.Instruction = selInstruction;

            GameObject buttonObj = Instantiate(Ubik.Instance.GenericButtonPrefab, _actionBoxContainer);
            var button = buttonObj.GetComponent<GenericButton>();            
            button.Setup(buttonData);
            _actionButtons.Add(button);
        }
    }
    public void ClearActionBox()
    {
        foreach(GenericButton gb in _actionButtons) { gb.SelfDestruct(); }
        _actionButtons.Clear(); ;
    }

    public StuffBox StuffBoxFor(StuffType stuffType)
    {
        StuffBox output = null;
        for (int i = 0; i < _stuffBoxes.Count; i++)
        {
            StuffBox selStuffBox = _stuffBoxes[i];
            if (selStuffBox.Data.Stuff.Type == stuffType)
            {
                return selStuffBox;
            }
        }
        return output;
    }
}
