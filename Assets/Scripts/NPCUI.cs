using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NPCUI : MonoBehaviour
{
    [SerializeField]
    Image holdingImg;
    [SerializeField]
    Sprite rockSprite, fruitsprite, goldsprite, woodsprite;
    [SerializeField]
    Slider slider;
    HumanBase human;
    [SerializeField]
    TextMeshProUGUI nameText;
    [SerializeField]
    TextMeshProUGUI timerText;

    void Start()
    {
        slider.interactable = false;
        human = GetComponent<HumanBase>();
        slider.maxValue = human.hungerMax;
        slider.value = human.Hunger;
        nameText.text = "" + human.npcType;
    }

    void Update()
    {
        slider.value = human.Hunger;

        if (human.timerActive)
        {
            timerText.gameObject.SetActive(true);

            timerText.text = string.Format("{0:0.0}", human.Timer) + " / " + human.timerMax;
        }
        else
        {
            timerText.gameObject.SetActive(false);
        }
        switch (human.holding)
        {
            case StatesAndResources.resourceType.none:
                holdingImg.sprite = null;
                holdingImg.color = new Color(0,0,0,0);
                break;
            case StatesAndResources.resourceType.wood:
                holdingImg.sprite = woodsprite;
                holdingImg.color = new Color(1, 1, 1, 1);
                break;
            case StatesAndResources.resourceType.stone:
                holdingImg.sprite = rockSprite;
                holdingImg.color = new Color(1, 1, 1, 1);

                break;
            case StatesAndResources.resourceType.fruit:
                holdingImg.sprite = fruitsprite;
                holdingImg.color = new Color(1, 1, 1, 1);

                break;
            case StatesAndResources.resourceType.gold:

                holdingImg.color = new Color(1, 1, 1, 1);
                holdingImg.sprite = goldsprite;
                break;
            default:
                break;
        }

    }
}
