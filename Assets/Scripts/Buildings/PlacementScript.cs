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
    public Settlements settlement;
    public Buildings building;

    //Location and Building Vars
    private GameObject selectedTileLocation;
    private GameObject buildingGameObject;

    public void PlaceBuilding(string buildType, GameObject building)
    {
        if(buildType == "Settlement")
        {
            buildingGameObject = building;
        }else 
        {
            buildingGameObject = building;
        }
    }

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Get the position of the hit point
            Vector3 hitPosition = hit.point;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            //
        }
    }
    
}
