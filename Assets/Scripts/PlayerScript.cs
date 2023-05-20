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
    public Dealer dealer;
    public PlacementScript placementScript;
    public EconomyManager economyManager;

    // Selected Card Game Object
    private GameObject selectedCard;

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
        } else if (currentTurn < eraTwoLength && currentTurn >= eraTwoLength)
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
                tempBuildingStorage = new List<GameObject>(card.buildings);

                placementScript.cardBuildings = tempBuildingStorage;
            }
        }
    }


    public void InitiateBuildingPlacement()
    {
        placementScript.Start();
        placementScript.Update();
    }
}
