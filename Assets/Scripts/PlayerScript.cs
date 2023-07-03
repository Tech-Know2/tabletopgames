using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Vars for Game Management
    public int currentEra;
    public int currentTurn;

    //Era Turn Deciding Variables
    public int eraOneLength;
    public int eraTwoLength;

    // Associate the script with our code so it can be called
    public CardPlayer cardPlayer;
    public Dealer dealer;
    public PlacementScript placementScript;
    public EconomyManager economyManager;
    public Government government;

    // Selected Card Game Object
    private GameObject selectedCard;
    public Card cardData;

    //Amount of Cards that Can Be Played Per Turn
    public int maxCardsPerTurn = 3;
    public int cardsPlayed = 0;

    //Controlling the Interactions with the Cards In Game
    public CardEffectManager cardEffectManager;

    // Start is called before the first frame update
    void Start()
    {
        // Assign the other script to this variable
        dealer = GetComponent<Dealer>();
        placementScript = GetComponent<PlacementScript>();

        currentEra = 1;
        currentTurn = 1;

        dealer.filterCards();
    }

    void checkEra()
    {
        if(currentTurn < eraOneLength)
        {
            currentEra = 1;
        } else if (currentTurn < eraTwoLength)
        {
            currentEra = 2;
        } else 
        {
            currentEra = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkEra();

        economyManager.passedTurnCount = currentTurn;
        economyManager.passedEraCount = currentEra;

        if (Input.GetMouseButtonDown(0)) // 0 represents the left mouse button
        {
            // Convert the mouse position from screen space to world space
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Temp Storage of the Buildings of the Selected Card
            List<GameObject> tempBuildingStorage = new List<GameObject>();

            // Cast a ray from the camera to detect if it hits the GameObject
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && (hit.collider.CompareTag("Action Card") || hit.collider.CompareTag("Event Card")))
            {
                selectedCard = hit.collider.gameObject;

                Card card = selectedCard.GetComponent<Card>();
                tempBuildingStorage = new List<GameObject>(card.buildingObjects);

                placementScript.cardBuildings = tempBuildingStorage;
            }
        }
    }

    public void CardSelected()
    {
        if (cardsPlayed < maxCardsPerTurn)
        {
            cardsPlayed = cardsPlayed + 1;
            PlayCard();
        } else 
        {
            print("You Have Reached Max Cards Played Per Turn");
        }
    }

    public void PlayCard()
    {
        cardEffectManager.card = cardData;
        cardEffectManager.EffectFilter();
    }

    public void InitiateBuildingPlacement()
    {
        placementScript.Start();
        placementScript.Update();
    }
}
