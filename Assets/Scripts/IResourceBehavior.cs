using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatesAndResources
{
    public interface IInteractableBehavior
    {

        void interact(HumanBase hb);
        void playSound();

    }

    public enum resourceType
    {
        none,
        wood,
        stone,
        fruit,
        gold
    }

    public enum techlvl
    {
        levelOne,
        levelTwo,
        levelThree
    }
}