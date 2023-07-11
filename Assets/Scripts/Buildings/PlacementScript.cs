using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementScript : MonoBehaviour
{
    // Materials Indicating Placement Capabilities
    public Material goodPlacement;
    public Material badPlacement;
    private Material originalMaterial;
    public Camera mainCamera;

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
                // Check if the object is different from the previously hovered object
                if (hit.transform.gameObject != hoveredObject && acceptableTileTags.Contains(hit.transform.tag))
                {
                    // Store the new hovered object
                    hoveredObject = hit.transform.gameObject;

                    // Update the preview position to follow the mouse
                    Vector3 mousePosition = hit.point;
                    hoveringLocation = hit.transform.position;

                    // Preview placement
                    Preview(buildingInstance.tag);
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
            Renderer renderer = buildingInstance.GetComponent<Renderer>();

            originalMaterial = renderer.material;

            bool solidPlacement = false;

            if (buildingType == "Settlement")
            {
                if (settlementData != null)
                {
                    if (settlementData.acceptableBuildTiles.Contains(hoveredObject.tag))
                    {
                        if (renderer != null)
                        {
                            renderer.material = goodPlacement;
                            solidPlacement = true;
                        }
                    }
                    else
                    {
                        if (renderer != null)
                        {
                            renderer.material = badPlacement;
                            solidPlacement = false;
                        }
                    }
                }
            }
            else
            {
                if (buildingData != null)
                {
                    if (buildingData.acceptableBuildTiles.Contains(hoveredObject.tag))
                    {
                        if (renderer != null)
                        {
                            renderer.material = goodPlacement;
                            solidPlacement = true;
                        }
                    }
                    else
                    {
                        if (renderer != null)
                        {
                            renderer.material = badPlacement;
                            solidPlacement = false;
                        }
                    }
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (solidPlacement == true)
                {
                    // Place the building
                    GameObject placedBuilding = Instantiate(buildingInstance, hoveringLocation, Quaternion.identity);

                    //Set the material back to the original
                    Renderer rend = placedBuilding.GetComponent<Renderer>();
                    rend.material = originalMaterial;

                    //Assign all of the data to the newly created building
                    BuildingDataController buildingDataController= placedBuilding.GetComponent<BuildingDataController>();
                    if(buildingType == "Settlement")
                    {
                        buildingDataController.originalSettlementData = settlementData;
                        buildingDataController.SettlementSetup();
                    }else 
                    {
                        buildingDataController.originalBuildingData = buildingData;
                        buildingDataController.BuildingSetUp();
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