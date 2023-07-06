using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardPlayer : MonoBehaviour, IPointerClickHandler
{
    public string cardTag = "Card";

    private bool isSelected = false;
    //private bool isClicked = false;
    private CameraController cameraController;
    private PlayerScript playerScript;
    public CardEffectManager cardEffectManager;

    // UX and UI for the selecting of cards and the playing of cards
    public float cardSelectScaler = 1.3f;
    public GameObject selectedTileLocation;
    public List<string> acceptableOutlineTags = new List<string>();

    public Card card;
    private CardDataHolder cardDataHolder;
    public GameObject currentCardGameObject;
    private GameObject previousCardGameObject;

    private void Start()
    {
        Transform playerAndCameraRig = GameObject.Find("Player and Camera Rig")?.transform;
        currentCardGameObject = gameObject;

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
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameObject.CompareTag(cardTag))
        {
            if (isSelected)
            {
                // Perform the desired functions, update camera, and send the card to the player script
                UpdateCamera();
                playerScript.cardData = card;
                cardEffectManager.firstTime = true;
                playerScript.CardSelected();

                // Destroy the card GameObject
                Destroy(currentCardGameObject);
            }
            else
            {
                // Select the card
                isSelected = true;
            }
        }
    }

    public void UpdateCamera()
    {
        cameraController.cameraPanningAllowed = !cameraController.cameraPanningAllowed;
    }
}