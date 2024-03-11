using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HumanBase : MonoBehaviour
{
    [SerializeField]
    public NPC.NPCType npcType;
    NPC npc;
    [SerializeField]
    public StatesAndResources.resourceType holding = StatesAndResources.resourceType.none;
    public StatesAndResources.resourceType Holding
    {
        get
        {
            return holding;
        }
        set
        {
            holding = value;
        }
    }

    public float Hunger = 100;
    public float hungerMax = 100;
    [SerializeField]
    float speed = 5;
    [SerializeField]
    float hungerRate = 0.5f;
    [SerializeField]
    Transform goalPosition;
    ResourceBase resbase;
    [SerializeField]
    StatesAndResources.resourceType desiredResource = StatesAndResources.resourceType.none;
    
    [SerializeField]
    float interactionDistance = 5;

    NavMeshAgent nma;

    float searchRange = 50f;

    private float timer;
    public float Timer
    {
        get { return timer; }
    }
    Animator anim;
    public enum state
    {
        travelling,
        interacting,
        idle
    }

    state currentState = state.idle;
    bool busy = false;
    public bool timerActive = false;
    public float timerMax;
    [SerializeField]
    Image holdingImage;
    //this holding image will display nothing if empty, but something if holding an item

    public void initNPC()
    {
        hungerRate = npc.hungerRate;
        speed = npc.speed;
        desiredResource = npc.NPCDesire;
    }
    public void initNPC(NPC stats)
    {
        npc = stats;
        hungerRate = stats.hungerRate;
        speed = stats.speed;
        desiredResource = stats.NPCDesire;
    }
    public void initNPC(NPC.NPCType stat)
    {
        NPC stats = new NPC(stat);
        npc = stats;
        hungerRate = stats.hungerRate;
        speed = stats.speed;
        desiredResource = stats.NPCDesire;
    }
    private void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        npc = new NPC(npcType);
        initNPC();
    }


    public void changeResource(StatesAndResources.resourceType r)
    {
        holding = r;
        //Debug.Log(r);
    }
    
    private void Update()
    {
        anim.SetFloat("speed", nma.velocity.magnitude);

        switch (currentState)
        {
            case state.travelling:
                checkTravel();
                //Debug.Log("Travel");
                //Debug.Log("goal and destination - " + nma.destination + " - " + goalPosition.transform.position);

                break;
            case state.interacting:
                checkInteract();
               // Debug.Log("interact");
                break;
            case state.idle:
                searchForTarget(Stockpile.instance);
                //searchForTarget();
                //Debug.Log("idle");
                break;
            default:
                break;
        }

        Hunger -= hungerRate * Time.deltaTime;
    }

    //search for target
    void searchForTarget()
    {
        //find a resourcebase that has the same type as us
        Collider[] c = Physics.OverlapSphere(transform.position, searchRange);
       // Debug.Log("searching for target general");

        foreach (Collider item in c)
        {
            if (item.TryGetComponent<ResourceBase>(out ResourceBase rb))
            {
                if (rb.resource == desiredResource && !rb.isExhausted)
                {
                    //Debug.Log("Item Found " + rb.name);
                    clearTravel(rb);
                    //startTravel(rb);
                }
            }
        }
    }
    void searchForTarget(Stockpile st)
    {
        //find a resourcebase that has the same type as us
        Collider[] c = Physics.OverlapSphere(transform.position, searchRange);

        //Debug.Log("searching for target stockpile");
        foreach (Collider item in c)
        {
            if (item.TryGetComponent<Stockpile>(out Stockpile rb))
            {
                if (rb == Stockpile.instance)
                {
                    //Debug.Log("STOCKPILE FOUND");
                    //startTravel(rb);
                    clearTravel(rb);
                }
            }
        }
    }
    

    void startTravel(ResourceBase rb)
    {
        goalPosition = rb.transform;
        resbase = rb;
        busy = true;
        currentState = state.travelling;
        //timer = 0;
        nma.SetDestination(goalPosition.position);
        nma.destination = goalPosition.transform.position;
        //Debug.Log("Starting travel to " + goalPosition.transform.name + goalPosition.position + " ");//+ nma.);
    }


    void clearTravel(ResourceBase r)
    {
        //Debug.Log("clearing path");
        if (nma.hasPath)
            nma.ResetPath();
        else
            nma.Warp(GameObject.Find("Spawnpoint").transform.position);
        anim.ResetTrigger("interact");
        //nma.SetDestination();
        startTravel(r);
    }

    void checkTravel()
    {
        if (Vector3.Distance(goalPosition.transform.position, transform.position) < interactionDistance)
        {
            //close enough to interact
            //Debug.Log("distance is close enough " + Vector3.Distance(goalPosition.transform.position, transform.position));
            startInteract();
        }
        //Debug.Log((Vector3.Distance(goalPosition.transform.position, transform.position) < interactionDistance )+ " " + Vector3.Distance(goalPosition.transform.position, transform.position) + " " );
        //nma.SetDestination(goalPosition.position);
        //nma.destination = goalPosition.position;
    }
    void startInteract()
    {
        //Debug.Log("Starting Interacting");
        timer = 0;
        currentState = state.interacting;
        nma.isStopped = true;//stay in place
        anim.SetTrigger("interact");
        timerActive = true;
        timerMax = resbase.InteractionTime;
        //Debug.Log("distance to interaction point " + Vector3.Distance(goalPosition.transform.position, transform.position));
    }
    void checkInteract()
    {
        if (timer >= resbase.InteractionTime)
        {
            busy = false;
            resbase.interact(this);
            //Debug.Log("Interacted with resource " + resbase);

            //holding = resbase.resource;
            
            anim.SetTrigger("interact");
            timerActive = false;
            goalPosition = null;
            resbase = null;
            //when we finish interacting we take the resource from the pile, then move it to the stockpile
            //or move from the stockpile,
            if (holding == StatesAndResources.resourceType.none)
            {
                //if we are holding nothing, go search for something
                //Debug.Log("Going to search for target");
                //currentState = state.travelling;
                searchForTarget();
            }
            else if(holding != StatesAndResources.resourceType.none)
            {
                //startTravel(Stockpile.instance);//travel to the stockpile
                //Debug.Log("HOLDING DESIRED go to stockpile instance ");//- not doing timer " + timer);

                searchForTarget(Stockpile.instance);
                //we are holding desired resource, go to stockpile to interact
                //startTravel(Stockpile.instance);

                //currentState = state.travelling;
            }
            //startTravel();
        }
        else
        {
            timer += Time.deltaTime;
            //Debug.Log("doing timer " + timer);

            nma.isStopped = true;
        }
    }
    //if desire is none, just wander around and take food


    //idle - travel to resource - interact - travel to stockpile - interact - travel to resource - travel to stockpile
    //Eddy / Guy, whoever reads this - please understand my frustration with Unity's horrible, terrible, no good AI system.
    //Why did I spend the last 6 hours trying to work out why this wasnt working - why did I have to just clear the path and reassign it? 
    //why couldnt I just set the new destination. 
    //this looks ugly with many blocks of green commented out text. Please understand it was made with equal parts frustration and dedication

    //basically 


}


