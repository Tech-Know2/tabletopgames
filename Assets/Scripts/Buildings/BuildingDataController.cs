using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDataController : MonoBehaviour
{
    //Access the player script
    public PlayerScript playerScript;
    public CityController cityController;

    //Data Provided By the Building
    public Settlements originalSettlementData;
    public Buildings originalBuildingData;

    //Data Copied to Be Manipulated
    public Settlements clonedSettlementData;
    public Buildings clonedBuildingData;

    //Get the Control Sphere
    public GameObject controlSphere = null;

    public Settlements buildingsSettlement;

    public void GetData()
    {
        Transform playerAndCameraRig = GameObject.Find("Player and Camera Rig")?.transform;

        if (playerAndCameraRig != null)
        {
            playerScript = playerAndCameraRig.GetComponent<PlayerScript>();
        }
        else
        {
            Debug.LogError("Could not find game object named 'Player and Camera Rig'.");
            return;
        }
    }

    public void SettlementSetup()
    {
        GetData();

        print("Settlement Setup In Progress");

        controlSphere.SetActive(true);

        if(originalSettlementData != null)
        {
            clonedSettlementData = Instantiate(originalSettlementData);
        }else 
        {
            return;
        }

        playerScript.Name();
        playerScript.playerSettlementDataList.Add(clonedSettlementData);
        GameObject attachedGameObject = gameObject;
        playerScript.playerSettlementObjectList.Add(gameObject);

        clonedSettlementData.cityName = playerScript.nameGenerator.newName;
        print(playerScript.nameGenerator.newName);

        cityController.SettlementStart();
    }

    public void BuildingSetUp()
    {
        GetData();

        print("Building Setup In Progress");
        
        if(originalBuildingData != null)
        {
            clonedBuildingData = Instantiate(originalBuildingData);
            print("Cloned Building Data");
        }else 
        {
            return;
        }

        //Add the Data to the Building Effect Controller Script
        if(clonedBuildingData.requiresASettlement == true)
        {
            buildingsSettlement.settlementBuildings.Add(clonedBuildingData);
        }
        else 
        {
            playerScript.buildingEffectController.buildingsList.Add(clonedBuildingData);
        }
    }
}
