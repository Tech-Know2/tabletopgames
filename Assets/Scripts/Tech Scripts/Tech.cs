using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Tech", menuName = "Tech")]
public class Tech : ScriptableObject
{
    public string techName;
    public string description;
    public string techType;
    public string techColor;
    public int techEra;

    public bool isResearched = false;

    public int goldCost;
    public List<Button> techButtons = new List<Button>();
    public List<GameObject> techCards = new List<GameObject>();
}
