using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour
{
    private GameObject selectedCard;
    private CameraController cameraController;
    private PlayerScript playerScript;
    private CardEffectManager cardEffectManager;

    // Original position and parent vars
    private Vector3 originalPosition;
    private Transform originalParent;
    private Vector3 originalScale;

    //Sending Data to the Effect Manager and the Player Script
    private CardDataHolder cardDataHolder;
    private GameObject collidedObject;
    public GameObject selectedTileLocation;
    public List<string> acceptableSelectionTags = new List<string>();

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
    }

    private void Update()
    {
        if (playerScript.cardsPlayed < playerScript.maxCardsPerTurn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (selectedCard == null)
                {
                    RaycastHit hit = CastRay();

                    if (hit.collider != null)
                    {
                        if (!hit.collider.CompareTag("Card"))
                        {
                            return;
                        }
                        
                        //Pick Up The Card Logic
                        selectedTileLocation = null;

                        selectedCard = hit.collider.gameObject;
                        originalParent = selectedCard.transform.parent;
                        originalPosition = selectedCard.transform.position;
                        originalScale = selectedCard.transform.localScale;
                        

                        selectedCard.transform.SetParent(null);
                        selectedCard.transform.localScale = new Vector3(10f, 15f, 1f);
                        selectedCard.transform.SetParent(null);
                        Cursor.visible = false;

                        // Store the original position and parent

                    }
                }
                else
                {
                    //Release the card
                    ExecuteCardAction();

                    selectedCard.transform.SetParent(null);
                    selectedCard.transform.localScale = new Vector3(4f, 6f, 0.4f);

                    selectedCard = null;
                    Cursor.visible = true;
                }
            }

            if (selectedCard != null)
            {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedCard.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                selectedCard.transform.position = new Vector3(worldPosition.x, 2f, worldPosition.z);
            }
        }
        

        // Check for Escape button press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (selectedCard != null)
            {
                // Reset the card to its original position, scale, and parent
                selectedCard.transform.SetParent(originalParent);
                selectedCard.transform.position = originalParent.transform.position;
                //selectedCard.transform.localScale = new Vector3(11.173914f ,17.3849792f ,1f );
                selectedCard.transform.localScale = originalScale;

                selectedCard = null;
                Cursor.visible = true;
            }
        }
    }

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);

        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);

        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }

    public void ExecuteCardAction()
    {
        if (playerScript.cardsPlayed < playerScript.maxCardsPerTurn)
        {
            cardDataHolder = selectedCard.GetComponent<CardDataHolder>();
            playerScript.cardData = cardDataHolder.cardData;

            // Check collisions with objects from acceptable tags list
            Collider[] colliders = Physics.OverlapBox(selectedCard.transform.position, selectedCard.transform.localScale / 2f);
            foreach (Collider collider in colliders)
            {
                if (acceptableSelectionTags.Contains(collider.tag))
                {
                    selectedTileLocation = collider.gameObject;
                    break;
                }
            }

            playerScript.selectedTileLocation = selectedTileLocation;
            cardEffectManager.firstTime = true;
            playerScript.CardSelected();

            // Destroy the card GameObject after it has collided and stored the data of its collision
            if (playerScript.cardData != null && playerScript.selectedTileLocation != null)
            {
                Destroy(selectedCard);
            }
        }
    }

}