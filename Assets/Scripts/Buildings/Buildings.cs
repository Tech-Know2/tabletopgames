using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Building")]
public class Buildings : ScriptableObject
{
    //Display Attributes
    public string buildingName;
    public string buildingDescription;
    public Sprite buildingThumbnail;

    //Game Modifier Attribute Variables for Consumption
    public int peopleInitialCost;
    public int goldUpKeep;
    public int silverUpKeep;
    public int peopleUpKeep;
    public int foodUpkeep;
    public float health;
    public int buildTime;

    //Game Modifier Attribute Variables for Production
    public int goldProduction;
    public int silverProduction;
    public int foodProduction;
    public float techPointProduction;
    public float travelStaminaDecreaseMultipler;

    //Variables to Keep Track of Building Constraints and Requierments
    public bool requiresASettlement;
    public List<string> acceptableBuildTiles = new List<string>();

    //Producible Objects from Building
    public List<Object> producableObjects = new List<Object>();

    //Bools to check Building Identity
    public bool isLandTrainingRelated;
    public bool isNavalTrainingRelated;
    public bool isReligiousTrainingRelated;
    public bool isSiegeTrainingRelated;
    public bool isPath;
    public bool isDefense;
    public bool isGate;
    public bool isBridge;
    public bool isMountable;
    public bool isSightCapable;

    public int sightRange;
}
