using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardAuctionHouseDisplay : MonoBehaviour
{
    public Card card;

    public TextMeshProUGUI cardName;
    public TextMeshProUGUI cardDescription;
    public TextMeshProUGUI cardEra;
    public TextMeshProUGUI cardCategory;
    public Image panelBackground;

    public void GenerateCard(Card card)
    {
        panelBackground = GetComponent<Image>();
        SetPanelBackgroundColor(card.cardColor);

        cardName.text = card.cardName;
        cardDescription.text = card.description;
        cardEra.text = card.cardEra;
        cardCategory.text = card.cardCategory;
    }

    private void SetPanelBackgroundColor(string hexColor)
    {
        Color targetColor;

        if (ColorUtility.TryParseHtmlString(hexColor, out targetColor))
        {
            panelBackground.color = targetColor;
        }
        else
        {
            Debug.LogError("Invalid color format: " + hexColor);
        }
    }
}
