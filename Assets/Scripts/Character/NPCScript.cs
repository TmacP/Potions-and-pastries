using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class NPCBehaviour : MonoBehaviour, IInteractableExtension

{
    public enum ENPCState
    {
        None = 0,
        ForeverIdle,
        Wander,
        Idle,
        FindTable,
        DecideOnOrder,
        WaitForOrder,
        Eating,
        Leaving // foundTable = false;
    }

    public ENPCState NPCState;

    public ENPCState NextNPCState = ENPCState.None;
    public DialogueBehavoir _DialogueBehavoir;

    //[SerializeField] public NPCData Data;
    public NPCCharacterData CharacterData;
    NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer;

    //for setting points to walk to (Wander or FindTable)
    public Vector3 destination;
    public bool pointSet;
    [SerializeField] float walkingRange;
    TableChair tableRef;

    [SerializeField] OrderData NpcOrder;

    bool foundTable;
    bool foundDoor;
    int LikeFoodBonus = 2;

    public GameObject GoodOrderVFX;
    public GameObject BadOrderVFX;
    public GameObject TargetVFX;
    public float VFXLifetime = 1.5f;
    
    // Animation variables
    private Rigidbody _Rigidbody;
    public Animator frontAnimator;
    public Animator backAnimator;
    private bool faceBack = false;
    private bool faceLeft = true;
    private Vector3 direction;


    //*************IInteractable interface***********
    public string InteractionPrompt => GetInteractionPrompt();

    public string GetSecondaryInteractionPrompt(InventoryItemData InteractionItem)
    {
        string ReturnPrompt = "Give Card";

        if (InteractionItem == null)
        {
            return ReturnPrompt;
        }

        if (InteractionItem.bIsCard && InteractionItem.CardActionType == ECardActionType.Use_Trash)
        {
            ReturnPrompt = "Give " + InteractionItem.Data.Name;
        }
        else
        {
            ReturnPrompt = "Show " + InteractionItem.Data.Name;
        }
        return ReturnPrompt;
    }

    public string GetThirdInteractionPrompt()
    {
        return "";
    }

    public EInteractionResult TryInteract(InteractorBehavoir InInteractor, List<InventoryItemData> InteractionItem = null)
    {

       //if(NPCState == ENPCState.WaitForOrder && InteractionItem != null && InteractionItem.Count > 0)
       //{
       //     GameEventManager.instance.DoneNPCOrder(NpcOrder);
       //     GameEventManager.instance.RemovePlayerItems(InteractionItem);
       //     WaitSecChangeState(0.5f, ENPCState.Eating);
       // }
       //else
       if(_DialogueBehavoir != null)
       {
            CreateDialogueState();
            _DialogueBehavoir.TryDialogue();
            return EInteractionResult.Success;
       }
       return EInteractionResult.Failure;
    }

    public EInteractionResult TrySecondaryInteract(InteractorBehavoir InInteractor, List<InventoryItemData> InteractionItems = null)
    {
        if(InteractionItems == null || InteractionItems.Count <= 0)
        {
            return EInteractionResult.Failure;
        }
        InventoryItemData Item = InteractionItems[0];
        if (NPCState == ENPCState.WaitForOrder && Item != null && Item.bIsFinalRecipe)
        {
            GameEventManager.instance.DoneNPCOrder(NpcOrder);

            //GameEventManager.instance.RemovePlayerItems(InteractionItem);
            GameEventManager.instance.Purchase(-EvaluateOrder(InteractionItems));
            WaitSecChangeState(0.5f, ENPCState.Eating);
            return EInteractionResult.Success_ConsumeItem;
        }
        return EInteractionResult.Failure;
    }

    public EInteractionResult TryThirdInteract(InteractorBehavoir InInteractor)
    {
        return EInteractionResult.Failure;
    }

    //*******end of IInteractable

    public string GetInteractionPrompt()
    {
        return "Speak";
    }

    public void Awake()
    {
        _DialogueBehavoir = GetComponent<DialogueBehavoir>();

        _Rigidbody = GetComponent<Rigidbody>();
        frontAnimator = transform.Find("F_BaseCharacter").GetComponent<Animator>();
        backAnimator = transform.Find("B_BaseCharacter").GetComponent<Animator>();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateNPCState(NPCState);
        foundTable = false;
        foundDoor = false;

    }



    // Update is called once per frame
    void Update()
    {
        //Debug.Log(foundDoor);
        //Debug.Log(Vector3.Distance(agent.transform.position, destination)); //to find out how far an NPC is from the point they are headed to (where the 1.1 came from)

        if (NPCState == ENPCState.Eating)
        {
            WaitSecChangeState(3, ENPCState.Leaving);
        }
        //else if(NPCState == ENPCState.Idle)
       // {
       //     StartIdle();
      //  }
        //at table
        else if (Vector3.Distance(agent.transform.position, destination) < 1.1 && foundTable)
        {
            foundTable = false;
            WaitSecChangeState(3, ENPCState.WaitForOrder);
        }
        //at door to leave
        else if (Vector3.Distance(agent.transform.position, destination) < 1.1 && foundDoor)
        {
            //Debug.Log("I am dead");
            
            Destroy(gameObject);
            foundDoor = false;
        }
        else if (Vector3.Distance(agent.transform.position, destination) < 2 && pointSet && !foundTable && !foundDoor)
        {
            pointSet = false;
            WaitSecChangeState(0, NPCState);
        }

        if (frontAnimator.gameObject.activeSelf == true)
        {
            frontAnimator.SetFloat("MoveSpeed", agent.velocity.magnitude);
            direction = (destination - agent.transform.position) / Time.deltaTime;
        }
        if (backAnimator.gameObject.activeSelf == true)
        {
            backAnimator.SetFloat("MoveSpeed", agent.velocity.magnitude);
            direction = (destination - agent.transform.position) / Time.deltaTime;
        }

        if (direction.x > 0 && faceLeft)
        {
            Vector3 scale = transform.Find("F_BaseCharacter").transform.localScale;
            scale.x *= -1.0f;
            transform.Find("F_BaseCharacter").transform.localScale = scale;
            scale = transform.Find("B_BaseCharacter").transform.localScale;
            scale.x *= -1.0f;
            transform.Find("B_BaseCharacter").transform.localScale = scale; 

            faceLeft = false;
        }
        else if (direction.x < 0 && !faceLeft)
        {
            Vector3 scale = transform.Find("F_BaseCharacter").transform.localScale;
            scale.x *= -1.0f;
            transform.Find("F_BaseCharacter").transform.localScale = scale;
            scale = transform.Find("B_BaseCharacter").transform.localScale;
            scale.x *= -1.0f;
            transform.Find("B_BaseCharacter").transform.localScale = scale;

            faceLeft = true;
        }

        if (direction.y > 0 && !faceBack)
        {
            frontAnimator.Rebind();
            transform.Find("F_BaseCharacter").gameObject.SetActive(false);
            faceBack = true;
            transform.Find("B_BaseCharacter").gameObject.SetActive(true);
        }
        else if (direction.y < 0 && faceBack)
        {
            backAnimator.Rebind();
            transform.Find("F_BaseCharacter").gameObject.SetActive(true);
            faceBack = false;
            transform.Find("B_BaseCharacter").gameObject.SetActive(false);
        }
    }

    private void UpdateNPCState(ENPCState newState)
    {

        if (NPCState != newState)
        {
            //Debug.Log("Changing State");
            NextNPCState = ENPCState.None;
        }

        NPCState = newState;
        
        switch (newState)
        {

            case ENPCState.ForeverIdle:
                //doing nothin
                break;
            case ENPCState.Wander:
                
                StartWander();
                break;
            case ENPCState.Idle:
                StartIdle();
                break;
            case ENPCState.FindTable:
                WalkToTable();
                break;
            case ENPCState.DecideOnOrder:
                WaitSecChangeState(3, ENPCState.WaitForOrder);
                break;
            case ENPCState.WaitForOrder:
                if(GameManager.Instance.OrderCount > 5)
                {
                    pointSet = false;
                    WaitSecChangeState(0.5f, ENPCState.Wander);
                }
                else
                {
                    GenerateOrder();
                    _DialogueBehavoir.State.ProvidingOrder = Time.time.ToString();
                    GameEventManager.instance.TakeNPCOrder(NpcOrder);
                }
                break;
            case ENPCState.Eating:
                GameEventManager.instance.DoneNPCOrder(NpcOrder);
                break;
            case ENPCState.Leaving:
                EatOrLeave();
                //WaitSecChangeState(3, ENPCState.Wander); //temp
                break;
            default:
                //Debug.Log("NPCScript::UpdateNPCState unknown NPC state given");
                break;
        }
    }

    void WanderDest()
    {
        float rangeZ = Random.Range(-walkingRange, walkingRange);
        float rangeX = Random.Range(-walkingRange, walkingRange);

        destination = new Vector3(transform.position.x + rangeX, transform.position.y, transform.position.z + rangeZ);
        
        //returns true if ground is available for walking
        if (Physics.Raycast(destination, Vector3.down, groundLayer))
        {
            pointSet = true;
        }
    }
    void StartWander()
    {
        if (tableRef != null)
        {
            tableRef.EmptySeat(this.gameObject);
            tableRef = null;
        }
        if (!pointSet)
        {
            WanderDest();

        }
        if (pointSet)
        {
            agent.SetDestination(destination);
            WaitSecChangeState(3, ENPCState.Idle);
        }
    }

    void StartIdle()
    {
        //waits until the NPC gets close to the point set by the Wander state
       // if(Vector3.Distance(agent.transform.position, destination) < 1) 
        //{
      //      pointSet = false;
            WaitSecChangeState(3, ENPCState.FindTable);
     //   }
       // else if (Physics.Raycast(destination, Vector3.down, groundLayer))
      //  {
      //      agent.SetDestination(destination);
    //    }
      //  else
      //  {
      //      WaitSecChangeState(3, ENPCState.FindTable);
     //   }

    }


    void FindTablePosition()
    {
        GameObject[] Tables = GameObject.FindGameObjectsWithTag("Table");
        GameObject Table = null;

        foreach(GameObject t in Tables)
        {
            if(t != null)
            {
                TableChair tableChair = t.GetComponent<TableChair>();
                Assert.IsNotNull(tableChair);
                if(tableChair.isEmpty())
                {
                    
                    tableChair.FillSeat(this.gameObject);
                    Table = t;
                    tableRef = tableChair;
                    break;
                }
            }
        }

        if (Table != null)
        {
            destination = Table.transform.position;
            foundTable = true;
            pointSet = false;
        }
        else
        {
            UpdateNPCState(ENPCState.Wander);
        }
    }
    void WalkToTable()
    {
        if (!foundTable)
        {
            FindTablePosition();
        }
        if (foundTable)
        {
 //           if (Vector3.Distance(agent.transform.position, destination) < 1.1)
 //           {
 //               Debug.Log("Made it here");
 //               foundTable = false; 
 //               WaitSecChangeState(3, ENPCState.WaitForOrder);
  //          }
  //          else
    //        {
                agent.SetDestination(destination);
    //        }
        }
    }


    void FindDoorPosition()
    {
        GameObject Door = GameObject.FindGameObjectWithTag("Door");
        if (Door!= null)
        {
            destination = Door.transform.position;
            foundDoor = true;
        }
        else
        {
            UpdateNPCState(ENPCState.Wander);
        }
        //returns true if ground is available for walking
        if (Physics.Raycast(destination, Vector3.down, groundLayer) && destination != null)
        {
            pointSet = true;
        }
    }
    void WalkToDoor()
    {

        GameEventManager.instance.NPCLeavingChair(this.gameObject);
        if (!foundDoor)
        {
            FindDoorPosition();
        }
        if (foundDoor)
        {
 //           if (Vector3.Distance(agent.transform.position, destination) < 0.5)
 //           {
 //               Debug.Log("Made it to door");
  //              foundDoor = false;
  //              Destroy(this.gameObject);
 //           }
 //           else
 //           {
                agent.SetDestination(destination);
//            }
        }

    }

    void EatOrLeave()
    {
        bool ChangeChance = RandomChance(0,200);
        //Debug.Log("CHANCE 1" + ChangeChance);

        // stay and eat more, but do we move or stay at table?
        if(ChangeChance)
        {
            bool ChangeChance2 = RandomChance(0,200);
            //Debug.Log("CHANCE 2" + ChangeChance2);

            // move from table and rerun from wandering state to go find another table
            if (ChangeChance2)
            {
                WaitSecChangeState(5, ENPCState.Wander);
            }
            // stay at table and feast more 
            else
            {
                WaitSecChangeState(3, ENPCState.WaitForOrder);
            }
        }
        // done eating and leaving (deleting NPC)
        else
        {
            WalkToDoor();
        }
    }




    public void WaitSecChangeState(float seconds, ENPCState newStateChange)
    {
        if (this.NextNPCState == ENPCState.None && newStateChange != this.NPCState)
        {
            this.NextNPCState = newStateChange;
            Invoke("OnUpdateNPCState", seconds);
            //set state here
        }
    }

    void OnUpdateNPCState()
    {
        UpdateNPCState(NextNPCState);
        //update state here
    }

    // splits the chance 50/50
    private bool RandomChance(float minNum, float maxNum)
    {
        float RandNum = Random.Range(minNum, maxNum);

        if(RandNum < (maxNum / 2) )
        {
            return true;
        }
        else 
        {
            return false;
        }
        
    }

    private void GenerateOrder()
    {
        NpcOrder = new OrderData();
        NpcOrder.NPCTarget = this.gameObject;
        DeckManager Deck = PlayerController.instance.GetComponent<DeckManager>();
        Assert.IsNotNull(Deck);

        foreach (EItemTags tag in CharacterData.NPCLikes)
        {
            //I know this is dumb but its an easy quick fix
            if(tag == EItemTags.Buttery ||
               tag == EItemTags.Doughy ||
               tag == EItemTags.Sweet ||
               tag == EItemTags.Salty)
            {
                NpcOrder.NPCLikes.Add(tag);
            }
            else if(Deck.AvailableTags.Contains(tag))
            {
                NpcOrder.NPCLikes.Add(tag);
            }
        }

        NpcOrder.NPCDislikes = CharacterData.NPCDislikes;

        if (NpcOrder.NPCLikes.Count == 0)
        {
            NpcOrder.NPCLikes.AddRange(CharacterData.NPCLikes);
        }
        
    }

    private int EvaluateOrder(List<InventoryItemData> Items)
    {
        _DialogueBehavoir.State.LastRecivedOrder = Time.time.ToString();

        if(Items.Count > 0)
        {
            InventoryItemData item = Items[0];

            int LikeCount = 0;

            foreach(EItemTags tag in item.CurrentItemTags)
            {
                if(NpcOrder.NPCLikes.Contains(tag))
                {
                    LikeCount += 2;
                }
                if(NpcOrder.NPCDislikes.Contains(tag))
                {
                    LikeCount--;
                }
            }

            if(LikeCount >= 0)
            {
                GameObject GO = Instantiate(GoodOrderVFX, this.transform);
                Destroy(GO, VFXLifetime);
            }
            else
            {
                GameObject GO = Instantiate(BadOrderVFX, this.transform);
                Destroy(GO, VFXLifetime);
            }

            return 4 + LikeCount * LikeFoodBonus;
        }
        return 0;

    }

    public void CreateDialogueState()
    {
        _DialogueBehavoir.State.Class = CharacterData.Archetype.ToString();
        _DialogueBehavoir.State.Favourite.Clear();

        _DialogueBehavoir.State.Hungry = "True";
        _DialogueBehavoir.State.Thirsty = "True";
        _DialogueBehavoir.State.ProvidingOrder = "0";
        _DialogueBehavoir.State.LastRecivedOrder = "0";
        foreach (EItemTags tag in CharacterData.NPCLikes)
        {
            _DialogueBehavoir.State.Favourite.Add(tag.ToString());
        }
    }

    public void ShowTarget()
    {
        TargetVFX.SetActive(true);
        Invoke("ClearVFX", 1.9f);
    }

    public void ClearVFX()
    {
        TargetVFX.SetActive(false);
    }
}
