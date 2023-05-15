using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    //Card Display Variables
    public string cardName;
    public string description;
    public string cardCategory;
    public string cardColor;
    public int cardEra;
    public Sprite art;

    //Variables to Control and Set the Card Displays With
    public TextMeshProUGUI cardNameHolder; 
    public TextMeshProUGUI cardDescriptionHolder;
    public TextMeshProUGUI cardCategoryHolder;
    public GameObject cardBackingHolder;
    public TextMeshProUGUI cardEraHolder;
    public RawImage cardImageHolder;

    //Bools for card related logic
    public bool isGovernmentCard;
    public bool isAllianceCard;
    public bool isDeclareWarCard;
    public bool isPeaceTreatyCard;
    public bool isPurchasable;
    
    //Bools for card related buildings and board manipulation
    public bool createsBuildings;
    public bool createdUnits;

    //Choose the Government if it is a Government Card
    public string governmentType;
    public Government government;

    //Choose the Alliance if it is an Alliance Card
    public string governmentAlliance;

    //Choose the Government to Declare War on if it is that Card Type
    public string governmentWarDecleration;

    //Card Effects / Costs
    public int goldEffect;
    public int silverEffect;
    public int empireEffect;
    public int wearinessEffect;
    public int loyaltySabotageEffect;
    public int religionEffect;

    //Card Impacts (Buildings, Units, etc)
    public List<GameObject> buildings = new List<GameObject>();
    public List<GameObject> units = new List<GameObject>();
    public List<GameObject> governmentTypeWarSupportEffect = new List<GameObject>();
}
