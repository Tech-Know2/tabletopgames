using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardPlayer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public string cardTag = "Card";

    private bool isClicked = false;
    private CameraController cameraController;
    private PlayerScript playerScript;
    public CardEffectManager cardEffectManager;

    public Card card;
    private CardDataHolder cardDataHolder;
    public GameObject cardGameObject;

    private void Start()
    {
        Transform playerAndCameraRig = GameObject.Find("Player and Camera Rig")?.transform;
        cardGameObject = gameObject;

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
        if (isClicked == true)
        {
            //Set card game object location to be that of the mouse.

        }
    }

    public void UpdateCamera()
    {
        if (cameraController.cameraPanningAllowed == false)
        {
            cameraController.cameraPanningAllowed = true;
        } else 
        {   
            cameraController.cameraPanningAllowed = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.CompareTag(cardTag))
        {
            if (playerScript.cardsPlayed < playerScript.maxCardsPerTurn)
            {
                isClicked = true;
                UpdateCamera();
                playerScript.cardData = card;
                cardEffectManager.firstTime = true;
                playerScript.CardSelected();
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isClicked)
        {
            isClicked = false;
            cameraController.cameraPanningAllowed = true;

            //Destroy the Game Object After its Use and After it has hit the ground
            if (isClicked == false)
            {
                //destory the object once it has collided with game objects tagged "Deep Sea", "Shallow Sea", "Sand", "Plains", "Mountains"
                Destroy(cardGameObject);
            }
            
        }
    }
}