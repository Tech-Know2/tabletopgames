using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementScript : MonoBehaviour
{
    private GameObject currentBuildCard; // Reference to the current build card
    public List<GameObject> cardBuildings = new List<GameObject>(); // List to store buildings

    private bool isPlacing = false; // Flag to track placement mode
    private GameObject currentBuilding; // Reference to the current building being placed
    private Buildings buildings; // Reference to the Building component of the current building

    private List<string> desiredTiles = new List<string>(); // List to store desired tiles

    public void Start()
    {
        // Get the Card component from the current build card
        Card card = currentBuildCard.GetComponent<Card>();

        // Add all buildings from the card to the list
        cardBuildings.AddRange(card.buildingObjects);

        // Assign the Building component of the current building
        buildings = currentBuilding.GetComponent<Buildings>();
    }

    public void Update()
    {
        // Check if the mouse is over the object
        if (IsMouseOverObject())
        {
            // Enable placement mode and set the current building if it's not already enabled
            if (!isPlacing)
            {
                isPlacing = true;
                currentBuilding = cardBuildings[0];
            }

            // Check if the mouse button is clicked and perform placement
            if (isPlacing && Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                desiredTiles.Clear(); // Clear the desired tiles list

                // Determine the desired tile based on the current building
                if (buildings.placedOnDeepSea)
                {
                    desiredTiles.Add("Deep Sea");
                }
                else if (buildings.placedOnShallowSea)
                {
                    desiredTiles.Add("Shallow Sea");
                }
                else if (buildings.placedOnSand)
                {
                    desiredTiles.Add("Sand");
                }
                else if (buildings.placedOnPlains)
                {
                    desiredTiles.Add("Plains");
                }
                else if (buildings.placedOnMountains)
                {
                    desiredTiles.Add("Mountains");
                }
                
                // Check if the raycast hits an object with the desired tag
                if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("plains"))
                {
                    // Place the current building at the hit point
                    Instantiate(currentBuilding, hit.point, Quaternion.identity);
                }
            }
        }
        else
        {
            // Disable placement mode and reset the current building if it's not already disabled
            if (isPlacing)
            {
                isPlacing = false;
                currentBuilding = null;
            }
        }
    }

    private bool IsMouseOverObject()
    {
        // Cast a ray from the camera to detect if it hits the GameObject's collider
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject;

        print("Mouse Over");
    }

    public void buildingReset()
    {
        desiredTiles.Clear();
    }
}
