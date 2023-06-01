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
    private bool isRequieredTechResearched;
    private bool scrollEnabled = true;

    public List<GameObject> techBranches = new List<GameObject>();
    private int currentBranchNumber = 0;

    public float rotationValue = 40f;
    private float currentRotation = 0f;
    
    public bool treeEnabled;

    // Start is called before the first frame update
    void Start()
    {
        techTree.SetActive(false);
        treeEnabled = false;

        for (int x = 0; x < techBranches.Count; x++)
        {
            techBranches[x].SetActive(true);
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
        if (scrollEnabled == true)
        {
            currentRotation -= rotationValue;
            nonagon.rectTransform.rotation = Quaternion.Euler(0f, 0f, currentRotation);

            if (currentBranchNumber >= techBranches.Count - 1)
            {
                currentBranchNumber = 0;
            }
            else
            {
                currentBranchNumber++;
            }

            displayCurrentBranch();
        }
    }

    public void leftSpin()
    {
        if (scrollEnabled == true)
        {
            currentRotation += rotationValue;
            nonagon.rectTransform.rotation = Quaternion.Euler(0f, 0f, currentRotation);

            if (currentBranchNumber > 0)
            {
                currentBranchNumber--;
            }
            else
            {
                currentBranchNumber = techBranches.Count - 1;
            }

            displayCurrentBranch();
        }
    }

    public void displayCurrentBranch()
    {
        print(currentBranchNumber);

        for (int i = 0; i < techBranches.Count; i++)
        {
            if (i == currentBranchNumber)
            {
                techBranches[i].SetActive(true);
            } else 
            {
                techBranches[i].SetActive(false);
            }
        }
    }
}
