using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardEffectManager : MonoBehaviour
{
    //Access and Retrieve the Scriptable Object Data From the Current Card
    public Card originalCard;
    public Card card;
    private Government government;
    public PlayerScript playerScript;
    public EconomyManager economyManager;
    public BuildingPopUp buildingPopUp;

    //Building and Creation Card Variables
    public GameObject selectedTileLocation;

    //Manage Weather or not The Cards Are Being Currently in Use, or First Time Use
    public bool firstTime = true;
    private Card currentHighLevelCard;

    //Setup The Varibales to Be Accessed and Assigned By The Scriptable Objects
    private string effectCostType;
    private string cardEffectRegion;
    private int turnEffectLength;
    private int turnEffectCost;
    
    //Government and Political Based Data Sets
    private string governmentName;
    private bool assignsGovernment = false;

    //Current Active Cards
    public List<Card> activeCards = new List<Card>();

    //Filter What Type Of Effect The Card is Going to Have On The Board
    public void EffectFilter()
    {
        if (firstTime == true)
        {
            originalCard = playerScript.cardData;
            card = Instantiate(originalCard);
        } else if (firstTime == false)
        {
            card = currentHighLevelCard;
        }

        if(selectedTileLocation != null)
        {
            print("Tile Selected");
        }

        if(card.isGovernmentCard == true)
        {
            government = card.government;
            governmentName = government.governmentName;
            assignsGovernment = card.assignsGovernment;
                
            if(card.isGovernmentWarSupportBoostCard == true || card.isAllianceCard == true || card.isDeclareWarCard == true || card.isPeaceTreatyCard == true || card.assignsGovernment == true)
            {
                //governmentName = card.governemnt.governmentName;

                GovernmentEffectFilter();
            }
        }

        //Check to see if the card creates buildings or settlements
        if (card.createsBuildings == true || card.createsSettlement == true)
        {
            ConstructBuilding();
        }

        //Check to see if the card creates units
        if(card.createsUnits == true)
        {
            CreateUnits();
        }

        for(int i = 0; i < card.effectManagerList.Count; i++)
        {
            //Check to see if the card Requires multiple Turns
            if (card.effectManagerList[i].requiresMultipleTurns == true && firstTime == true)
            {
                activeCards.Add(card);
            }
            
            effectCostType = card.effectManagerList[i].effectCostType;
            turnEffectLength = card.effectManagerList[i].turnEffectLength;
            turnEffectCost = card.effectManagerList[i].turnEffectCost;
            cardEffectRegion = card.effectManagerList[i].religionName;

            if(effectCostType == "Gold Cost")
            {
                GoldCost(i);
            }
            else if (effectCostType == "Silver Cost")
            {
                SilverCost(i);
            }
            else if (effectCostType == "Loyalty Cost")
            {
                LoyaltyCost(i);
            }
            else if (effectCostType == "Food Cost")
            {
                FoodCost(i);
            }
            else if (effectCostType == "People Cost")
            {
                PeopleCost(i);
            }
            else if (effectCostType == "War Support Cost")
            {
                WarSupportCost(i);
            }
            else if (effectCostType == "Religion Cost")
            {
                ReligionCost(i);
            }
            else if (effectCostType == "Weariness Cost")
            {
                WearinessCost(i);
            } else
            {
                if(government != null)
                {
                    print(card + "Does Not Have An Effect, Probably and Error");
                }
            }
        }
    }

    //Government Related Effects
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
        else if (card.assignsGovernment == true)
        {
            AssignGovernment();
        }
    }    

    //Different Types of Economic and Social Effects
    public void GoldCost(int i)
    {
        print("Gold Cost");
        economyManager.currentGold = economyManager.currentGold + card.effectManagerList[i].turnEffectCost;
    }

    public void SilverCost(int i)
    {
        print("Silver Cost");
        economyManager.currentSilver = economyManager.currentSilver + card.effectManagerList[i].turnEffectCost;
    }

    public void LoyaltyCost(int i)
    {
        print("Loyalty Cost");
        //economyManager.currentLoyalty = economyManager.currentLoyalty + card.turnEffectCost;
    }

    public void FoodCost(int i)
    {
        print("Food Cost");
        if(card.requiresASettlement == true)
        {
            //keep Count of the Tiles Being checked
            int acceptableTileCount = 0;
            List<string> acceptableTileString = new List<string>();

            Settlements buildingsSettlement = playerScript.playerSettlementDataList.Find(settlement => settlement.tilesUnderCityControl.Contains(selectedTileLocation));
            
            for (int x = 0; x < buildingsSettlement.tilesUnderCityControl.Count; x++)
            {
                GameObject tile =  buildingsSettlement.tilesUnderCityControl[x];
                if(acceptableTileString.Contains(tile.tag))
                {
                    acceptableTileCount += 1;
                }
            }

            buildingsSettlement.cityFood += card.effectManagerList[i].turnEffectCost;
        }
    }

    public void PeopleCost(int i)
    {
        print("People Cost");
    }

    public void WarSupportCost(int i)
    {
        print("War Support Cost");
    }

    public void ReligionCost(int i)
    {
        print("Religion Cost");
    }

    public void WearinessCost(int i)
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

    public void AssignGovernment()
    {
        print("Assigned Government" + governmentName);
        playerScript.government = government;
    }

    //Placement and Creation Based Card Effects
    public void ConstructBuilding()
    {
        Settlements settlementData;
        Buildings buildingData;

        if(card.createsSettlement == true)
        {
            //Settlement Placement logic
            if (card.settlementScriptableObject != null)
            {
                settlementData = card.settlementScriptableObject;

                buildingPopUp.SetSettlementData(settlementData);
                buildingPopUp.BuildingDisplay("Settlement");
                buildingPopUp.settlementData = settlementData;

                print("Also Called");
            }
        }

        if(card.createsBuildings == true)
        {
            // Building Placement logic
            if (card.buildingGameObjects != null)
            {
                for (int i = 0; i < card.buildingGameObjects.Count; i++)
                {
                    GameObject building = card.buildingGameObjects[i];
                    BuildingDataController buildingDataController = building.GetComponent<BuildingDataController>();
                    buildingData = buildingDataController.originalBuildingData;
                    buildingPopUp.buildingsData.Add(buildingData);
                }
                buildingPopUp.BuildingDisplay("Building");
            }
        }
    }

    public void CreateUnits()
    {
        //Create Units
    }

    //Manage the Currently Active Cards and their Effects
    public void CurrentlyActiveCards()
    {
        List<Card> cardsToRemove = new List<Card>();

        foreach (Card currentCard in activeCards.ToArray()) // Iterate over a copy of activeCards
        {
            // Apply the card effect
            card = currentCard;

            // Store the current card as the high-level card
            currentHighLevelCard = card;
            firstTime = false;
            EffectFilter();

            //Go through the effects and check if they have been running for the appropiate amount of time
            for (int i = 0; i < card.effectManagerList.Count; i++)
            {
                currentCard.effectManagerList[i].turnsActive++;

                if (currentCard.effectManagerList[i].turnsActive >= currentCard.effectManagerList[i].turnEffectLength)
                {
                    currentCard.effectManagerList.RemoveAt(i);
                    i--; // Adjust the index after removing an element
                }

                if (currentCard.effectManagerList.Count <= 0)
                {
                    cardsToRemove.Add(currentCard);
                    break; // No need to continue iterating if all effects have been removed
                }
            }
        }

        // Remove the cards outside of the loop
        foreach (Card cardToRemove in cardsToRemove)
        {
            activeCards.Remove(cardToRemove);
        }
    }
}