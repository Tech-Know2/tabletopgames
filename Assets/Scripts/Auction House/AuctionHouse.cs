using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuctionHouse : MonoBehaviour
{
    public List<Card> auctionHouseCards = new List<Card>();

    // Prefab for the Card UI Elements
    public GameObject cardUITemplate;
    public List<GameObject> spawnLocations = new List<GameObject>();

    //A list of all of the cards that have just been generated in UI
    public List<GameObject> generatedUICards = new List<GameObject>();

    public void GenerateCardLayout()
    {
        for (int i = 0; i < spawnLocations.Count; i++)
        {
            GameObject cardUI = Instantiate(cardUITemplate, spawnLocations[i].transform);
            CardAuctionHouseDisplay cardDisplay = cardUI.GetComponent<CardAuctionHouseDisplay>();
            cardDisplay.GenerateCard(auctionHouseCards[i]);

            generatedUICards.Add(cardUI);
        }
    }
}
