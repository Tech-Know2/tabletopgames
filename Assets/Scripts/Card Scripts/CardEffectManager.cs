using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectManager : MonoBehaviour
{
    //Access and Retrieve the Scriptable Object Data From the Current Card
    public Card originalCard;
    public Card card;
    public Government government;
    public PlayerScript playerScript;
    public EconomyManager economyManager;

    //Manage Weather or not The Cards Are Being Currently in Use, or First Time Use
    private bool firstTime = true;
    private Card currentHighLevelCard;

    //Setup The Varibales to Be Accessed and Assigned By The Scriptable Objects
    private string effectCostType;
    private string cardEffectRegion;
    private int turnEffectLength;
    private int turnEffectCost;
    
    //Government and Political Based Data Sets
    private string governmentName;

    //Current Active Cards
    public List<Card> activeCards = new List<Card>();

    //Filter What Type Of Effect The Card is Going to Have On The Board
    public void EffectFilter()
    {
        if (firstTime == true)
        {
            originalCard = playerScript.cardData;
            card = Instantiate(originalCard);
            currentHighLevelCard = card;
        } else if (firstTime == false)
        {
            card = currentHighLevelCard;
        }

        effectCostType = card.effectCostType;
        turnEffectLength = card.turnEffectLength;
        turnEffectCost = card.turnEffectCost;
        cardEffectRegion = card.cardEffectRegion;

        if(government != null)
        {
            governmentName = government.governmentName;
            
            if(card.isGovernmentWarSupportBoostCard == true || card.isAllianceCard == true || card.isDeclareWarCard == true || card.isPeaceTreatyCard == true)
            {
                //governmentName = card.governemnt.governmentName;

                GovernmentEffectFilter();
            }
        }

        if (card.requiresMultipleTurns == true && firstTime == true)
        {
            activeCards.Add(card);
        }

        if(effectCostType == "Gold Cost")
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
        } else
        {
            print(card + "Does Not Have An Effect, Probably and Error");
        }
    }

    public void GovernmentEffectFilter()
    {
        if(card.isGovernmentWarSupportBoostCard == true)
        {
            GovernmentWarSupportBoost();
        }
        else if(card.isAllianceCard == true == true)
        {
            AllianceCard();
        } 
        else if (card.isPeaceTreatyCard == true)
        {
            PeaceTreatyCard();
        }
        else if (card.isDeclareWarCard == true)
        {
            DeclareWarCard();
        }
    }    

    //Different Types of Economic and Social Effects
    public void GoldCost()
    {
        print("Gold Cost");
        economyManager.currentGold = economyManager.currentGold + card.turnEffectCost;
    }

    public void SilverCost()
    {
        print("Silver Cost");
        economyManager.currentSilver = economyManager.currentSilver + card.turnEffectCost;
    }

    public void LoyaltyCost()
    {
        print("Loyalty Cost");
        economyManager.currentLoyalty = economyManager.currentLoyalty + card.turnEffectCost;
    }

    public void FoodCost()
    {
        print("Food Cost");
    }

    public void PeopleCost()
    {
        print("People Cost");
    }

    public void WarSupportCost()
    {
        print("War Support Cost");
    }

    public void ReligionCost()
    {
        print("Religion Cost");
    }

    public void WearinessCost()
    {
        print("Weariness Cost");
    }

    //Different Types of Political Effects    
    public void GovernmentWarSupportBoost()
    {
        print("Government War Support Boost");
    }

    public void AllianceCard()
    {
        print("Alliance Card");
    }

    public void DeclareWarCard()
    {
        print("Declare War Card");
    }

    public void PeaceTreatyCard()
    {
        print("Peace Treaty Card");
    }

    //Restart the States of the Card Turns in Order to Count Properly
    public void RestartGame()
    {
        // Reset the state of active cards
        foreach (Card card in activeCards)
        {
            card.ResetCardState();
        }
    }

    //Manage Active Card Effects
    public void CurrentlyActiveCards()
    {
        for (int i = activeCards.Count - 1; i >= 0; i--)
        {
            Card currentCard = activeCards[i];

            currentCard.turnsActive++;

            // Apply the card effect
            card = currentCard;
            EffectFilter();

            if (currentCard.turnsActive > currentCard.turnEffectLength)
            {
                activeCards.Remove(currentCard);
            }
        }
    }
}
