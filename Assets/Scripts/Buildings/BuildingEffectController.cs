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

    public void EffectController(Buildings building, Settlements settlement)
    {
        if (settlement != null)
        {
            // Update the City's Food
            settlement.cityFood += building.foodUpkeep;
            settlement.cityFood += building.foodProduction;

            // Update the City's Population
            settlement.cityPopulation += building.peopleUpKeep;

            // Add to the City's Produced Goods
            for (int i = 0; i < building.producableObjects.Count; i++)
            {
                settlement.storedObjects.Add(building.producableObjects[i]);
            }

            foreach (Object obj in building.producableObjects)
            {
                settlement.objectsProducedEachTurn.Add(obj);
            }
        }

        economyManager.currentGold += building.goldUpKeep;
        economyManager.currentSilver += building.silverUpKeep;
        economyManager.currentTechPoints += building.techPointProduction;
    }

}
