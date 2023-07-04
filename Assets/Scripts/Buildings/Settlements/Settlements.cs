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
    private List<GameObject> settlementReligiousFollowers = new List<GameObject>();
    private List<Buildings> settlementBuildings = new List<Buildings>();
    private List<GameObject> tilesUnderCityControl = new List<GameObject>();
    private List<Unit> settlementUnits = new List<Unit>();
}
