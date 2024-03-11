using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneResource : ResourceBase
{
    private void Awake()
    {
        resource = StatesAndResources.resourceType.stone;
    }

    //stone has a 10% chance to give gold instead of stone
    public override void interact(HumanBase hb)
    {
        int x = Random.Range(0,20);
        if (x == 1)
        {
            hb.changeResource(StatesAndResources.resourceType.gold);
        }
        else
        {
            hb.changeResource(resource);
        }
        uses = (inexhaustible) ? uses : uses - 1;
        usesText.text = "Uses: " + uses.ToString();
    }
}
