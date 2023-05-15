using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
public class Unit : ScriptableObject
{
    //Display Information
    public string unitName;
    public string description;
    public string unitCategory;
    public string unitColor;
    public int unitEra;
    public Sprite art;

    //Upkeep Information
    public int silverCostPerTurn;
    public int goldCostPerTurn;
    public int foodCostPerTurn;

    //Unit Stats
    public int stamina;
    public int attack;
    public int defense;
    public bool isRanged;
    public int range;
    public bool isSiege;

    //Unit Backend Information and Requierments
    public GameObject unitTech;
    public GameObject primaryUnitForm;
    public List<GameObject> secondaryUnitForm = new List<GameObject>();
    public List<GameObject> finalUnitForm = new List<GameObject>();
    public List<GameObject> mergeableUnitCombos = new List<GameObject>();
}
