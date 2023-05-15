using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : MonoBehaviour
{
    //Access The Scriptable Objects Necessary
    public Government government;

    //Dealer Related Arrays
    public List<GameObject> actionCards = new List<GameObject>();
    public List<GameObject> eventCards = new List<GameObject>();

    //Dealer Locations and Spot Types
    public List<GameObject> actionCardSlot = new List<GameObject>();
    public List<GameObject> eventCardSlots = new List<GameObject>();

    //Pull the Data from The Government of The Player
    private int agrarianModifier;
    private int militaryModifier;
    private int industrialModifier;
    private int academicAndReligiousModifier;
    private int navalModifier;
    private int transportationAndEconomyModifier;
    private int defenseModifier;
    private int scoutingModifier;
    private int mediaAndSocialModifier;

    public void dealActionCards()
    {
        string govType = government.governmentName;

        if (govType != null)
        {
            //Set the Data About The Government to Variables
            agrarianModifier = government.agrarian;
            militaryModifier = government.military;
            industrialModifier = government.industrial;
            academicAndReligiousModifier = government.academicAndReligious;
            navalModifier = government.naval;
            transportationAndEconomyModifier = government.transportationAndEconomy;
            defenseModifier = government.defense;
            scoutingModifier = government.scouting;
            mediaAndSocialModifier = government.mediaAndSocial;
        }

        if (govType != null)
        {
            int cardCat = Random.Range(0, 901);

            if (cardCat >= 0 && cardCat <= 100 + agrarianModifier) //Agrarian
            {
                //Pick Card From This Category
            }else if (cardCat > 100 + agrarianModifier && cardCat <= 200 + militaryModifier + agrarianModifier) //Military
            {
                //Pick Card From This Category
            }else if (cardCat > 200 + militaryModifier + agrarianModifier && cardCat <= 300 + militaryModifier + agrarianModifier + industrialModifier) //Industrial
            {
                //Pick Card From This Category
            }else if (cardCat > 300 + agrarianModifier + militaryModifier + industrialModifier && cardCat <= 400 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier) //Academic and Religious
            {
                //Pick Card From This Category
            } else if (cardCat > 400 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier && cardCat <= 500 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier + navalModifier) //Naval
            {
                //Pick Card From This Category
            } else if (cardCat > 500 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier  + navalModifier && cardCat <= 600 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier + navalModifier + transportationAndEconomyModifier) //Transportation and Economy
            {
                //Pick Card From This Category
            } else if (cardCat > 600 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier  + navalModifier + transportationAndEconomyModifier && cardCat <= 700 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier + navalModifier + transportationAndEconomyModifier + defenseModifier) //Defense
            {
                //Pick Card From This Category
            } else if (cardCat > 700 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier  + navalModifier + transportationAndEconomyModifier + defenseModifier && cardCat <= 800 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier + navalModifier + transportationAndEconomyModifier + defenseModifier + scoutingModifier) //Scouting
            {
                //Pick Card From This Category
            } else if (cardCat > 800 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier  + navalModifier + transportationAndEconomyModifier + defenseModifier + scoutingModifier && cardCat <= 900 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier + navalModifier + transportationAndEconomyModifier + defenseModifier + scoutingModifier + mediaAndSocialModifier) //Media And Social
            {
                //Pick Card From This Category
            } else {
                //Means that the number chosen did not result in a tech being chosen so throw in code to just pull from a random card category
            }

        } else {
            int cardCat = Random.Range(0, 901);
            
            if (cardCat >= 0 && cardCat <= 100) //Agrarian
            {

            } else if (cardCat > 100 && cardCat <= 200) //Military
            {

            } else if (cardCat > 200 && cardCat <= 300) //Industrial
            {

            } else if (cardCat > 300 && cardCat <= 400) //Academic and Religious
            {

            } else if (cardCat > 400 && cardCat <= 500) //Naval
            {

            } else if (cardCat > 500 && cardCat <= 600) //Transportation and Economy
            {

            } else if (cardCat > 600 && cardCat <= 700) //Defense
            {

            } else if (cardCat > 700 && cardCat <= 800) //Scouting
            {

            } else if (cardCat > 800 && cardCat <= 900) //Media and Social
            {

            } else {
                //Means that the number chosen did not result in a tech being chosen so throw in code to just pull from a random card category
            }
        }
    }

    public void dealEventCards()
    {
        int eventEra = Random.Range(0, 31);

        if(eventEra >= 0 && eventEra <= 10)
        {
            //Era 1
        } else if(eventEra > 10 && eventEra <= 20)
        {
            //Era 2
        } else if(eventEra > 20 && eventEra <= 30)
        {
            //Era 3
        } else {
            //Try again I guess
        }
    }
}