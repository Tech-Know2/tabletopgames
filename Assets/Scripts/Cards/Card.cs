using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    //Card Display Variables
    public string cardName;
    public string description;
    public string cardType;
    public Sprite art;
    
    //Card Effects / Costs
    public int goldCost;
    public int silverCost;
    public int empireLoyalty;
    public int weariness;
    public int loyaltySabotage;
    public int religion;

    //Card Impacts (Buildings, Units, etc)
    public List<GameObject> buildings = new List<GameObject>();
    public List<GameObject> units = new List<GameObject>();
    public List<GameObject> governmentTypes = new List<GameObject>();
    public List<GameObject> treaties = new List<GameObject>();
    public List<GameObject> alliances = new List<GameObject>();
    public List<GameObject> governmentTypeWarSupport = new List<GameObject>();
}
