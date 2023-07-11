using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementScript : MonoBehaviour
{
    //Materials Indicating Placement Capabilities
    public Material goodPlacement;
    public Material badPlacement;
    public Camera mainCamera;

    //Access the Information About the Building
    public BuildingPopUp buildingPopUp;
    public Settlements settlementData;
    public Buildings buildingData;

    //Location and Building Vars
    private GameObject selectedTileLocation;
    private GameObject buildingGameObject;
    public List<string> acceptableTileTags;

    public void PlaceBuilding(string buildType, GameObject building, Settlements passedSettlementData, Buildings passedBuildingData)
{
    if (buildType == "Settlement")
    {
        buildingGameObject = building;
        settlementData = passedSettlementData;
    }
    else
    {
        buildingGameObject = building;
        buildingData = passedBuildingData;
    }
}


    private GameObject currentlyHoveredTile;

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the object hit by the raycast is a tile with an acceptable tag
            GameObject tile = hit.collider.gameObject;
            if (IsTileTagAcceptable(tile.tag))
            {
                // Store the currently hovered tile
                currentlyHoveredTile = tile;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (currentlyHoveredTile != null)
            {
                Debug.Log("Tile clicked: " + currentlyHoveredTile.name);
            }
        }
    }

    // Check if the given tag is in the list of acceptable tile tags
    private bool IsTileTagAcceptable(string tag)
    {
        return acceptableTileTags.Contains(tag);
    }
}
