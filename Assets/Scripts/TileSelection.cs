using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileSelection : MonoBehaviour, IPointerClickHandler
{
    private CardPlayer cardPlayer;
    private CameraController cameraController;
    private PlayerScript playerScript;
    private CardEffectManager cardEffectManager;

    public void Start()
    {
        Transform playerAndCameraRig = GameObject.Find("Player and Camera Rig")?.transform;

        if (playerAndCameraRig != null)
        {
            cardPlayer = playerAndCameraRig.GetComponentInChildren<CardPlayer>();
            playerScript = playerAndCameraRig.GetComponentInChildren<PlayerScript>();
            cardEffectManager = playerAndCameraRig.GetComponentInChildren<CardEffectManager>();
            cameraController = playerAndCameraRig.GetComponentInChildren<CameraController>();
        }
        else
        {
            Debug.LogError("Could not find game object named 'Player and Camera Rig'.");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (cardPlayer.acceptableSelectionTags.Contains(gameObject.tag))
        {
            cardPlayer.selectedTileLocation = gameObject;
            print("Location Selected");
        }
    }
}
