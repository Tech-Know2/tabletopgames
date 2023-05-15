using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EconomyManager : MonoBehaviour
{
    //Effectable Values
    public string currentGold;
    public string currentSilver;
    public string currentWarWeariness;
    public string currentLoyalty; // Default Value is 100

    //War Support Against Nations
    public string currentDemocraticWarSupport;
    public string currentRepublicWarSupport;
    public string currentTheocracyWarSupport;
    public string currentFascistWarSupport;
    public string currentCommunistWarSupport;
    public string currentMonarchyWarSupport;
    public string currentOligarchyWarSupport;

    //UI Display Elements
    public TextMeshProUGUI goldDisplay;
    public TextMeshProUGUI silverDisplay;
    public TextMeshProUGUI warWearinessDisplay;
    public TextMeshProUGUI warSupportDisplay;
    public TextMeshProUGUI loyaltyDisplay;

    // Start is called before the first frame update
    void Start()
    {
        goldDisplay.text = currentGold;
        silverDisplay.text = currentSilver;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
