using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool isGovernmentCard;
    public bool isAllianceCard;
    public bool isDeclareWarCard;
    public bool isPeaceTreatyCard;

    //Choose the Government if it is a Government Card
    public string governmentType;

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
