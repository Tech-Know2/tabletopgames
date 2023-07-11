using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlacementScript : MonoBehaviour
{
    // Materials Indicating Placement Capabilities
    private Material originalMaterial;
    public Camera mainCamera;

    //Access Scripts
    public PlayerScript playerScript;

    // Access the Information About the Building
    public BuildingPopUp buildingPopUp;
    public Settlements settlementData;
    public Buildings buildingData;

    // Location and Building Vars
    private GameObject buildingInstance;
    public List<string> acceptableTileTags;
    private GameObject hoveredObject;
    private bool isPlacing = false;
    private Vector3 hoveringLocation;

    private string buildingType;

    public void PlaceBuilding(string buildType, GameObject building, BuildingSlotDisplay buildingSlotDisplay)
    {
        if (buildType == "Settlement")
        {
            settlementData = buildingSlotDisplay.settlementData;
            buildingInstance = Instantiate(building);
            buildingType = buildType;
            isPlacing = true;
        }
        else
        {
            buildingData = buildingSlotDisplay.buildingData;
            buildingInstance = Instantiate(building);
            buildingType = buildType;
            isPlacing = true;
        }
    }

    private void Update()
    {
        if (isPlacing)
        {
            // Cast a ray from the mouse position into the scene
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits any objects
            if (Physics.Raycast(ray, out hit))
            {
                if (acceptableTileTags.Contains(hit.transform.tag))//hit.transform.position != hoveringLocation && 
                {
                    // Store the new hovered object
                    hoveredObject = hit.transform.gameObject;

                    // Update the preview position to follow the mouse
                    Vector3 mousePosition = hit.point;
                    hoveringLocation = mousePosition;
                }
            }
            else
            {
                // No object is being hovered
                hoveredObject = null;
            }

            Preview(buildingType);
        }
    }


    private void Preview(string buildingType)
    {
        if(isPlacing)
        {
            bool solidPlacement = false;

            if (buildingType == "Settlement")
            {
                if (settlementData != null)
                {
                    if (settlementData.acceptableBuildTiles.Contains(hoveredObject.tag))
                    {
                        solidPlacement = true;

                        buildingInstance.transform.position = hoveringLocation;
                    }
                    else
                    {
                        solidPlacement = false;
                    }
                }
            }
            else
            {
                if (buildingData != null)
                {
                    if (buildingData.acceptableBuildTiles.Contains(hoveredObject.tag))
                    {
                        if (playerScript.playerSettlementDataList.Any(settlement => settlement.tilesUnderCityControl.Contains(hoveredObject)) && buildingData.requiresASettlement == true)
                        {
                            solidPlacement = true;

                            buildingInstance.transform.position = hoveringLocation;

                        }else if(buildingData.acceptableBuildTiles.Contains(hoveredObject.tag) && buildingData.requiresASettlement == false)
                        {
                            solidPlacement = true;

                            buildingInstance.transform.position = hoveringLocation;
                        }
                        else
                        {
                            solidPlacement = false;
                            Debug.Log("Tile not under a city's control");
                        }
                    }
                    else
                    {
                        solidPlacement = false;
                    }
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (solidPlacement == true)
                {
                    //Delete the buildingInstance so that there is only one buildinh
                    Destroy(buildingInstance);

                    // Place the building
                    GameObject placedBuilding = Instantiate(buildingInstance, hoveringLocation, Quaternion.identity);

                    //Assign all of the data to the newly created building
                    BuildingDataController buildingDataController= placedBuilding.GetComponent<BuildingDataController>();
                    if(buildingType == "Settlement")
                    {
                        buildingDataController.originalSettlementData = settlementData;
                        buildingDataController.SettlementSetup();
                    }else 
                    {
                        if(playerScript.playerSettlementDataList.Any(settlement => settlement.tilesUnderCityControl.Contains(hoveredObject)))
                        {
                            buildingDataController.originalBuildingData = buildingData;
                            buildingDataController.BuildingSetUp();
                        } else 
                        {
                            print("Tile not under a city's control");
                        }
                        
                    }
                    
                    // Reset placement variables
                    buildingInstance.transform.position = hoveringLocation;
                    isPlacing = false;

                    //Hide the Pop-up and show the Cards
                    buildingPopUp.closeBuildingDisplay();
                }
            }
        }
    }
}