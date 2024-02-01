using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : MonoBehaviour
{
    /**
     * Executes some arbitrary command 
     * returns a string explaining whats happened or containing an error
     */
    public abstract string Execute(string[] args);

    /**
     * Returns a String containing instructions on how to use a command
     */
    public abstract string Help();
}
