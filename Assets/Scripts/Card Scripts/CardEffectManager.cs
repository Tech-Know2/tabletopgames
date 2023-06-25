using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectManager : MonoBehaviour
{
    //Access and Retrieve the Scriptable Object Data From the Current Card
    public Card card;
    public Government government;

    //Setup The Varibales to Be Accessed and Assigned By The Scriptable Objects
    private string effectCostType;
    private string cardEffectRegion;
    private int effectTurnLength;
    private int turnEffectCost;
    
    //Government and Political Based Data Sets
    private string governmentName;

    //Filter What Type Of Effect The Card is Going to Have On The Board
    public void EffectFilter()
    {
        effectCostType = card.effectCostType;
        effectTurnLength = card.turnEffectLength;
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

    }

    public void SilverCost()
    {
        
    }

    public void LoyaltyCost()
    {
        
    }

    public void FoodCost()
    {
        
    }

    public void PeopleCost()
    {
        
    }

    public void WarSupportCost()
    {
        
    }

    public void ReligionCost()
    {

    }

    //Different Types of Political Effects    
    public void GovernmentWarSupportBoost()
    {

    }

    public void AllianceCard()
    {

    }

    public void DeclareWarCard()
    {

    }

    public void PeaceTreatyCard()
    {

    }
}
