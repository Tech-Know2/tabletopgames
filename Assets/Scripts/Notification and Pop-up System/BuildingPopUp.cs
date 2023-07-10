using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPopUp : MonoBehaviour
{
    //Connecting to Other Scripts
    private BuildingDataController buildingDataController;
    public CardEffectManager cardEffectManager;

    //Enabling and Disabling the Pop-ups
    public GameObject buildingPopUp;
    public bool isBuildingPopUpActive = false;
    public List<GameObject> cardSlots = new List<GameObject>();
    public bool isCardsShowing = true;

    //Prefab for Dispays
    public GameObject buildingSlotPrefab;
    public List<GameObject> buildingSlots = new List<GameObject>(); //Max buildings per card is 4

    //Data for the Buildings
    public List<Buildings> buildingsData = new List<Buildings>();
    public Settlements settlementData;

    public void Start()
    {
        isBuildingPopUpActive = false;
        buildingPopUp.SetActive(isBuildingPopUpActive);
    }

    public void BuildingDisplay(string passedBuildType)
    {
        if (!isBuildingPopUpActive)
        {
            isBuildingPopUpActive = true;
            isCardsShowing = false;

            buildingPopUp.SetActive(true);

            foreach (GameObject card in cardSlots)
            {
                card.SetActive(isCardsShowing);
            }

            Debug.Log("Popup Active: " + isBuildingPopUpActive);
            PropogateBuildingDisplays(passedBuildType);
        }
        else
        {
            isBuildingPopUpActive = false;
            isCardsShowing = true;

            foreach (GameObject card in cardSlots)
            {
                card.SetActive(isCardsShowing);
            }

            buildingPopUp.SetActive(false);
            ClearBuildingDisplays();
        }
    }


    public void PropogateBuildingDisplays(string passedBuildType)
    {
        if (passedBuildType == "Settlement")
        {
            GameObject newBuildingSlot = Instantiate(buildingSlotPrefab);
            newBuildingSlot.transform.SetParent(buildingSlots[0].transform, false);
            newBuildingSlot.transform.position = buildingSlots[0].transform.position;

            BuildingSlotDisplay buildingSlotDisplay = newBuildingSlot.GetComponent<BuildingSlotDisplay>();
            buildingSlotDisplay.settlementData = settlementData;
            buildingSlotDisplay.setDisplayVariables("Settlement");
            
        }
        else
        {
            for (int i = 0; i < buildingsData.Count; i++)
            {
                GameObject newBuildingSlot = Instantiate(buildingSlotPrefab);
                newBuildingSlot.transform.SetParent(buildingSlots[i].transform, false);
                newBuildingSlot.transform.position = buildingSlots[i].transform.position;
            }
        }
    }

    public void SetSettlementData(Settlements data)
    {
        settlementData = data;
    }

    public void ClearBuildingDisplays()
    {
        buildingsData.Clear();
    }
}
