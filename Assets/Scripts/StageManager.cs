using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class StageManager : MonoBehaviour
{
    ///keeps track of entire game progress
    //resource trackers
    [SerializeField]
    int stoneAmount = 0;
    [SerializeField]
    int woodAmount = 0;
    [SerializeField]
    int fruitAmount = 0;
    [SerializeField]
    int goldAmount = 0;

    [SerializeField]
    int kingCount = 0;

    [SerializeField]
    TextMeshProUGUI woodText, stoneText, fruitText, goldText;
    [SerializeField]
    TextMeshProUGUI kingText;
    public static StageManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else if (instance == null)
        {
            instance = this;
        }
        updateUI();
    }

    void updateUI()
    {
        woodText.text = woodAmount.ToString();
        stoneText.text = stoneAmount.ToString();
        goldText.text = goldAmount.ToString();
        fruitText.text = fruitAmount.ToString();

        kingText.text = "Kings: " + kingCount + "/3";

    }

    private void Update()
    {
        if (kingCount >= 3)
        {
            instance.updateUI();
            SceneManager.LoadScene(2);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            //addKing();
        }
    }

    public static void addResource(StatesAndResources.resourceType type)
    {
        switch (type)
        {
            case StatesAndResources.resourceType.wood:
                instance.woodAmount++;
                break;
            case StatesAndResources.resourceType.stone:
                instance.stoneAmount++;
                break;
            case StatesAndResources.resourceType.fruit:
                instance.fruitAmount++;
                break;
            case StatesAndResources.resourceType.gold:
                instance.goldAmount++;
                break;
            default:
                break;
        }
        instance.updateUI();

    }
    public static void addKing()
    {
        instance.kingCount++;
        instance.updateUI();
    }

    public static bool canSubtractResources(int amnt, StatesAndResources.resourceType type)
    {
        switch (type)
        {
            case StatesAndResources.resourceType.wood:
                if (amnt <= instance.woodAmount)
                {
                    return true;
                }
                break;
            case StatesAndResources.resourceType.stone:
                if (amnt <= instance.stoneAmount)
                {
                    return true;
                }
                break;
            case StatesAndResources.resourceType.fruit:
                if (amnt <= instance.fruitAmount)
                {
                    return true;
                }
                break;
            case StatesAndResources.resourceType.gold:
                if (amnt <= instance.goldAmount)
                {
                    return true;
                }
                break;
            default:
                return false;
                break;
        }
        return false;
    }

    public static void buyResource(int amnt, StatesAndResources.resourceType type)
    {
        //on our side just remove the amount, the spawn manager will buy unit. Ideally unit will have all of its spawn info listed
        if (canSubtractResources(amnt, type))
        {
            switch (type)
            {
                case StatesAndResources.resourceType.wood:
                    instance.woodAmount -= amnt;
                    break;
                case StatesAndResources.resourceType.stone:
                    instance.stoneAmount -= amnt;
                    break;
                case StatesAndResources.resourceType.fruit:
                    instance.fruitAmount -= amnt;
                    break;
                case StatesAndResources.resourceType.gold:
                    instance.goldAmount -= amnt;
                    break;
                default:
                    break;
            }
        }
    }


}
