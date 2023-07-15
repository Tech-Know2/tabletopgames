using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro; 

//Not really used fazed out for a newer script. Thanks

public class CardPlayer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string cardTag = "Card";

    private bool isDragging = false;
    private Vector3 originalPosition;
    private CameraController cameraController;
    private PlayerScript playerScript;
    public CardEffectManager cardEffectManager;

    public GameObject selectedTileLocation;
    public List<string> acceptableSelectionTags = new List<string>();

    public Card card;
    private CardDataHolder cardDataHolder;

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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameObject.CompareTag(cardTag))
        {
            // Select the card
            if (playerScript.cardsPlayed < playerScript.maxCardsPerTurn)
            {
                ExecuteCardAction();
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gameObject.CompareTag(cardTag))
        {
            // Start dragging the card
            isDragging = true;
            originalPosition = transform.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            // Convert screen space position to world space
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(eventData.position);
            worldPosition.z = 0f; // Set the z-coordinate to the desired value

            // Update the card's position
            transform.position = worldPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            // Stop dragging and make the card fall down
            isDragging = false;
            //transform.position = originalPosition;
        }
    }

    public void ExecuteCardAction()
    {
        if (playerScript.cardsPlayed <= playerScript.maxCardsPerTurn)
        {
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
        //cameraController.cameraPanningAllowed = !cameraController.cameraPanningAllowed;
    }
}
