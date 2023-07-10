using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingSlotDisplay : MonoBehaviour
{
    //UI Displays
    public TextMeshProUGUI nameSlot;
    public TextMeshProUGUI descriptionSlot;
    //public RawImage iconSlot;

    //Data for the UI
    public Settlements settlementData;
    public Buildings buildingData;

    public void Awake()
    {
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
            nameSlot.text = settlementData.cityName;
            descriptionSlot.text = "Construct a new settlement to further your empire's reach";
            //iconSlot.texture = settlementData.settlementBuildThumbnail.texture;
        }
        else 
        {
            //Do Something later
        }
    }
}
