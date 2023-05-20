using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechDisplay : MonoBehaviour
{
    public Tech tech;
    public Dealer dealer;
    public PlayerScript playerScript;

    public Button previousTechButton;
    private Tech previousTech;

    void Start()
    {
        tech.isResearched = false;
    }

    public void techResearch()
    {
        if (previousTechButton != null)
        {
            TechDisplay techDisplay = previousTechButton.GetComponent<TechDisplay>();
            if (techDisplay != null)
            {
                previousTech = techDisplay.tech;
            }
        }

        if (previousTech == null && playerScript.currentEra == tech.techEra)
        {
            tech.isResearched = true;

            if(tech.techCards.Count != 0)
            {
                dealer.actionCardArray.AddRange(tech.techCards);
                dealer.filterCards();
            }

            print(tech.techName + " Researched");
        }
        else if (previousTech != null && tech.isResearched == false && previousTech.isResearched == true && playerScript.currentEra == tech.techEra)
        {
            tech.isResearched = true;
            
            if(tech.techCards.Count != 0)
            {
                dealer.actionCardArray.AddRange(tech.techCards);
                dealer.filterCards();
            }

            print(tech.techName + " Researched");
        }
    }
}
