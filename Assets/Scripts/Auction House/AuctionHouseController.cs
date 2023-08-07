using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuctionHouseController : MonoBehaviour
{
    //Display Controller for the Auction House
    public Button auctionHouseButton;
    private bool auctionHouseEnabled = false;
    public GameObject auctionHouse;

    //Access the Camera to enable and disable the movement
    public CameraController cameraController;

    //Access the Auction House Information
    public AuctionHouse auctionHouseScript;

    public void Start()
    {
        cameraController.cameraPanningAllowed = true;
        cameraController.cameraScrollAllowed = true;
        cameraController.cameraMovementAllowed = true;
    }

    public void AuctionHouseClick()
    {
        if(auctionHouseEnabled == false)
        {
            auctionHouseEnabled = true;
            auctionHouse.SetActive(auctionHouseEnabled);

            cameraController.cameraPanningAllowed = auctionHouseEnabled;
            cameraController.cameraScrollAllowed = auctionHouseEnabled;
            cameraController.cameraMovementAllowed = auctionHouseEnabled;

            auctionHouseScript.GenerateCardLayout();
        } else 
        {
            auctionHouseEnabled = false;
            auctionHouse.SetActive(auctionHouseEnabled);

            cameraController.cameraPanningAllowed = auctionHouseEnabled;
            cameraController.cameraScrollAllowed = auctionHouseEnabled;
            cameraController.cameraMovementAllowed = auctionHouseEnabled;

            foreach (GameObject cardUI in auctionHouseScript.generatedUICards)
            {
                Destroy(cardUI);
            }

            auctionHouseScript.generatedUICards.Clear();
        }
    }

}