public struct NPC
{
    public enum NPCType
    {
        miner,
        peasant,
        axeman,
        king
    }
    public StatesAndResources.resourceType NPCDesire;

    public float hungerRate;
    public float speed;

    public int costAmount;
    public StatesAndResources.resourceType costType;

    public NPC(NPCType t)
    {
        switch (t)
        {
            case NPCType.miner:
                speed = 3.5f;
                hungerRate = 0.2f;
                costType = StatesAndResources.resourceType.wood;

                NPCDesire = StatesAndResources.resourceType.stone;
                costAmount = 3;
                break;
            case NPCType.peasant:
                speed = 5f;
                hungerRate = 0.1f;
                costType = StatesAndResources.resourceType.stone;
                costAmount = 5;
                NPCDesire = StatesAndResources.resourceType.fruit;
                break;
            case NPCType.axeman:
                speed = 4;
                hungerRate = 0.2f;
                costType = StatesAndResources.resourceType.fruit;
                costAmount = 8;
                NPCDesire = StatesAndResources.resourceType.wood;
                break;
            case NPCType.king:
                speed = 2f;
                hungerRate = 0.1f;
                costType = StatesAndResources.resourceType.gold;
                costAmount = 10;
                NPCDesire = StatesAndResources.resourceType.none;
                break;
            default:
                speed = 1;
                hungerRate = 1;
                costType = StatesAndResources.resourceType.none;
                costAmount = 1;
                NPCDesire = StatesAndResources.resourceType.none;
                break;
        }
    }
    

}

