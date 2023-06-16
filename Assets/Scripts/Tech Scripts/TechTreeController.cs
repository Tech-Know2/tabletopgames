using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechTreeController : MonoBehaviour
{
    public Button techTreeButton;
    public GameObject techTree;
    public RawImage nonagon;

    public Tech requiredTech;
    private bool isRequiredTechResearched;
    private bool scrollEnabled;

    public List<GameObject> techBranches = new List<GameObject>();
    public GameObject techBranchHolder;
    private int currentBranchNumber = 0;

    private float rotationValue;
    private float currentRotation = 0f;
    
    public bool treeEnabled;

    // Start is called before the first frame update
    void Start()
    {
        techTree.SetActive(false);
        treeEnabled = false;

        rotationValue = 360f / techBranches.Count;

        techBranchHolder.SetActive(true);

        for (int x = 0; x < techBranches.Count; x++)
        {
            if (x == 0)
            {
                techBranches[x].SetActive(true);
            }
            else
            {
                techBranches[x].SetActive(false);
            }
        }
    }

    public void checkRequiredResearch()
    {
        if(requiredTech.isResearched == true)
        {
            scrollEnabled = true;
        } else 
        {
            scrollEnabled = false;
        }
    }

    public void ActivateTree()
    {
        if (treeEnabled == false)
        {
            treeEnabled = true;
            techTree.SetActive(true);

            // Check if requiredTech is not null before accessing its properties
            if (requiredTech != null && requiredTech.isResearched == true)
            {
                scrollEnabled = true;
            }
            else
            {
                scrollEnabled = false;
            }

            currentRotation = currentBranchNumber * rotationValue; // Calculate the initial rotation based on the current branch number
            nonagon.rectTransform.rotation = Quaternion.Euler(0f, 0f, currentRotation);

            displayCurrentBranch();

        }
        else
        {
            treeEnabled = false;
            techTree.SetActive(false);
        }
    }


    public void rightSpin()
    {
        checkRequiredResearch();
        
        if (scrollEnabled == true)
        {
            if (currentBranchNumber >= techBranches.Count - 1)
            {
                currentBranchNumber = 0;
            }
            else
            {
                currentBranchNumber++;
            }

            currentRotation -= rotationValue;
            nonagon.rectTransform.rotation = Quaternion.Euler(0f, 0f, currentRotation);

            displayCurrentBranch();
        }
    }

    public void leftSpin()
    {
        checkRequiredResearch();

        if (scrollEnabled == true)
        {
            if (currentBranchNumber > 0)
            {
                currentBranchNumber--;
            }
            else
            {
                currentBranchNumber = techBranches.Count - 1;
            }

            currentRotation += rotationValue;
            nonagon.rectTransform.rotation = Quaternion.Euler(0f, 0f, currentRotation);

            displayCurrentBranch();
        }
    }

    public int displayCurrentBranch()
    {
        print(currentBranchNumber);
        for (int i = 0; i < techBranches.Count; i++)
        {
            if (i == currentBranchNumber)
            {
                techBranches[i].SetActive(true);
            }
            else
            {
                techBranches[i].SetActive(false);
            }
        }
        return currentBranchNumber;
    }
}
