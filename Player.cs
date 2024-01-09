using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    [SerializeField] private List<Stuff> _inventory;
    public List<Stuff> Inventory {  get { return _inventory; } }

    public Player()
    {
        _inventory = new List<Stuff>();
    }
    public Player(Stuff[] stuff)
    {
        _inventory = new List<Stuff>();
        if (stuff != null && stuff.Length > 0)
        {
            foreach (Stuff s in stuff) { _inventory.Add(s); }
        }        
    }

    public void GainStuff(Stuff stuff)
    {
        Stuff onInventory = GetStuff(stuff.Type);
        if (onInventory == null)
        {
            _inventory.Add(stuff);
            return;
        }
        Debug.Log("Increasing amount");
        if (!onInventory.IncreaseAmount(stuff.amount))
        {
            Debug.Log("Threshold breached");
            Context context = new Context();
            context.Stuff = stuff;
            Parsing.ExecuteInstruction(stuff.Type.UpperThresholdBreachInstruction, context);
        }
        
    }
    public void LoseStuff(Stuff stuff)
    {
        Stuff onInventory = GetStuff(stuff.Type);
        if (onInventory == null)
        {
            return;
        }
        if (!onInventory.DecreaseAmount(stuff.amount))
        {
            Context context = new Context();
            context.Stuff = stuff;
            Parsing.ExecuteInstruction(stuff.Type.LowerThresholdBreachInstruction, context);
        }
    }
    public Stuff GetStuff(StuffType stuffType)
    {
        Stuff output = null;
        for (int i = 0; i < _inventory.Count; i++)
        {
            Stuff selStuff = _inventory[i];
            if (selStuff.Type == stuffType) { return selStuff; }
        }
        return output;
    }

}
