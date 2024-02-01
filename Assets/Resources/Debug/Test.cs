using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : Command
{    
    public override string Execute(string[] args)
    {
        Debug.Log("est args: " + string.Join(" ", args));
        Debug.Log("Test Executed successfully");

        return "";
    }

    public override string Help()
    {
        return "A test command";
    }
}
