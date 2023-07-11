using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class BuildingSlotDisplay : MonoBehaviour, IPointerClickHandler
{
    //Public Script Connections
    public BuildingPopUp buildingPopUp;

    //UI Displays
    public TextMeshProUGUI nameSlot;
    public TextMeshProUGUI descriptionSlot;

    //Data for the UI
    public Settlements settlementData;
    public Buildings buildingData;

    //Data for the placement
    public GameObject building;

    public void Awake()
    {
        GameObject buildingPopUpObject = GameObject.Find("Building UI Popup Panel");
        buildingPopUp = buildingPopUpObject.GetComponent<BuildingPopUp>();

        Transform childName = transform.Find("Name");
        if (childName != null)
        {
            nameSlot = childName.GetComponent<TextMeshProUGUI>();
        }

        Transform childDescription = transform.Find("Description");
        if (childDescription != null)
        {
            descriptionSlot = childDescription.GetComponent<TextMeshProUGUI>();
        }
    }

    public void setDisplayVariables(string passedBuildType)
    {
        if (passedBuildType == "Settlement")
        {
            nameSlot.text = "Settlement";
            descriptionSlot.text = "Construct a new settlement to further your empire's reach";
        }
        else 
        {
            //Do Something later
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CompareTag("Building Slot"))
        {
            GameObject clickedObject = eventData.pointerPress;

            buildingPopUp.SlotClickFunctionality(clickedObject);
        }
    }
}
