using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingEffectController : MonoBehaviour
{
    public PlayerScript playerScript;
    public EconomyManager economyManager;

    public List<Buildings> buildingsList = new List<Buildings>();

    public void Start()
    {

    }
    
    public void BuildingsEffects()
    {
        for (int x = 0; x < buildingsList.Count; x++)
        {
            Buildings buildings = buildingsList[x];

            economyManager.currentGold += buildings.goldUpKeep;
            economyManager.currentSilver += buildings.silverUpKeep;
        }
    }

}
