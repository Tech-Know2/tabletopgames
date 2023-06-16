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
    public string cardTech;
    public string cardEra;
    //public Sprite art;

    //Multiplayer and Auction House Vars
    public string originalPlayer;

    //Variables to Control and Set the Card Displays With
    /*public TextMeshProUGUI cardNameHolder; 
    public TextMeshProUGUI cardDescriptionHolder;
    public TextMeshProUGUI cardCategoryHolder;
    public GameObject cardBackingHolder;
    public TextMeshProUGUI cardEraHolder;
    public RawImage cardImageHolder;*/

    //Bools for card related logic
    public bool isGovernmentCard;
    public bool isGovernmentWarSupportBoostCard;
    public bool isAllianceCard;
    public bool isDeclareWarCard;
    public bool isPeaceTreatyCard;
    public bool isPurchasable;
    
    //Bools for card related buildings and board manipulation
    public bool createsBuildings;
    public bool createdUnits;

    //Choose the Government if it is a Government Card or Government Related Card
    public string governmentType; // Name of the governemnt created, or the name of the governemnt the war support count will be effecting
    public Government government;

    //Choose the Alliance if it is an Alliance Card
    public string governmentAlliance;

    //Choose the Government to Declare War on if it is that Card Type
    public string governmentWarDecleration;

    //Card Effects / Costs
    public int goldPerTurnEffect;
    public int silverPerTurnEffect;
    public int warSupportAgainstGovernmentEffect;
    public int wearinessEffect;
    public int loyaltySabotageEffect;
    public int religionEffect;

    //Strings to determine the effects on the game
    //"Gold Cost" to take gold per turn
    //"Loyalty Cost" to take loyalty per turn
    //"Silver Cost" to take silver per turn
    //"Food Cost" to take food per turn
    //"People Cost" to take people per turn
    //"War Support Cost" to take War Support per turn
    public bool requiresMultipleTurns; //Enter the amount of turns the card effect lasts
    public int turnEffectCount; //List the amount of change (-10 loyalty, -10 food, etc)
    public string costType; //List types from above
    public int peopleCost;

    //Card Impacts (Buildings, Units, etc)
    public GameObject[] buildings;
    public GameObject[] units;
    public GameObject[] governmentTypeWarSupportEffect;
}
