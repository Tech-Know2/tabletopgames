using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EconomyManager : MonoBehaviour
{
    //Effectable Values
    private int currentGold;
    private int currentSilver;
    private int currentWarWeariness;
    private int currentLoyalty; // Default Value is 100

    //Passed Display Values
    public int passedTurnCount;
    public int passedEraCount;

    //War Support Against Nations
    private int currentDemocraticWarSupport;
    private int currentRepublicWarSupport;
    private int currentTheocracyWarSupport;
    private int currentFascistWarSupport;
    private int currentCommunistWarSupport;
    private int currentMonarchyWarSupport;
    private int currentOligarchyWarSupport;

    //UI Display Elements
    public TextMeshProUGUI goldDisplay;
    public TextMeshProUGUI silverDisplay;
    public TextMeshProUGUI warWearinessDisplay;
    public TextMeshProUGUI warSupportDisplay;
    public TextMeshProUGUI loyaltyDisplay;
    public TextMeshProUGUI turnCounterDisplay;
    public TextMeshProUGUI eraCounterDisplay;

    // Start is called before the first frame update
    void Start()
    {
        goldDisplay.text = currentGold.ToString();
        silverDisplay.text = currentSilver.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        turnCounterDisplay.text = passedTurnCount.ToString();
        eraCounterDisplay.text = passedEraCount.ToString();
    }
}
