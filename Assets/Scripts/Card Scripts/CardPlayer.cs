using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro; 

public class CardPlayer : MonoBehaviour, IPointerClickHandler
{
    public string cardTag = "Card";

    private bool isSelected = false;
    private CameraController cameraController;
    private PlayerScript playerScript;
    public CardEffectManager cardEffectManager;
    private TileSelection tileSelection;

    // UX and UI for the selecting of cards and the playing of cards
    public float cardSelectScaler = 1.3f;
    public GameObject selectedTileLocation;
    public List<string> acceptableSelectionTags = new List<string>();

    public Card card;
    private CardDataHolder cardDataHolder;
    private static CardPlayer previousCard;
    private static CardPlayer currentCard;

    //Pop-up for card requierments
    public TextMeshProUGUI cardRequierment; 
    public GameObject requiermentPopUp;
    private bool popUpActivated = false;

    private void Start()
    {
        popUpActivated = false;
        Transform playerAndCameraRig = GameObject.Find("Player and Camera Rig")?.transform;

        if (playerAndCameraRig != null)
        {
            cameraController = playerAndCameraRig.GetComponentInChildren<CameraController>();
            playerScript = playerAndCameraRig.GetComponentInChildren<PlayerScript>();
            cardEffectManager = playerAndCameraRig.GetComponentInChildren<CardEffectManager>();
        }
        else
        {
            Debug.LogError("Could not find game object named 'Player and Camera Rig'.");
        }

        cardDataHolder = GetComponent<CardDataHolder>();
        card = cardDataHolder.cardData;
    }

    public void Update()
    {
        if (isSelected)
        {
            // Scale the card by the cardSelectScaler value
            transform.localScale = Vector3.one * cardSelectScaler;
        }
        else
        {
            // Reset the card scale to its original size
            transform.localScale = Vector3.one;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameObject.CompareTag(cardTag))
        {
            if (isSelected && currentCard == this)
            {
                // The current card was clicked twice, execute the action
                ExecuteCardAction();
            }
            else if (isSelected && currentCard != this)
            {
                // A different card was clicked, update the previous and current cards
                previousCard = currentCard;
                currentCard.isSelected = false;
                currentCard = this;
                isSelected = true;
            }
            else
            {
                // Select the card
                if (currentCard != null)
                {
                    previousCard = currentCard;
                    previousCard.isSelected = false;
                }

                currentCard = this;

                if (playerScript.cardsPlayed < playerScript.maxCardsPerTurn)
                {
                    isSelected = true;
                }
            }
        }
    }

    public void ExecuteCardAction()
    {
        if (playerScript.cardsPlayed <= playerScript.maxCardsPerTurn)
        {
            if (!card.requiresTileLocation || (card.requiresTileLocation && selectedTileLocation != null))
            {
                // The card does not require a tile or a tile has been selected
                
                // Hide the Pop-up
                popUpActivated = false;
                requiermentPopUp.SetActive(popUpActivated);

                UpdateCamera();
                playerScript.cardData = card;
                cardEffectManager.firstTime = true;
                playerScript.CardSelected();

                // Destroy the card GameObject
                Destroy(gameObject);
            }
            else
            {
                // The card requires a tile, but no tile has been selected
                popUpActivated = true;
                requiermentPopUp.SetActive(popUpActivated);

                cardRequierment.text = "Card Requires a Tile";
            }
        }
    }


    public void UpdateCamera()
    {
        cameraController.cameraPanningAllowed = !cameraController.cameraPanningAllowed;
    }
}