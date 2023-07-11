using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour
{
    // Accessing Data and Scripts
    public BuildingDataController buildingDataController;
    private Settlements settlementData;

    // List of tiles under the city's control
    public List<GameObject> collidedTiles = new List<GameObject>();

    // Information About the City (Obtained and Stored from the Scriptable Object). Don't edit these values, edit the scriptable objects' values
    public string cityName;
    public int cityLevel;
    public int cityPopulation;
    public float calculatedLoyalty;
    public List<GameObject> settlementReligiousFollowers = new List<GameObject>();
    public List<Buildings> settlementBuildings = new List<Buildings>();
    public List<GameObject> tilesUnderCityControl = new List<GameObject>();
    public List<Unit> settlementUnits = new List<Unit>();

    public void SettlementStart()
    {
        
        settlementData = buildingDataController.clonedSettlementData;
        settlementData.cityPopulation += 10;

        //Assign the Variables to be public so they can be seen from the editor for debugging
        cityName = settlementData.cityName;
        cityLevel = settlementData.cityLevel;
        cityPopulation = settlementData.cityPopulation;
        calculatedLoyalty = settlementData.calculatedLoyalty;
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckCollision(other);
    }

    private void OnTriggerStay(Collider other)
    {
        CheckCollision(other);
    }

    private void CheckCollision(Collider collider)
    {
        if (collider.CompareTag("Deep Sea") || collider.CompareTag("Shallow Sea") || collider.CompareTag("Sand") || collider.CompareTag("Plains") || collider.CompareTag("Mountains"))
        {
            if (collider.gameObject == buildingDataController.controlSphere)
            {
                if (!collidedTiles.Contains(collider.gameObject))
                {
                    Debug.Log("Tile Under Control: " + collider.gameObject.name);
                    settlementData.tilesUnderCityControl.Add(collider.gameObject);
                    collidedTiles.Add(collider.gameObject);
                }
            }
        }
    }


    public void CityUpgraded()
    {
        // City Upgrade System
    }
}
