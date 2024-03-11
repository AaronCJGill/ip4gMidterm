using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourceBase : MonoBehaviour, StatesAndResources.IInteractableBehavior
{
    //amount of resources this thing has
    [SerializeField]
    private protected int uses = 100;

    public StatesAndResources.resourceType resource = StatesAndResources.resourceType.none;

    public bool isExhausted
    {
        get { return uses < 0; }
    }

    //if all of the resources can be used up
    [SerializeField]
    protected bool inexhaustible = false;
    [SerializeField]
    private int interactionTime = 5;
    public int InteractionTime
    {
        get { return interactionTime; }
    }

    Sprite img;
    [SerializeField]
    Image icon;
    [SerializeField]
    string resourceName;
    [SerializeField]
    Sprite spr;
    [SerializeField]
    protected TextMeshProUGUI NameText, usesText;

    [SerializeField]
    float useTime = 3f;

    [SerializeField]
    AudioSource source;
    [SerializeField]
    AudioClip clip;


    private void Start()
    {
        initializeUI();
        source = GetComponent<AudioSource>();
        source.clip = clip;
        icon.sprite = img;
    }
    private void initializeUI()
    {
        icon.sprite = img;
        NameText.text = resourceName;
        usesText.text = uses.ToString();
    }

    public virtual void interact(HumanBase hb)
    {
        hb.changeResource(resource);
        uses = (inexhaustible) ? uses : uses - 1;
        usesText.text = uses.ToString();
    }

    public void playSound()
    {
        source.Play();
    }


}
