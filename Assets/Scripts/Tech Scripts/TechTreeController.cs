using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechTreeController : MonoBehaviour
{
    public Button techTreeButton;
    public GameObject techTree;
    
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
}
