using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settlement", menuName = "Settlement")]
public class Settlements : ScriptableObject
{
    public string cityName;
    public int cityLevel;
    public int cityPopulation;

    private List<GameObject> religiousFollowers = new List<GameObject>();
}
