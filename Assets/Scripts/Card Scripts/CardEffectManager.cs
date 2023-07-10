using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        effectCostType = card.effectCostType;
        turnEffectLength = card.turnEffectLength;
        turnEffectCost = card.turnEffectCost;
        cardEffectRegion = card.cardEffectRegion;

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

        //Check to see if the card Requires multiple Turns
        if (card.requiresMultipleTurns == true && firstTime == true)
        {
            activeCards.Add(card);
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
            if(government != null)
            {
                print(card + "Does Not Have An Effect, Probably and Error");
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

    public void AssignGovernment()
    {
        print("Assigned Government" + governmentName);
        playerScript.government = government;
    }

    //Placement and Creation Based Card Effects
    public void ConstructBuilding() //Temporary Setup (Can only have one building per tile at this point)
    {
        Settlements settlementData = null;
        List<Buildings> buildingData = null;

        //Settlement Placement logic
        if (card.settlementScriptableObject != null)
        {
            settlementData = card.settlementScriptableObject;

            buildingPopUp.SetSettlementData(settlementData);
            buildingPopUp.BuildingDisplay("Settlement");
            buildingPopUp.settlementData = settlementData;
        }
        else 
        {
            return;
        }

        //Building Placement logic
        if (card.buildingScriptableObject != null)
        {
            buildingData = card.buildingScriptableObject;

            buildingPopUp.BuildingDisplay("Building");
            foreach (Buildings building in buildingData)
            {
                buildingPopUp.buildingsData.Add(building);
            }
        }
        else 
        {
            return;
        }

        /*
        //Card Creates Settlements
        if (card.createsSettlement)
        {
            Vector3 tilePosition = selectedTileLocation.transform.position;

            if (card.buildingGameObjects.Count > 0)
            {
                if(settlementData.acceptableBuildTiles.Contains(selectedTileLocation.tag))
                {
                    GameObject settlementPrefab = card.buildingGameObjects[0];
                    GameObject settlement = Instantiate(settlementPrefab, tilePosition, Quaternion.identity);
                    BuildingDataController buildingController = settlement.GetComponent<BuildingDataController>();

                    buildingController.SettlementSetup();

                    //Add the data to the Players Empire Data Collector List
                    playerScript.playerSettlementDataList.Add(buildingController.clonedSettlementData);
                    playerScript.playerSettlementObjectList.Add(settlement);

                } else //Building on the Wrong Tile
                {
                    print("Wrong tile");
                }
            }
            else
            {
                Debug.LogError("No settlement prefab defined for the card: " + card.name);
            }
        }
        else //Card Creates Every other build type
        {
            Vector3 tilePosition = selectedTileLocation.transform.position;

            if (card.buildingGameObjects.Count > 0)
            {
                for (int x = 0; x < buildingData.Count; x++)
                {
                    if(buildingData[x].acceptableBuildTiles.Contains(selectedTileLocation.tag))
                    {
                        GameObject buildingPrefab = card.buildingGameObjects[0];
                        GameObject building = Instantiate(buildingPrefab, tilePosition, Quaternion.identity);
                        BuildingDataController buildingController = building.GetComponent<BuildingDataController>();

                        buildingController.BuildingSetUp();

                        //Add the data to the Players Empire Data Collector List
                        playerScript.playerBuildingsDataList.Add(buildingController.clonedBuildingData);
                        playerScript.playerBuildingObjectList.Add(building);


                    } else //Building on the Wrong Tile
                    {
                        print("Wrong tile");
                    }
                }
            }
            else
            {
                Debug.LogError("No building prefab defined for the card: " + card.name);
            }
        }*/
    }

    public void CreateUnits()
    {

    }

    //Manage the Currently Active Cards and their Effects
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

            if (currentCard.turnsActive + 1 > currentCard.turnEffectLength)
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