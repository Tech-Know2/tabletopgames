using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechTreeController : MonoBehaviour
{
    public Button techTreeButton;
    public GameObject techTree;
    public RawImage nonagon;

    public float rotationValue = 40f;
    private float currentRotation = 0f;
    
    public bool treeEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        techTree.SetActive(false);
    }

    public void ActivateTree()
    {
        if (treeEnabled == false)
        {
            treeEnabled = true;
            techTree.SetActive(true);
        } else {
            treeEnabled = false;
            techTree.SetActive(false);
        }
    }
    public void rightSpin()
    {
        currentRotation -= rotationValue;
        nonagon.rectTransform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
    }

    public void leftSpin()
    {
        currentRotation += rotationValue;
        nonagon.rectTransform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
    }
}
