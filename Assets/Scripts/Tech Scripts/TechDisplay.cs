using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechDisplay : MonoBehaviour
{
    public Tech originalTech;
    private Tech tech;
    public Dealer dealer;
    public PlayerScript playerScript;
    public TechTreeController techTreeController;
    private int updatedtechPointCount;

    public Button currentTechButton;
    public Button previousTechButton;
    private Tech originalPreviousTech;
    private Tech previousTech;
    public EconomyManager economyManager;

    void Start()
    {
        originalTech.ResetTechState();

        currentTechButton = GetComponent<Button>();

        tech = Instantiate(originalTech);

        if (previousTechButton != null)
        {
            previousTech = Instantiate(originalPreviousTech);
        }

        Image buttonImage = currentTechButton.image;
        string techColor = tech.techColor;

        Color newColor;
        if (ColorUtility.TryParseHtmlString(techColor, out newColor))
        {
            buttonImage.color = newColor;
        }
        else
        {
            Debug.LogError("Invalid color code: " + techColor);
        }
    }

    public void techResearch()
    {
        int playerGoldCount = economyManager.currentGold;

        // Get the Current Amount of Tech Points from Player
        updatedtechPointCount = economyManager.currentTechPoints;

        if (previousTechButton != null)
        {
            TechDisplay techDisplay = previousTechButton.GetComponent<TechDisplay>();
            if (techDisplay != null)
            {
                previousTech = techDisplay.tech;

                print(tech.techName + " has already been researched");
            }
        }

        if (playerScript.currentEra >= tech.techEra && updatedtechPointCount >= 1)
        {
            tech.isResearched = true;
            updatedtechPointCount = updatedtechPointCount - 1;

            // Set the Economy Manager tech Points to the New Updated Values
            economyManager.currentTechPoints = updatedtechPointCount;

            if (tech.techCards.Count != 0)
            {
                foreach (Card card in tech.techCards)
                {
                    dealer.actionCardArray.Add(card);
                }
                dealer.filterCards();

                if (tech.techName == "Settlements")
                {
                    techTreeController.isSettlementTechResearched = true;
                }
            }

            print(tech.techName + " Researched");
        }
        else if (previousTech != null && tech.isResearched == false && previousTech.isResearched == true && playerScript.currentEra >= tech.techEra && updatedtechPointCount >= 1)
        {
            tech.isResearched = true;
            updatedtechPointCount = updatedtechPointCount - 1;

            // Set the Economy Manager tech Points to the New Updated Values
            economyManager.currentTechPoints = updatedtechPointCount;

            if (tech.techCards.Count != 0)
            {
                foreach (Card card in tech.techCards)
                {
                    dealer.actionCardArray.Add(card);
                }
                dealer.filterCards();
            }

            print(tech.techName + " Researched");
        }
    }
}
