using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardPlayer : MonoBehaviour, IPointerClickHandler
{
    public string cardTag = "Card";

    private bool isSelected = false;
    private CameraController cameraController;
    private PlayerScript playerScript;
    public CardEffectManager cardEffectManager;

    // UX and UI for the selecting of cards and the playing of cards
    public float cardSelectScaler = 1.3f;
    public GameObject selectedTileLocation;
    public List<string> acceptableSelectionTags = new List<string>();

    public Card card;
    private CardDataHolder cardDataHolder;
    private static CardPlayer previousCard;
    private static CardPlayer currentCard;

    private void Start()
    {
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

        if (acceptableSelectionTags.Contains(gameObject.tag))
        {
            selectedTileLocation = gameObject;
            print("Location Selected");
        }

    }

    public void ExecuteCardAction()
    {
        if (playerScript.cardsPlayed <= playerScript.maxCardsPerTurn)
        {
            // Perform the desired functions, update camera, and send the card to the player script
            UpdateCamera();
            playerScript.cardData = card;
            cardEffectManager.firstTime = true;
            playerScript.CardSelected();

            // Destroy the card GameObject
            Destroy(gameObject);
        }
    }

    public void UpdateCamera()
    {
        cameraController.cameraPanningAllowed = !cameraController.cameraPanningAllowed;
    }
}