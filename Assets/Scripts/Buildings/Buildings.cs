using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Building")]
public class Buildings : ScriptableObject
{
    //Display Attributes
    public string buildingName;
    public string buildingDescription;
    public GameObject requiredTech;

    //Game Modifier Attribute Variables for Consumption
    public int goldUpKeep;
    public int silverUpKeep;
    public int peopleUpKeep;
    public float health;
    public int buildTime;

    //Tiles it Can Be Placed On
    public bool placedOnDeepSea;
    public bool placedOnShallowSea;
    public bool placedOnSand;
    public bool placedOnPlains;
    public bool placedOnMountains;


    //Game Modifier Attribute Variables for Production
    public int goldProduction;
    public int silverProduction;
    public float techPointProduction;
    public float travelStaminaDecreaseMultipler;

    //Variables to Keep Track of Building Constraints and Requierments
    public bool requiresAnotherBuilding;
    public List<GameObject> localBuildingRequirements = new List<GameObject>();

    //Producible Objects from Building
    public List<GameObject> produceableObjects = new List<GameObject>();

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
}
