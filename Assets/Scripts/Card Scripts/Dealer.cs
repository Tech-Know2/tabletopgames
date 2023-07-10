using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dealer : MonoBehaviour
{
    //Access The Scriptable Objects Necessary
    private Government government;
    public GameObject player;
    public AuctionHouse auctionHouse;
    public CardEffectManager cardEffectManager;
    private string govType;

    //Card Slot Related Information
    public GameObject cardPrefab;
    private TextMeshProUGUI nameSlot;
    private TextMeshProUGUI cardDescriptionSlot;
    private TextMeshProUGUI cardEraSlot;
    private TextMeshProUGUI cardCategorySlot;

    //Assign The Card Scriptable Object to the Card Prefab Using the Card Data Holder Script. This can then be used to get info from it when it is clicked
    public CardDataHolder cardDataHolder;

    //Store Total/All of the Action and Event Cards
    public List<Card> actionCardArray = new List<Card>();
    public List<Card> eventCardArray = new List<Card>();

    //Dealer Related Event Card Arrays
    private List<Card> eraOneEventCards = new List<Card>();
    private List<Card> eraTwoEventCards = new List<Card>();
    private List<Card> eraThreeEventCards = new List<Card>();

    //Dealer Related Action Based Arrays Sorted By Category
    //Settlement Action Card Array
    private List<Card> settlementArray = new List<Card>();
    
    //Agrarian Action Card Array
    private List<Card> agrarianArray = new List<Card>();

    //Military Action Card Array
    private List<Card> militaryArray = new List<Card>();

    //Industrial Action Card Array
    private List<Card> industrialArray = new List<Card>();

    //Academic and Religious Action Card Array
    private List<Card> academicAndReligiousArray = new List<Card>();

    //Naval Action Card Array
    private List<Card> navalArray = new List<Card>();

    //Transportation and Economy Action Card Array
    private List<Card> transportationAndEconomyArray = new List<Card>();

    //Defense Action Card Array
    private List<Card> defenseArray = new List<Card>();

    //Scouting Action Card Array
    private List<Card> scoutingArray = new List<Card>();

    //Media and Social Action Card Array
    private List<Card> mediaAndSocialArray = new List<Card>();

    //Dealer Locations and Spot Types
    //First Arrays store the drawn cards
    private List<Card> actionCard = new List<Card>();
    private List<Card> eventCardSlot = new List<Card>();

    //Second Array stores the spots where the cards will be displayed
    public List<GameObject> actionCardHolder = new List<GameObject>();
    public List<GameObject> eventCardHolder = new List<GameObject>();

    //Pull the Data from The Government of The Player
    private int settlementModifier;
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

    //Government Check. If this bool is true it means that you have already had a government assigned, and now it is preventing you from getting any more government cards
    private bool governmentAlreadyAssigned = false;

    void Start()
    {
        
    }

    public void nextTurn()
    {
        print("Next Turn Pressed");

        PlayerScript playerScript = player.GetComponent<PlayerScript>();
        currentEra = playerScript.currentEra;
        playerScript.cardsPlayed = 0;

        // Access the government scriptable object from the player script
        
        if (playerScript.government != null)
        {
            Government gov = playerScript.government;
            govType = gov.governmentName;

            governmentAlreadyAssigned = true;
        }
        else
        {
            Debug.Log("Government scriptable object is not attached to the player.");
        }

        playerScript.currentTurn += 1;

        // Call the cards to be drawn by the dealer script
        // Filter The Cards From the Newly Researched Techs
        filterCards();
        // Discard All Unused Cards to the Auction House
        discardCards();
        // Pick the Cards To Be Dealt From the Deck
        // Take Into Account the Current Card Effects
        cardEffectManager.CurrentlyActiveCards();
        playerScript.TurnEffects();
        
        //Check to make sure there are cards to deal or it crashes and breaks everything
        if(agrarianArray.Count != 0 || militaryArray.Count != 0 || settlementArray.Count != 0 || industrialArray.Count != 0 || transportationAndEconomyArray.Count != 0 || academicAndReligiousArray.Count != 0 || defenseArray.Count != 0 || mediaAndSocialArray.Count != 0 || navalArray.Count != 0 || scoutingArray.Count != 0)
        {
            dealActionCards();
            //dealEventCards();
        } else
        {
            return;
        }
        // Display the Cards in the Appropriate Slots
        playActionCards();
        //playEventCards();

        print("Next Turn Function Ran Through Here");
    }

    public void discardCards()
    {
        //auctionHouse.auctionHouseCards.AddRange(actionCard);

        //Temp Way to Clear the lists of the cards, will be changed later, and have the cards sent to the auction house
        actionCard.Clear();
        eventCardSlot.Clear();

        //Remove the Game Object Cards that where just created
        for (int x = 0; x < actionCardHolder.Count; x++)
        {
            int childCount = actionCardHolder[x].transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                GameObject childObject = actionCardHolder[x].transform.GetChild(i).gameObject;
                Destroy(childObject);
            }
        }
    }

    public void filterCards()
    {
        List<Card> cardsToRemove = new List<Card>();

        for (int a = 0; a < actionCardArray.Count; a++)
        {
            Card card = actionCardArray[a];
            string currentCardCat = card.cardCategory;
            
            if (currentCardCat == "Settlement")
            {
                settlementArray.Add(card);
                cardsToRemove.Add(card);
            }else if (currentCardCat == "Agrarian")
            {
                agrarianArray.Add(card);
                cardsToRemove.Add(card);
            }
            else if (currentCardCat == "Military")
            {
                militaryArray.Add(card);
                cardsToRemove.Add(card);
            }
            else if (currentCardCat == "Industrial")
            {
                industrialArray.Add(card);
                cardsToRemove.Add(card);
            }
            else if (currentCardCat == "Academic and Religious")
            {
                academicAndReligiousArray.Add(card);
                cardsToRemove.Add(card);
            }
            else if (currentCardCat == "Naval")
            {
                navalArray.Add(card);
            }
            else if (currentCardCat == "Transportation and Economy")
            {
                transportationAndEconomyArray.Add(card);
                cardsToRemove.Add(card);
            }
            else if (currentCardCat == "Defense")
            {
                defenseArray.Add(card);
                cardsToRemove.Add(card);
            }
            else if (currentCardCat == "Scouting")
            {
                scoutingArray.Add(card);
            }
            else if (currentCardCat == "Media and Social")
            {
                mediaAndSocialArray.Add(card);
                cardsToRemove.Add(card);
            }
            else
            {
                Debug.LogError("Card with wrong identifier: " + card.name);
            }
        }

        // Remove the cards after the iteration
        foreach (Card cardToRemove in cardsToRemove)
        {
            actionCardArray.Remove(cardToRemove);
        }
    }

    public void dealActionCards()
    {
        for (int x = 0; x < actionCardHolder.Count;)
        {
            //Stores the Current Card
            Card currentCard = null;
            
            PlayerScript playerScript = player.GetComponent<PlayerScript>();

            if (playerScript.government != null)
            {
                //Get the name of the government
                Government gov = playerScript.government;
                govType = gov.governmentName;

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
                int cardCat = Random.Range(0, 951);

                //Console Messages to Help Me Debug the Path and the Errors
                print("The Cards dealt have a Governemnt");

                if (cardCat >= 0 && cardCat <= 100 + agrarianModifier) //Agrarian
                {
                    int cardCount = agrarianArray.Count;
                    
                    //Pick a Card From Inside One of the Categorys
                    int cardSelect = Random.Range(0, cardCount);

                    if (cardSelect >= 0 && cardSelect < cardCount)
                    {
                        currentCard = agrarianArray[cardSelect];
                        actionCard.Add(currentCard);

                        x++;
                    }

                }else if (cardCat > 100 + agrarianModifier && cardCat <= 200 + militaryModifier + agrarianModifier) //Military
                {
                    int cardCount = militaryArray.Count;

                    //Pick a Card From Inside One of the Categorys
                    int cardSelect = Random.Range(0, cardCount);

                    if (cardSelect >= 0 && cardSelect < cardCount)
                    {
                        currentCard = militaryArray[cardSelect];
                        actionCard.Add(currentCard);
                        
                        x++;
                    }
                }
                else if (cardCat > 200 + militaryModifier + agrarianModifier && cardCat <= 300 + militaryModifier + agrarianModifier + industrialModifier) //Industrial
                {
                    int cardCount = industrialArray.Count;
                        
                    //Pick a Card From Inside One of the Categorys
                    int cardSelect = Random.Range(0, cardCount);

                    
                    if (cardSelect >= 0 && cardSelect < cardCount)
                    {
                        currentCard = industrialArray[cardSelect];
                        actionCard.Add(currentCard);
                        
                        x++;
                    }
                }
                else if (cardCat > 300 + agrarianModifier + militaryModifier + industrialModifier && cardCat <= 400 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier) //Academic and Religious
                {
                    if(governmentAlreadyAssigned == true)
                    {
                        academicAndReligiousArray.RemoveAll(card => card.cardTech == "Government");
                    }

                    int cardCount = academicAndReligiousArray.Count;

                    //Pick a Card From Inside One of the Categories
                    int cardSelect = Random.Range(0, cardCount);

                    if (cardSelect >= 0 && cardSelect < cardCount)
                    {
                        currentCard = academicAndReligiousArray[cardSelect];
                        actionCard.Add(currentCard);

                        x++;
                    }
                }
                else if (cardCat > 400 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier && cardCat <= 500 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier + navalModifier) //Naval
                {
                    int cardCount = navalArray.Count;
                        
                    //Pick a Card From Inside One of the Categorys
                    int cardSelect = Random.Range(0, cardCount);

                    if (cardSelect >= 0 && cardSelect < cardCount)
                    {
                        currentCard = navalArray[cardSelect];
                        actionCard.Add(currentCard);
                        
                        x++;
                    }
                    
                } 
                else if (cardCat > 500 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier  + navalModifier && cardCat <= 600 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier + navalModifier + transportationAndEconomyModifier) //Transportation and Economy
                {
                    int cardCount = transportationAndEconomyArray.Count;
                        
                    //Pick a Card From Inside One of the Categorys
                    int cardSelect = Random.Range(0, cardCount);

                    if (cardSelect >= 0 && cardSelect < cardCount)
                    {
                        currentCard = transportationAndEconomyArray[cardSelect];
                        actionCard.Add(currentCard); 
                        
                        x++;
                    }
                    
                } 
                else if (cardCat > 600 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier  + navalModifier + transportationAndEconomyModifier && cardCat <= 700 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier + navalModifier + transportationAndEconomyModifier + defenseModifier) //Defense
                {
                    int cardCount = defenseArray.Count;
                        
                    //Pick a Card From Inside One of the Categorys
                    int cardSelect = Random.Range(0, cardCount);

                    if (cardSelect >= 0 && cardSelect < cardCount)
                    {
                        currentCard = defenseArray[cardSelect];
                        actionCard.Add(currentCard);
                        
                        x++;
                    }
                    
                } 
                else if (cardCat > 700 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier  + navalModifier + transportationAndEconomyModifier + defenseModifier && cardCat <= 800 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier + navalModifier + transportationAndEconomyModifier + defenseModifier + scoutingModifier) //Scouting
                {
                    int cardCount = scoutingArray.Count;
                        
                    //Pick a Card From Inside One of the Categorys
                    int cardSelect = Random.Range(0, cardCount);

                    if (cardSelect >= 0 && cardSelect < cardCount)
                    {
                        currentCard = scoutingArray[cardSelect];
                        actionCard.Add(currentCard);
                        
                        x++;
                    }
                    
                } 
                else if (cardCat > 800 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier  + navalModifier + transportationAndEconomyModifier + defenseModifier + scoutingModifier && cardCat <= 900 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier + navalModifier + transportationAndEconomyModifier + defenseModifier + scoutingModifier + mediaAndSocialModifier) //Media And Social
                {
                    int cardCount = mediaAndSocialArray.Count;
                        
                    //Pick a Card From Inside One of the Categorys
                    int cardSelect = Random.Range(0, cardCount);

                    if (cardSelect >= 0 && cardSelect < cardCount)
                    {
                        currentCard = mediaAndSocialArray[cardSelect];
                        actionCard.Add(currentCard);
                        
                        x++;
                    }
                    
                } else if (cardCat > 900 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier  + navalModifier + transportationAndEconomyModifier + defenseModifier + scoutingModifier + mediaAndSocialModifier && cardCat <= 950 + militaryModifier + agrarianModifier + industrialModifier + academicAndReligiousModifier + navalModifier + transportationAndEconomyModifier + defenseModifier + scoutingModifier + mediaAndSocialModifier + settlementModifier) // Settlement
                {
                    int cardCount = settlementArray.Count;

                    // Pick a Card From Inside the Category
                    int cardSelect = Random.Range(0, cardCount);

                    if (cardSelect >= 0 && cardSelect < cardCount)
                    {
                        currentCard = settlementArray[cardSelect];
                        actionCard.Add(currentCard);
                        
                        x++;
                    }
                }

            } else {
                int cardCat = Random.Range(0, 951);

                if (cardCat >= 0 && cardCat <= 100) //Agrarian
                {
                    int cardCount = agrarianArray.Count;

                    //Pick a Card From Inside One of the Categories
                    int cardSelect = Random.Range(0, cardCount);

                    if (cardSelect >= 0 && cardSelect < cardCount)
                    {
                        currentCard = agrarianArray[cardSelect];
                        actionCard.Add(currentCard);
                        
                        x++;
                    }
                    
                } 
                else if (cardCat > 100 && cardCat <= 200) //Military
                {
                    int cardCount = militaryArray.Count;

                    //Pick a Card From Inside One of the Categories
                    int cardSelect = Random.Range(0, cardCount);

                    if (cardSelect >= 0 && cardSelect < cardCount)
                    {
                        currentCard = militaryArray[cardSelect];
                        actionCard.Add(currentCard);
                        
                        x++;
                    }
                } 
                else if (cardCat > 200 && cardCat <= 300) //Industrial
                {
                    int cardCount = industrialArray.Count;

                    //Pick a Card From Inside One of the Categories
                    int cardSelect = Random.Range(0, cardCount);

                    if (cardSelect >= 0 && cardSelect < cardCount)
                    {
                        currentCard = industrialArray[cardSelect];
                        actionCard.Add(currentCard);
                        
                        x++;
                    }
                } 
                else if (cardCat > 300 && cardCat <= 400) //Academic and Religious
                {
                    if(governmentAlreadyAssigned == true)
                    {
                        academicAndReligiousArray.RemoveAll(card => card.cardTech == "Government");
                    }

                    int cardCount = academicAndReligiousArray.Count;

                    //Pick a Card From Inside One of the Categories
                    int cardSelect = Random.Range(0, cardCount);

                    if (cardSelect >= 0 && cardSelect < cardCount)
                    {
                        currentCard = academicAndReligiousArray[cardSelect];
                        actionCard.Add(currentCard);
                        
                        x++;
                    }
                } 
                else if (cardCat > 400 && cardCat <= 500) //Naval
                {
                    int cardCount = navalArray.Count;

                    //Pick a Card From Inside One of the Categories
                    int cardSelect = Random.Range(0, cardCount);

                    if (cardSelect >= 0 && cardSelect < cardCount)
                    {
                        currentCard = navalArray[cardSelect];
                        actionCard.Add(currentCard);
                        
                        x++;
                    }
                } 
                else if (cardCat > 500 && cardCat <= 600) //Transportation and Economy
                {
                    int cardCount = transportationAndEconomyArray.Count;

                    //Pick a Card From Inside One of the Categories
                    int cardSelect = Random.Range(0, cardCount);

                    if (cardSelect >= 0 && cardSelect < cardCount)
                    {
                        currentCard = transportationAndEconomyArray[cardSelect];
                        actionCard.Add(currentCard);
                        
                        x++;
                    }
                } 
                else if (cardCat > 600 && cardCat <= 700) //Defense
                {
                    int cardCount = defenseArray.Count;
                        
                    //Pick a Card From Inside One of the Categorys
                    int cardSelect = Random.Range(0, cardCount);

                    if (cardSelect >= 0 && cardSelect < cardCount)
                    {
                        currentCard = defenseArray[cardSelect];
                        actionCard.Add(currentCard);
                        
                        x++;
                    }
                } 
                else if (cardCat > 700 && cardCat <= 800) //Scouting
                {
                    int cardCount = scoutingArray.Count;
                        
                    //Pick a Card From Inside One of the Categorys
                    int cardSelect = Random.Range(0, cardCount);

                    if (cardSelect >= 0 && cardSelect < cardCount)
                    {
                        currentCard = scoutingArray[cardSelect];
                        actionCard.Add(currentCard);
                        
                        x++;
                    }
                } 
                else if (cardCat > 800 && cardCat <= 900) //Media and Social
                {
                    int cardCount = mediaAndSocialArray.Count;
                        
                    //Pick a Card From Inside One of the Categorys
                    int cardSelect = Random.Range(0, cardCount);

                    if (cardSelect >= 0 && cardSelect < cardCount)
                    {
                        currentCard = mediaAndSocialArray[cardSelect];
                        actionCard.Add(currentCard);
                        
                        x++;
                    }

                } else if (cardCat > 900 && cardCat <= 950) //Settlement
                {
                    int cardCount = settlementArray.Count;
                        
                    //Pick a Card From Inside One of the Categorys
                    int cardSelect = Random.Range(0, cardCount);

                    if (cardSelect >= 0 && cardSelect < cardCount)
                    {
                        currentCard = settlementArray[cardSelect];
                        actionCard.Add(currentCard);

                        x++;
                    }
                }
            }
        }
    }

    public void dealEventCards()
    {
        for (int e = 0; e < eventCardHolder.Count; e++)
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
        for (int a = 0; a < actionCardHolder.Count; a++)
        {
            Card card = actionCard[a];

            GameObject physicalCard = Instantiate(cardPrefab, actionCardHolder[a].transform);
            physicalCard.tag = "Card";
            physicalCard.transform.position = actionCardHolder[a].transform.position;
            physicalCard.transform.rotation = actionCardHolder[a].transform.rotation;
            physicalCard.transform.localScale = new Vector3(1f, 1f, 1.2f);

            //Attach the Sctipt, and then the Data
            CardDataHolder cardDataHolder = physicalCard.AddComponent<CardDataHolder>();
            cardDataHolder.cardData = card;

            //Change the color of the card to coincide with the tech category that the card belongs to
            MeshRenderer cardBackingRenderer = physicalCard.transform.Find("CardBacking")?.GetComponent<MeshRenderer>();
            string cardColor = card.cardColor;

            if (cardBackingRenderer != null)
            {
                Color newColor;
                if (ColorUtility.TryParseHtmlString(cardColor, out newColor))
                {
                    // Get a copy of the material
                    Material newMaterial = new Material(cardBackingRenderer.sharedMaterial);
                    // Assign the new color to the material
                    newMaterial.color = newColor;
                    // Assign the updated material to the MeshRenderer
                    cardBackingRenderer.material = newMaterial;
                }
                else
                {
                    Debug.LogError("Invalid color code: " + cardColor);
                }
            }

            // Find the "Card UI Canvas" GameObject within the instantiated prefab, the parent of all of the UI elements for the card.
            //Make sure that the UI element name for all of the things are without spaces, and are properly capsized with the first letter of both words being upper case and everyhting else lowercase, and with no spaces
            Transform cardUICanvas = physicalCard.transform.Find("Card UI Canvas");
            if (cardUICanvas != null)
            {
                TextMeshProUGUI nameSlot = cardUICanvas.GetComponentInChildren<TextMeshProUGUI>();
                if (nameSlot != null)
                    nameSlot.text = card.name;

                TextMeshProUGUI cardDescriptionSlot = cardUICanvas.transform.Find("CardDescription").GetComponent<TextMeshProUGUI>();
                if (cardDescriptionSlot != null)
                    cardDescriptionSlot.text = card.description;

                TextMeshProUGUI cardEraSlot = cardUICanvas.transform.Find("CardEra").GetComponent<TextMeshProUGUI>();
                if (cardEraSlot != null)
                    cardEraSlot.text = card.cardEra;

                TextMeshProUGUI cardCategorySlot = cardUICanvas.transform.Find("CardCategory").GetComponent<TextMeshProUGUI>();
                if (cardCategorySlot != null)
                    cardCategorySlot.text = card.cardCategory;
            }
        }
    }

    public void playEventCards()
    {
        //Assign the cards that where selectd to be placed into the slots for them
        for (int e = 0; e < eventCardHolder.Count; e++)
        {
            //event card sorting and placements
        }
    }
}