using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class GovernmentEffectManager
{
    public string governmentName; //Name of the Government
    public int currentGovernmentWarSupport = 50; //Initial War Support Value
}

public class EconomyManager : MonoBehaviour
{
    //Effectable Values
    public float currentGold;
    public int currentSilver;
    public float currentTechPoints;
    public int currentWarWeariness;
    public int currentLoyalty; // Default Value is 100

    //Passed Display Values
    public int passedTurnCount;
    public int passedEraCount;

    //War Support Against Nations
    public List<GovernmentEffectManager> governmentEffectManagerList = new List<GovernmentEffectManager>();

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