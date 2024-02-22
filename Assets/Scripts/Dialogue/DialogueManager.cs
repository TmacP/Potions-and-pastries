using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public DialogueDataContainer DataContainer;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public DialogueData GetDialogue(NPCDialogueState NPC)
    {
        DialogueData data = null;

        List<DialogueData> PassedDialogues = new List<DialogueData>();

        int i = 0;
        foreach(DialogueData Data in DataContainer.Data)
        {
            bool ConditionPassed = true;
            foreach (DialogueCondition Condition in Data.Conditions)
            {
                switch(Condition.Source)
                {
                    case EDialogueConditionSource.NPCState:
                        if(!CheckNPCState(Condition, NPC))
                        {
                            ConditionPassed = false;
                        }
                        break;
                    case EDialogueConditionSource.GameState:
                        if(!CheckGameState(Condition))
                        {
                            ConditionPassed = false;
                        }
                        break;
                    case EDialogueConditionSource.PlayerState:
                        if(!CheckPlayerState(Condition))
                        {
                            ConditionPassed = false;
                        }
                        break;
                }
                if(!ConditionPassed)
                {
                    break;
                }
            }

            if(ConditionPassed)
            {
                PassedDialogues.Add(Data);
            }
            i++;

        }
       
        if(PassedDialogues.Count > 0)
        {
            int MaxConditions = PassedDialogues.Max(D => D.Conditions.Count);
            PassedDialogues = PassedDialogues.Where(D => D.Conditions.Count == MaxConditions).ToList();
            int Index = Random.Range(0, PassedDialogues.Count);
            data = PassedDialogues[Index];
        }
        return data;
    }

    private bool CheckNPCState(DialogueCondition Condition, NPCDialogueState NPC)
    {
        string Property = Condition.Property.ToString();


        switch (Property)
        {
            case "Favourite":
                if(NPC.Favourite.Contains(Condition.ConditionValue))
                {
                    return true;
                }
                return false;
            default:
                string Value = NPC.GetType().GetField(Property).GetValue(NPC).ToString();

                if(Condition.Comparison == EDialogueConditionComparison.Equals)
                {
                    if (Value == Condition.ConditionValue)
                    {
                        return true;
                    }
                    break;
                }
                else if(Condition.Comparison == EDialogueConditionComparison.LessThan)
                {
                    if(float.Parse(Value) < float.Parse(Condition.ConditionValue))
                    {
                        return true;
                    }

                    break;
                }
                break;
        }
        return false;
    }

    private bool CheckGameState(DialogueCondition Condition)
    {
        return true;
        //switch(Condition.ConditionValue)
        //{
        //    case "Night":
        //        if(GameManager.Instance.GetGameScene() == EGameScene.InnInterior)
        //        {
        //            return true;
        //        }
        //        return false;
        //    case "Day":
        //        if (GameManager.Instance.GetGameScene() == EGameScene.InnExterior)
        //        {
        //            return true;
        //        }
        //        return false;
        //    default:
        //        return false;
        //}
    }


    private bool CheckPlayerState(DialogueCondition Condition)
    {
        return true;
        //switch (Condition.ConditionValue)
        //{
        //    case "Converse":
        //        return true;
        //    default:
        //        return false;
        //}
    }


}
