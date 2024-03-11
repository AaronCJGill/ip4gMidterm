using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    //when a button is pressed, creates an NPC of a certain type
    [SerializeField]
    Transform spawnpoint;
    [SerializeField]
    Button minerButton, peasantButton, axemanbutton, kingbutton;
    [SerializeField]
    GameObject axeman;
    [SerializeField]
    GameObject peasant;
    [SerializeField]
    GameObject king;
    [SerializeField]
    GameObject miner;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && StageManager.canSubtractResources(3, StatesAndResources.resourceType.wood))
        {
            //miner
            StageManager.buyResource(3, StatesAndResources.resourceType.wood);
            GameObject g = Instantiate(miner, spawnpoint);
            g.GetComponent<HumanBase>().initNPC(NPC.NPCType.miner);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && StageManager.canSubtractResources(5, StatesAndResources.resourceType.stone))
        {
            //peasant
            StageManager.buyResource(5, StatesAndResources.resourceType.stone);
            GameObject g = Instantiate(peasant, spawnpoint);
            g.GetComponent<HumanBase>().initNPC(NPC.NPCType.peasant);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && StageManager.canSubtractResources(4, StatesAndResources.resourceType.fruit))
        {
            //axeman
            StageManager.buyResource(4, StatesAndResources.resourceType.fruit);
            GameObject g = Instantiate(axeman, spawnpoint);
            g.GetComponent<HumanBase>().initNPC(NPC.NPCType.axeman);

        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && StageManager.canSubtractResources(10, StatesAndResources.resourceType.gold))
        {
            //king
            StageManager.buyResource(10, StatesAndResources.resourceType.gold);
            GameObject g = Instantiate(king, spawnpoint);
            g.GetComponent<HumanBase>().initNPC(NPC.NPCType.king);
            StageManager.addKing();
        }


        //check if we can purchase
        if (StageManager.canSubtractResources(10, StatesAndResources.resourceType.gold))
        {
            //king uses 10 gold
            kingbutton.interactable = true;
        }
        else
        {
            kingbutton.interactable = false;
        }

        if (StageManager.canSubtractResources(3, StatesAndResources.resourceType.wood))
        {
            //miner uses 3 wood
            minerButton.interactable = true;
        }
        else
        {
            minerButton.interactable = false;
        }

        if (StageManager.canSubtractResources(4, StatesAndResources.resourceType.fruit))
        {
            //axeman uses 4 fruit
            axemanbutton.interactable = true;
        }
        else
        {
            axemanbutton.interactable = false;
        }
        if (StageManager.canSubtractResources(5, StatesAndResources.resourceType.stone))
        {
            //peasant uses 5 stone
            peasantButton.interactable = true;
        }
        else
        {
            peasantButton.interactable = false;
        }


    }



}
