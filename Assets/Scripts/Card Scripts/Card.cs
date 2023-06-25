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

    //Name and Location of the City or Region that the Card Effects
    public string cardEffectRegion; //Area of the board where the card effects. For example, the city where a food card is placed will recieve the food from the card

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
    public string governmentType; // Name of the government created, or the name of the governemnt the war support count will be effecting
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
    //"Religion Cost" to take away religion from area
    public bool requiresMultipleTurns; //Enter the amount of turns the card effect lasts
    public int turnEffectLength;
    public bool requiresPeople;
    public int turnEffectCost; //List the amount of change (-10 loyalty, -10 food, etc)
    public string effectCostType; //List types from above
    public string religionName; //Name if the cost is related to religion

    //Card Impacts (Buildings, Units, etc)
    /*public List<Building> buildings = new List<Building>();
    public List<Unit> units = new List<Unit>();*/

    public List<GameObject> buildingObjects = new List <GameObject>();
    public List<Buildings> buildings = new List <Buildings>();
}
