using UnityEngine;

[System.Serializable]
public class Stuff
{
    [SerializeField] private StuffType _type;
    public StuffType Type {  get { return _type; } }
    [SerializeField] private int _amount;
    public int amount { get { return _amount; } }

    public Stuff(StuffType stuffType, int amount)
    {
        _type = stuffType;
        _amount = amount;
    }

    public bool DecreaseAmount(int amount)
    {
        bool output = true;
        _amount -= amount;
        if (_amount < 0) 
        {
            _amount = 0;
            output = false; 
        }
        return output;
    }
    public bool IncreaseAmount(int amount)
    {
        bool output = true;
        _amount += amount;
        if (_amount > _type.DefaultUpperThreshold)
        {            
            _amount = _type.DefaultUpperThreshold;
            output = false;
        }
        return output;
    }
}
