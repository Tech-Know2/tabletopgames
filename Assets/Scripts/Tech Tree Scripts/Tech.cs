using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tech", menuName = "Tech")]
public class Tech : ScriptableObject
{
    public string techName;
    public string description;
    public string techType;
    
    public int cost;
    public GameObject previousTech;

    public List<GameObject> techCards = new List<GameObject>();
}
