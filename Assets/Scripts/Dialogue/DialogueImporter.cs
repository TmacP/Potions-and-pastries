using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using static Unity.VisualScripting.Member;

public class DialogueImporter : MonoBehaviour
{
    
    [MenuItem("Tools/ImportQuips")]
    public static void ImportQuips()
    {

        Debug.Log("************IMPORTING QUIPS*******************");
        string QuipsPath = Path.Combine("Assets", "Scripts", "Dialogue", "Dialogue - Quips.tsv");
        string QuipsOutputPath = Path.Combine("Assets", "Scripts", "Dialogue", "QuipsContainer.asset");

        //DialogueDataContainer.Data.Clear();
        using (StreamReader sr = new StreamReader(QuipsPath))
        {
            
            DialogueDataContainer Container = ScriptableObject.CreateInstance<DialogueDataContainer>();

            Container.Data = new List<DialogueData>();

            //Assume the first line is a header
            string line = sr.ReadLine();

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                string[] cells = line.Split("\t");

                if (cells.Length < 1) continue;
                int ID = -1;
                try
                {
                    ID = int.Parse(cells[0]);
                }
                catch
                {
                    continue;
                }
                DialogueData data = new DialogueData();

                data.ID = ID;
                data.Dialogue = cells[1].Trim();

                string[] triggers = ParseTriggers(cells[2]);

                foreach (string trigger in triggers)
                {
                    DialogueCondition condition = new DialogueCondition();
                    string[] pair = { trigger.Trim() };


//********************First check the comparison we either have < or = for now
                    if (trigger.Contains('<'))
                    {
                        condition.Comparison = EDialogueConditionComparison.LessThan;
                        pair = trigger.Trim().Split('<');
                    }
                    else //assume it is an equals sign or implied equals sign
                    {
                        condition.Comparison = EDialogueConditionComparison.Equals;
                        if (trigger.Contains('='))
                        {
                            pair = trigger.Trim().Split('=');
                        }
                    }

//********************First check the comparison we either have < or = for now
                    if (pair.Length > 1)
                    {
                        condition.ConditionValue = pair[1].Trim();
                    }
                    else
                    {
                        condition.ConditionValue = "True";
                    }

                    ParseKey(pair[0], condition);

                    data.Conditions.Add(condition);

                }
                Container.Data.Add(data);
            }


            Debug.Log(Container.Data.Count + " lines found");

            AssetDatabase.DeleteAsset(QuipsOutputPath);
            AssetDatabase.CreateAsset(Container, QuipsOutputPath);
            EditorUtility.SetDirty(Container);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }


    public static string[] ParseTriggers(string Triggers)
    {
        Triggers = Triggers.Trim();

        //This is gross but taken from here: https://stackoverflow.com/questions/3147836/c-sharp-regex-split-commas-outside-quotes
        string[] OutTriggers = Regex.Split(Triggers, "[,]{1}(?=(?:[^]]*][^]]*])*(?![^]]*]))");

        return OutTriggers;
    }

    public static void ParseKey(string key, DialogueCondition Condition)
    {
        string[] pair = key.Trim().Split(".");

        Assert.IsTrue(pair.Length > 0);

        EDialogueConditionProperty PropertyValue = EDialogueConditionProperty.None;
        EDialogueConditionSource SourceValue = EDialogueConditionSource.None;

        //Not all sources need properties 
        if (pair.Length > 1)
        {
            string source = pair[1].Trim();
            try
            {
                Enum.TryParse(source, out PropertyValue);
            }
            catch { } 

        }
        try
        {
            Enum.TryParse(pair[0].Trim(), out SourceValue);
        }
        catch { }

        Condition.Property = PropertyValue;
        Condition.Source = SourceValue;
    }
}
