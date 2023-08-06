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

    public void BuildingEffectManager(Buildings building, Settlements settlement)
    {
        if (settlement != null)
        {
            // Update the City's Food
            settlement.cityFood += building.foodUpkeep;
            settlement.cityFood += building.foodProduction;

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

    public void SettlementEffectManager(Settlements settlement)
    {
        //Calculate City Income
        economyManager.currentGold += (float)(settlement.cityPopulation * 0.1);

        /* Food Calculation
        //Have the people consume some of the food
        settlement.cityFood -= settlement.cityPopulation;

        //Have the people reproduce based on how much food there is (system may not work, will rework later)
        settlement.cityPopulation += (int)(settlement.cityFood * 0.25);
        */
    }
}
