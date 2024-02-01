using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    List<GameObject> commands;
    // Start is called before the first frame update
    void Start()
    {
        MonoScript[] scripts = Resources.LoadAll<MonoScript>("Debug");
        Debug.Log("Commands Loaded: " + scripts.Length);

        commands = new List<GameObject>();
        GameObject newCommand;

        foreach (MonoScript command in scripts)
        {
            Debug.Log(command.name);

            newCommand = new GameObject(command.name);
            newCommand.AddComponent(command.GetClass());
            commands.Add(newCommand);
        }
    }

    public void Execute(string text)
    {
        if (commands == null)
        {
            Debug.Log("No available debug commands");
            return;
        }

        List<string> args = text.Split(' ').ToList();
        if (args.Count == 0) { return; }

        string commandName = args[0];
        args.RemoveAt(0);
       
        foreach (GameObject command in commands)
        {
            Debug.Log(commandName);
            if (commandName == command.name)
            {
                command.GetComponent<Command>().Execute(args.ToArray());
                return;
            }
        }
        Debug.Log("No commands by name " + commandName + " found");
    }
}
