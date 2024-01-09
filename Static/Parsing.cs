using System;
using UnityEngine;

public static class Parsing
{
    public static void ExecuteInstruction(Instruction instruction, Context context)
    {
        Debug.Log("ExecuteInstruction");
        if ( instruction.SuccessRequirements == null || instruction.SuccessRequirements.Length == 0)
        {
            Debug.Log("Requirements not found");
            Arguments(instruction.SuccessArguments, context);
            return;
        }
        switch (Requirements(instruction.SuccessRequirements, context))
        {
            case true:
                Arguments(instruction.SuccessArguments, context);
                return;
            case false:
                Arguments(instruction.FailureArguments, context);
                return;            
        }
    }
    public static bool DisplayInstructionRequirements(Instruction instruction, Context context)
    {
        return Requirements(instruction.DisplayRequirements, context);
    }
    public static bool ExecuteInstructionsRequirements(Instruction instruction, Context context)
    {
        Debug.Log("Parsing : Execute Instructions requirements");
        return Requirements(instruction.ExecutionRequirements, context);
    }

    public static string Text(string textEng, string textSpa)
    {
        string output = "";
        switch (Ubik.Instance.GameLanguage)
        {
            case Ubik.GameLanguages.english:
                output = textEng;
                break;
            case Ubik.GameLanguages.spanish:
                output = textSpa;
                break;            
        }
        return output;
    }

    public static void Arguments(string[] arguments)
    {
        Arguments(arguments, new Context());
    }        
    public static void Arguments(string[] arguments, Context context)
    {
        for (int i = 0; i < arguments.Length; i++)
        {
            
            string arg = arguments[i];
            if (arg.Contains("[debug]"))
            {
                Debug.Log(arg);
            }
            if (arg.Contains("[discreteUpdate]"))
            {
                if (Main.Instance != null) { Main.Instance.DiscreteUpdate(); }
                continue;
            }
            if (arg.Contains("[switchLanguage]"))
            {
                Ubik.Instance.SwitchLanguage();
                continue;
            }
            if (arg.Contains("[closeWindow]"))
            {
                Stage.Instance.CloseUI(UI.Categories.window);
                continue;
            }
            if (arg.Contains("[openSettingsWindow]"))
            {
                if (Main.Instance != null) { Main.Instance.OpenSettingsWindow(); }
                continue;
            }
            if (arg.Contains("[setTimeFactor]"))
            {
                arg = arg.Replace("[setTimeFactor]", "");
                Main main = Main.Instance;
                if (main == null) { continue; }
                if (arg.Contains("[zero]"))
                {
                    main.SetTimeFactor(Ubik.Instance.ZeroTimeFactor);
                }
                else if (arg.Contains("[slow]"))
                {
                    main.SetTimeFactor(Ubik.Instance.SlowTimeFactor);
                }
                else if (arg.Contains("[fast]"))
                {
                    main.SetTimeFactor(Ubik.Instance.FastTimeFactor);
                }
                else if (arg.Contains("[veryFast]"))
                {
                    main.SetTimeFactor(Ubik.Instance.VeryFastTimeFactor);
                }
                continue;
            }
            if (arg.Contains("[changeTileType]"))
            {
                if (context.Tiles == null || context.Tiles.Length == 0) { continue; }
                arg = arg.Replace("[changeTileType]", "");
                TileType selTileType = Auxiliary.GetInvokable<TileType>(arg, Ubik.Instance.TileTypes);
                Tile selTile = context.Tiles[0];
                if (selTileType == null) 
                {
                    Debug.LogWarning("Parsing : [changeTileType] : TileType is null");
                    continue; 
                }
                selTile.DiscreteUpdate(selTileType);
                Block selBlock = Main.Instance.BlockDaimon.Block(selTile.GridPos);
                if (selBlock == null)
                {
                    Debug.LogWarning("Parsing : [changeTileType] : Block not found");
                    continue;
                }
                selBlock.Setup(selTile);
            }
            if (arg.Contains("[decreasePlayerStuff]"))
            {
                arg = arg.Replace("[decreasePlayerStuff]", "");
                StuffType stuffType = Auxiliary.GetInvokableLoose<StuffType>(arg, Ubik.Instance.StuffTypes);
                if (stuffType == null)
                {
                    Debug.LogWarning("Parsing : [decreasePlayerStuff] : StuffType not found");
                    continue;
                }                
                Stuff[] playerInv = Main.Instance.Player.Inventory.ToArray();
                for (int x = 0; x < playerInv.Length; x++)
                {
                    Stuff selStuff = playerInv[x];
                    if (selStuff.Type == stuffType)
                    {
                        arg = arg.Replace(selStuff.Type.Invoke, "");
                        int.TryParse(arg, out int amount);
                        Stuff newStuff = null;
                        if (selStuff.amount < amount)
                        {
                            newStuff = new Stuff(selStuff.Type, selStuff.amount);
                            Main.Instance.Player.LoseStuff(newStuff);
                        }
                        else
                        {
                            newStuff = new Stuff(selStuff.Type, amount);
                            Main.Instance.Player.LoseStuff(newStuff);
                        }
                    }
                }
                
            }
            if (arg.Contains("[increasePlayerStuff]"))
            {
                arg = arg.Replace("[increasePlayerStuff]", "");
                StuffType stuffType = Auxiliary.GetInvokableLoose<StuffType>(arg, Ubik.Instance.StuffTypes);
                if (stuffType == null)
                {
                    Debug.LogWarning("Parsing : [increasePlayerStuff] : StuffType not found");
                    continue;
                }
                Stuff[] playerInv = Main.Instance.Player.Inventory.ToArray();
                for (int x = 0; x < playerInv.Length; x++)
                {
                    Stuff selStuff = playerInv[x];
                    if (selStuff.Type == stuffType)
                    {
                        arg = arg.Replace(selStuff.Type.Invoke, "");
                        int.TryParse(arg, out int amount);
                        Stuff newStuff = new Stuff(selStuff.Type, amount);
                        Main.Instance.Player.GainStuff(newStuff);
                    }
                }

            }
        }
    }

