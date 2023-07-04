using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectManager : MonoBehaviour
{
    // Access and Retrieve the Scriptable Object Data From the Current Card
    public Card originalCard;
    public Card card;
    public Government government;
    public PlayerScript playerScript;
    public EconomyManager economyManager;

    // Manage Whether the Cards Are Currently in Use or First Time Use
    public bool firstTime = true;
    private Card currentHighLevelCard;

    // Setup the Variables to be Accessed and Assigned by the Scriptable Objects
    private string effectCostType;
    private string cardEffectRegion;
    private int turnEffectLength;
    private int turnEffectCost;

    // Government and Political Based Data Sets
    private string governmentName;

    // Current Active Cards
    public List<Card> activeCards = new List<Card>();

    // Filter What Type of Effect the Card is Going to Have on the Board
    public void EffectFilter()
    {
        if (firstTime)
        {
            //originalCard = playerScript.cardData;
            card = Instantiate(playerScript.cardData);
        }
        else
        {
            card = currentHighLevelCard;
        }

        effectCostType = card.effectCostType;
        turnEffectLength = card.turnEffectLength;
        turnEffectCost = card.turnEffectCost;
        cardEffectRegion = card.cardEffectRegion;

        if (government != null)
        {
            governmentName = government.governmentName;

            if (card.isGovernmentWarSupportBoostCard || card.isAllianceCard || card.isDeclareWarCard || card.isPeaceTreatyCard)
            {
                GovernmentEffectFilter();
            }
        }

        if (card.requiresMultipleTurns) // Check if the card requires multiple turns
        {
            if (firstTime)
            {
                activeCards.Add(card);
            }
            else if (card.turnsActive > card.turnEffectLength)
            {
                activeCards.Remove(card);
            }
        }

        if (effectCostType == "Gold Cost")
        {
            GoldCost();
        }
        else if (effectCostType == "Silver Cost")
        {
            SilverCost();
        }
        else if (effectCostType == "Loyalty Cost")
        {
            LoyaltyCost();
        }
        else if (effectCostType == "Food Cost")
        {
            FoodCost();
        }
        else if (effectCostType == "People Cost")
        {
            PeopleCost();
        }
        else if (effectCostType == "War Support Cost")
        {
            WarSupportCost();
        }
        else if (effectCostType == "Religion Cost")
        {
            ReligionCost();
        }
        else if (effectCostType == "Weariness Cost")
        {
            WearinessCost();
        }
        else
        {
            Debug.Log(card.name + " does not have an effect. Check for errors.");
        }
    }


    public void GovernmentEffectFilter()
    {
        if (card.isGovernmentWarSupportBoostCard)
        {
            GovernmentWarSupportBoost();
        }
        else if (card.isAllianceCard)
        {
            AllianceCard();
        }
        else if (card.isPeaceTreatyCard)
        {
            PeaceTreatyCard();
        }
        else if (card.isDeclareWarCard)
        {
            DeclareWarCard();
        }
    }

    // Different Types of Economic and Social Effects
    public void GoldCost()
    {
        Debug.Log("Gold Cost");
        economyManager.currentGold += turnEffectCost;
    }

    public void SilverCost()
    {
        Debug.Log("Silver Cost");
        economyManager.currentSilver += turnEffectCost;
    }

    public void LoyaltyCost()
    {
        Debug.Log("Loyalty Cost");
        economyManager.currentLoyalty += turnEffectCost;
    }

    public void FoodCost()
    {
        Debug.Log("Food Cost");
    }

    public void PeopleCost()
    {
        Debug.Log("People Cost");
    }

    public void WarSupportCost()
    {
        Debug.Log("War Support Cost");
    }

    public void ReligionCost()
    {
        Debug.Log("Religion Cost");
    }

    public void WearinessCost()
    {
        Debug.Log("Weariness Cost");
    }

    // Different Types of Political Effects
    public void GovernmentWarSupportBoost()
    {
        Debug.Log("Government War Support Boost");
    }

    public void AllianceCard()
    {
        Debug.Log("Alliance Card");
    }

    public void DeclareWarCard()
    {
        Debug.Log("Declare War Card");
    }

    public void PeaceTreatyCard()
    {
        Debug.Log("Peace Treaty Card");
    }

    // Manage the Currently Active Cards and their Effects
    public void CurrentlyActiveCards()
    {
        List<Card> cardsToRemove = new List<Card>();

        foreach (Card currentCard in activeCards.ToArray()) // Iterate over a copy of activeCards
        {
            currentCard.turnsActive++;

            // Apply the card effect
            card = currentCard;

            // Store the current card as the high-level card
            currentHighLevelCard = card;
            firstTime = false;
            EffectFilter();

            if (currentCard.turnsActive > currentCard.turnEffectLength)
            {
                cardsToRemove.Add(currentCard);
            }
        }

        // Remove the cards outside of the loop
        foreach (Card cardToRemove in cardsToRemove)
        {
            activeCards.Remove(cardToRemove);
        }
    }
}
