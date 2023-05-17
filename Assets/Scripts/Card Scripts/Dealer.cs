using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : MonoBehaviour
{
    //Access The Scriptable Objects Necessary
    public Government government;
    public GameObject player;
    private string govType;

    //Store Total/All of the Action and Event Cards
    public List<GameObject> actionCardArray = new List<GameObject>();
    public List<GameObject> eventCardArray = new List<GameObject>();

    //Dealer Related Event Card Arrays
    private List<GameObject> eraOneEventCards = new List<GameObject>();
    private List<GameObject> eraTwoEventCards = new List<GameObject>();
    private List<GameObject> eraThreeEventCards = new List<GameObject>();

    //Dealer Related Action Based Arrays Sorted By Category
    //Agrarian Action Card Array
    private List<GameObject> agrarianArray = new List<GameObject>();

    //Military Action Card Array
    private List<GameObject> militaryArray = new List<GameObject>();

    //Industrial Action Card Array
    private List<GameObject> industrialArray = new List<GameObject>();

    //Academic and Religious Action Card Array
    private List<GameObject> academicAndReligiousArray = new List<GameObject>();

    //Naval Action Card Array
    private List<GameObject> navalArray = new List<GameObject>();

    //Transportation and Economy Action Card Array
    private List<GameObject> transportationAndEconomyArray = new List<GameObject>();

    //Defense Action Card Array
    private List<GameObject> defenseArray = new List<GameObject>();

    //Scouting Action Card Array
    private List<GameObject> scoutingArray = new List<GameObject>();

    //Media and Social Action Card Array
    private List<GameObject> mediaAndSocialArray = new List<GameObject>();

    //Dealer Locations and Spot Types
    //First Arrays store the drawn cards
    private List<GameObject> actionCardSlot = new List<GameObject>();
    private List<GameObject> eventCardSlot = new List<GameObject>();
    //Second Array stores the spots where the cards will be displayed
    public List<GameObject> actionCardHolder = new List<GameObject>();
    public List<GameObject> eventCardHolder = new List<GameObject>();

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

    //Data About the Game
    public int currentEra;

    void Start()
    {
        
    }

    public void filterCards()
    {
        //Sort The Cards By Category
        for (int a = 0; a < actionCardArray.Count; a++)
        {
            //Create a Game Object Variable
            //Assign the Variable to the Current Card that Is Being Filtered
            GameObject currentFilteringCard = actionCardArray[a];

            //fetch the Data about the Currently Filtered Card
            Card card = currentFilteringCard.GetComponent<Card>();
            string currentCardCat = card.cardCategory;

            //Sort the cards into arrays that will store only cards in their category
            if(currentCardCat == "Agrarian")
            {
                agrarianArray.Add(currentFilteringCard);
                actionCardArray.Remove(currentFilteringCard);

            } else if(currentCardCat == "Military")
            {
                militaryArray.Add(currentFilteringCard);
                actionCardArray.Remove(currentFilteringCard);
                
            } else if(currentCardCat == "Industrial")
            {
                industrialArray.Add(currentFilteringCard);
                actionCardArray.Remove(currentFilteringCard);
                
            } else if(currentCardCat == "Academic and Religious")
            {
                academicAndReligiousArray.Add(currentFilteringCard);
                actionCardArray.Remove(currentFilteringCard);
                
            } else if(currentCardCat == "Naval")
            {
                navalArray.Add(currentFilteringCard);
            } else if(currentCardCat == "Transportation and Economy")
            {
                transportationAndEconomyArray.Add(currentFilteringCard);
                actionCardArray.Remove(currentFilteringCard);
                
            } else if(currentCardCat == "Defense")
            {
                defenseArray.Add(currentFilteringCard);
                actionCardArray.Remove(currentFilteringCard);
                
            } else if(currentCardCat == "Scouting")
            {
                scoutingArray.Add(currentFilteringCard);
            } else if(currentCardCat == "Media and Social")
            {
                mediaAndSocialArray.Add(currentFilteringCard);
                actionCardArray.Remove(currentFilteringCard);
                
            } else
            {
                print("Card with wrong identifier" + currentFilteringCard);
                
            }
        }
    }

    public void dealActionCards()
    {
        for (int x = 0; x < actionCardHolder.Count + 1; x++)
        {
            //Stores the Current Card
            GameObject currentCard = null;
            
            Government gov = player.GetComponent<Government>();
            govType = gov.governmentName;  

            if (govType != null)
            {
                //Set the Data About The Government to Variables
                agrarianModifier = gov.agrarian;
                militaryModifier = gov.military;
                industrialModifier = gov.industrial;
                academicAndReligiousModifier = gov.academicAndReligious;
                navalModifier = gov.naval;
                transportationAndEconomyModifier = gov.transportationAndEconomy;
                defenseModifier = gov.defense;
                scoutingModifier = gov.scouting;
                mediaAndSocialModifier = gov.mediaAndSocial;

                //Random Number To Decide the Category
                int cardCat = Random.Range(0, 901);

                if (cardCat >= 0 && cardCat <= 100 + agrarianModifier) //Agrarian
                {
                    //Pick a Card From Inside One of the Categorys
                    int cardCount = agrarianArray.Count;
                    int cardSelect = Random.Range(0, cardCount);

                    currentCard = agrarianArray[cardSelect];
                    actionCardSlot.Add(currentCard);

                }else if (cardCat > 100 + agrarianModifier && cardCat <= 200 + militaryModifier + agrarianModifier) //Military
                {
                    //Pick a Card From Inside One of the Categorys
                    int cardCount = militaryArray.Count;
                    int cardSelect = Random.Range(0, cardCount);

                    currentCard = militaryArray[cardSelect];
                    actionCardSlot.Add(currentCard);

                }
                else if (cardCat > 200 + militaryModifier + agrarianModifier && cardCat <= 300 + militaryModifier + agrarianModifier + industrialModifier) //Industrial
                {
                    //Pick a Card From Inside One of the Categorys
                    int cardCount = industrialArray.Count;
                    int cardSelect = Random.Range(0, cardCount);

                    currentCard = industrialArray[cardSelect];
                    actionCardSlot.Add(currentCard);

                }
                else if (cardCat > 300 + agrarianModifier + militaryModifier + industrialModifier && cardCat <= 400 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier) //Academic and Religious
                {
                    //Pick a Card From Inside One of the Categorys
                    int cardCount = academicAndReligiousArray.Count;
                    int cardSelect = Random.Range(0, cardCount);

                    currentCard = academicAndReligiousArray[cardSelect];
                    actionCardSlot.Add(currentCard);

                } 
                else if (cardCat > 400 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier && cardCat <= 500 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier + navalModifier) //Naval
                {
                    //Pick a Card From Inside One of the Categorys
                    int cardCount = navalArray.Count;
                    int cardSelect = Random.Range(0, cardCount);

                    currentCard = navalArray[cardSelect];
                    actionCardSlot.Add(currentCard);

                } 
                else if (cardCat > 500 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier  + navalModifier && cardCat <= 600 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier + navalModifier + transportationAndEconomyModifier) //Transportation and Economy
                {
                    //Pick a Card From Inside One of the Categorys
                    int cardCount = transportationAndEconomyArray.Count;
                    int cardSelect = Random.Range(0, cardCount);

                    currentCard = transportationAndEconomyArray[cardSelect];
                    actionCardSlot.Add(currentCard);

                } 
                else if (cardCat > 600 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier  + navalModifier + transportationAndEconomyModifier && cardCat <= 700 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier + navalModifier + transportationAndEconomyModifier + defenseModifier) //Defense
                {
                    //Pick a Card From Inside One of the Categorys
                    int cardCount = defenseArray.Count;
                    int cardSelect = Random.Range(0, cardCount);

                    currentCard = defenseArray[cardSelect];
                    actionCardSlot.Add(currentCard);

                } 
                else if (cardCat > 700 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier  + navalModifier + transportationAndEconomyModifier + defenseModifier && cardCat <= 800 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier + navalModifier + transportationAndEconomyModifier + defenseModifier + scoutingModifier) //Scouting
                {
                    //Pick a Card From Inside One of the Categorys
                    int cardCount = scoutingArray.Count;
                    int cardSelect = Random.Range(0, cardCount);

                    currentCard = scoutingArray[cardSelect];
                    actionCardSlot.Add(currentCard);

                } 
                else if (cardCat > 800 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier  + navalModifier + transportationAndEconomyModifier + defenseModifier + scoutingModifier && cardCat <= 900 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier + navalModifier + transportationAndEconomyModifier + defenseModifier + scoutingModifier + mediaAndSocialModifier) //Media And Social
                {
                    //Pick a Card From Inside One of the Categorys
                    int cardCount = mediaAndSocialArray.Count;
                    int cardSelect = Random.Range(0, cardCount);

                    currentCard = mediaAndSocialArray[cardSelect];
                    actionCardSlot.Add(currentCard);

                } 
                else {
                    //Means that the number chosen did not result in a tech being chosen so throw in code to just pull from a random card category
                    //Pick a Card From Inside One of the Categorys
                    int cardCount = militaryArray.Count;
                    int cardSelect = Random.Range(0, cardCount);

                    currentCard = militaryArray[cardSelect];
                    actionCardSlot.Add(currentCard);
                }

            } else {
                int cardCat = Random.Range(0, 901);
                
                if (cardCat >= 0 && cardCat <= 100) //Agrarian
                {
                    //Pick a Card From Inside One of the Categorys
                    int cardCount = agrarianArray.Count;
                    int cardSelect = Random.Range(0, cardCount);

                    currentCard = agrarianArray[cardSelect];
                    actionCardSlot.Add(currentCard);

                } 
                else if (cardCat > 100 && cardCat <= 200) //Military
                {
                    //Pick a Card From Inside One of the Categorys
                    int cardCount = militaryArray.Count;
                    int cardSelect = Random.Range(0, cardCount);

                    currentCard = militaryArray[cardSelect];
                    actionCardSlot.Add(currentCard);

                } 
                else if (cardCat > 200 && cardCat <= 300) //Industrial
                {
                    //Pick a Card From Inside One of the Categorys
                    int cardCount = industrialArray.Count;
                    int cardSelect = Random.Range(0, cardCount);

                    currentCard = industrialArray[cardSelect];
                    actionCardSlot.Add(currentCard);

                } 
                else if (cardCat > 300 && cardCat <= 400) //Academic and Religious
                {
                    //Pick a Card From Inside One of the Categorys
                    int cardCount = academicAndReligiousArray.Count;
                    int cardSelect = Random.Range(0, cardCount);

                    currentCard = academicAndReligiousArray[cardSelect];
                    actionCardSlot.Add(currentCard);

                } 
                else if (cardCat > 400 && cardCat <= 500) //Naval
                {
                    //Pick a Card From Inside One of the Categorys
                    int cardCount = navalArray.Count;
                    int cardSelect = Random.Range(0, cardCount);

                    currentCard = navalArray[cardSelect];
                    actionCardSlot.Add(currentCard);

                } 
                else if (cardCat > 500 && cardCat <= 600) //Transportation and Economy
                {
                    //Pick a Card From Inside One of the Categorys
                    int cardCount = transportationAndEconomyArray.Count;
                    int cardSelect = Random.Range(0, cardCount);

                    currentCard = transportationAndEconomyArray[cardSelect];
                    actionCardSlot.Add(currentCard);

                } 
                else if (cardCat > 600 && cardCat <= 700) //Defense
                {
                    //Pick a Card From Inside One of the Categorys
                    int cardCount = defenseArray.Count;
                    int cardSelect = Random.Range(0, cardCount);

                    currentCard = defenseArray[cardSelect];
                    actionCardSlot.Add(currentCard);

                } 
                else if (cardCat > 700 && cardCat <= 800) //Scouting
                {
                    //Pick a Card From Inside One of the Categorys
                    int cardCount = scoutingArray.Count;
                    int cardSelect = Random.Range(0, cardCount);

                    currentCard = scoutingArray[cardSelect];
                    actionCardSlot.Add(currentCard);

                } 
                else if (cardCat > 800 && cardCat <= 900) //Media and Social
                {
                    //Pick a Card From Inside One of the Categorys
                    int cardCount = mediaAndSocialArray.Count;
                    int cardSelect = Random.Range(0, cardCount);

                    currentCard = mediaAndSocialArray[cardSelect];
                    actionCardSlot.Add(currentCard);

                } 
                else 
                {
                    //Means that the number chosen did not result in a tech being chosen so throw in code to just pull from a random card category
                    //Pick a Card From Inside One of the Categorys
                    int cardCount = militaryArray.Count;
                    int cardSelect = Random.Range(0, cardCount);

                    currentCard = militaryArray[cardSelect];
                    actionCardSlot.Add(currentCard);
                }
            }
        }
    }

    public void dealEventCards()
    {
        for (int e = 0; e < eventCardHolder.Count + 1; e++)
        {
            int eventEra = Random.Range(0, 31);

            if (currentEra == 1)
            {

            } else if (currentEra == 2)
            {

            } else if (currentEra == 3)
            {

            }
        }
    }

    public void playActionCards()
    {
        //Assign the cards that where selectd to be placed into the slots for them
        for (int a = 0; a < actionCardHolder.Count + 1; a++)
        {
            Vector3 objectPosition = actionCardHolder[a].transform.position;

            actionCardSlot[a].transform.position = objectPosition;
        }
    }

    public void playEventCards()
    {
        //Assign the cards that where selectd to be placed into the slots for them
        for (int e = 0; e < eventCardHolder.Count + 1; e++)
        {
            Vector3 objectPosition = eventCardHolder[e].transform.position;

            eventCardSlot[e].transform.position = objectPosition;
        }
    }
}