    public static bool Requirements(string[] requirements)
    {
        return Requirements(requirements, new Context());
    }
    public static bool Requirements(string[] requirements, Context context)
    {
        bool output = true;
        for (int i = 0; i < requirements.Length; i++)
        {
            string req = requirements[i];
            if (req.Contains("[debug]"))
            {
                Debug.Log(req);
            }
            if (req.Contains("[false]"))
            {
                return false;
            }
            if (req.Contains("[playerStuff]"))
            {
                req = req.Replace("[playerStuff]", "");
                StuffType selType = Auxiliary.GetInvokableLoose<StuffType>(req, Ubik.Instance.StuffTypes);
                if (selType == null)
                {
                    Debug.LogWarning("Parsing : [playerStuff] : StuffType not found");
                    continue;
                }
                Stuff[] playerInv = Main.Instance.Player.Inventory.ToArray();
                bool hasType = false;
                bool hasAmount = false;
                for (int x = 0; x < playerInv.Length; x++)
                {
                    Stuff selStuff = playerInv[x];
                    if (selStuff.Type == selType) 
                    {
                        hasType = true;
                        req = req.Replace(selStuff.Type.Invoke, "");
                        int.TryParse(req, out int amount);
                        if (selStuff.amount >= amount)
                        {
                            hasAmount = true;
                        }
                    }
                }
                if (!hasType || !hasAmount) 
                {
                    output = false;
                    return output;
                }
            }
        }
        return output;
    }
}
[System.Serializable]
public class Context
{
    public Tile[] Tiles;
    public Block[] Blocks;
    public UI[] UIs;
    public Stuff Stuff;
}
[System.Serializable] 
public class Instruction
{
    public string[] DisplayRequirements;
    public string[] ExecutionRequirements;
    public string[] SuccessRequirements;
    public string[] SuccessArguments;
    public string[] FailureArguments;        
}
