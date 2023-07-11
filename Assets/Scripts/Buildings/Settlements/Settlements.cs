using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settlement", menuName = "Settlement")]
public class Settlements : ScriptableObject
{
    public string cityName;
    public int cityLevel;
    public int cityPopulation;
    public float calculatedLoyalty;

    //Create a list for the loyalty of the citizens
    public List<string> acceptableBuildTiles = new List<string>();
    public List<GameObject> settlementReligiousFollowers = new List<GameObject>();
    public List<Buildings> settlementBuildings = new List<Buildings>();
    public List<GameObject> tilesUnderCityControl = new List<GameObject>();
    public List<Unit> settlementUnits = new List<Unit>();
}
