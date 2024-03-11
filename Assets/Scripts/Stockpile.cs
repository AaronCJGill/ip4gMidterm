using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stockpile : ResourceBase
{
    public static Stockpile instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else if (instance == null)
        {
            instance = this;
            resource = StatesAndResources.resourceType.none;

            inexhaustible = true;
            usesText.gameObject.SetActive(false);
        }
    }

    public override void interact(HumanBase hb)
    {
        //takes in item that the npc is holding
        StageManager.addResource(hb.Holding);
        //Debug.Log("Resource added: " + hb.holding);
        hb.changeResource(StatesAndResources.resourceType.none);
        //Debug.Log("Taking in held item");


        if (hb.Hunger < hb.Hunger * 0.4f)
        {
            hb.Hunger = 100;
        }
    }
}
