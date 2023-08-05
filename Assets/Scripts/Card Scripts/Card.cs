using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

[System.Serializable]
public class EffectManagerList
{
    public bool requiresMultipleTurns; //Enter the amount of turns the card effect lasts
    public int turnEffectLength; //Length of the effect
    public int turnEffectCost; //List the amount of change (-10 loyalty, -10 food, etc)
    public string effectCostType; //List types from above
    public string religionName; //Name if the cost is related to religion
    public string governmentName; //Name of the government your effecting war support for
    public Object relgiousIndividual; //Scriptable Object containing the information about the religon
    public int turnsActive = 0; //Dont Mess With This Var. It stores how many turns that this card has been active.
}

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

    //Multiplayer and Auction House Vars
    public string originalPlayer;

    //Name and Location of the City or Region that the Card Effects
    public string cardEffectRegion; //Area of the board where the card effects. For example, the city where a food card is placed will recieve the food from the card

    //Bools for card related logic
    public bool isGovernmentCard;
    public bool assignsGovernment;
    public bool isGovernmentWarSupportBoostCard;
    public bool isAllianceCard;
    public bool isDeclareWarCard;
    public bool isPeaceTreatyCard;
    public bool isPurchasable;
    
    //Bools for card related buildings and board manipulation
    public bool requiresASettlement; //Used to determine weather or not a tile needs to be selected in order for the card to be used
    public bool createsBuildings;
    public bool createsSettlement; //You can only ever create one settlement for each card. Never any more as the code is setup only to handle one, it is also a game feature
    public Settlements settlementScriptableObject;

    //Unit Creation Variables
    public bool createsUnits;
    public bool requiresBarrack;
    public bool requiresWeaponSmith;
    public bool requiresDiplomatTrainingHall;
    public bool requiresTrainingHall;
    public bool requiresMachineWorkshop;

    //Choose the Government if it is a Government Card or Government Related Card
    public string governmentType; // Name of the government created, or the name of the governemnt the war support count will be effecting
    public Government government;

    //Choose the Alliance if it is an Alliance Card
    public string governmentAlliance;

    //Choose the Government to Declare War on if it is that Card Type
    public string governmentWarDecleration;

    //Card Effects / Costs
    /*public int goldPerTurnEffect;
    public int silverPerTurnEffect;
    public int warSupportAgainstGovernmentEffect;
    public int wearinessEffect;
    public int loyaltySabotageEffect;
    public int religionEffect;*/

    [TextArea(12, 40)]
    public string effectDescriptionEditorDisplay =
        "Strings to determine the effects on the game: Don't Alter Won't Change\n" +
        "If the effect only lasts for 1 turn, don't select the Requires Multiple Turns Bool.\n" +
        "If it only lasts for one turn, don't put in anything\n" +
        "\"Gold Cost\" to take gold per turn\n" +
        "\"Loyalty Cost\" to take loyalty per turn\n" +
        "\"Silver Cost\" to take silver per turn\n" +
        "\"Food Cost\" to take food per turn\n" +
        "\"People Cost\" to take people per turn\n" +
        "\"War Support Cost\" to increase War Support per turn against a certain government\n" +
        "\"Religion Cost\" to take away religion from area\n" +
        "\"Weariness Cost\" to effect war weariness\n" +
        "\"Loyalty Cost\" to effect the loyalty of your people" +
        "\"Tech Cost\" to effect your empire's tech points";

    public List<EffectManagerList> effectManagerList = new List<EffectManagerList>();
    
    //Building Based Vars
    public List<GameObject> buildingGameObjects = new List <GameObject>(); //used for both storing building and settlement game objects. Also make sure that the builing that you add always has the BuildingDataController attached to it

    [TextArea(12, 40)]
    public string desierdTileString = 
        "Strings to determine the effects on the game: Don't Alter Won't Change\n" +
        "\"Indicates which tile the card can effect\" \n" +
        "\"Deep Sea\" \n" +
        "\"Shallow Sea\" \n" +
        "\"Sand\" \n" +
        "\"Plains\" \n" +
        "\"Mountains\" \n";
    public List<string> desiredTilesList = new List<string>();

    //Unit Based Vars
    public List<Unit> units = new List<Unit>();
}
