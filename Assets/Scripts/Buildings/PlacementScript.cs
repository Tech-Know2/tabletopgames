using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementScript : MonoBehaviour
{
    private GameObject currentBuildCard; // Reference to the current build card
    public List<GameObject> cardBuildings = new List<GameObject>(); // List to store buildings

    private GameObject currentBuilding; // Reference to the current building being placed
    private Buildings buildings; // Reference to the Building component of the current building

    private List<string> desiredTiles = new List<string>(); // List to store desired tiles
}
