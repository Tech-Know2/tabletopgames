using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EconomyManager : MonoBehaviour
{
    //Effectable Values
    public int currentGold;
    public int currentSilver;
    public float currentTechPoints;
    public int currentWarWeariness;
    public int currentLoyalty; // Default Value is 100

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
    public TextMeshProUGUI techPointsCounterDisplay;

    // Start is called before the first frame update
    void Start()
    {
        goldDisplay.text = currentGold.ToString();
        silverDisplay.text = currentSilver.ToString();
        techPointsCounterDisplay.text = currentTechPoints.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        goldDisplay.text = currentGold.ToString();
        silverDisplay.text = currentSilver.ToString();
        turnCounterDisplay.text = passedTurnCount.ToString();
        eraCounterDisplay.text = passedEraCount.ToString();
        techPointsCounterDisplay.text = currentTechPoints.ToString();
    }
}
