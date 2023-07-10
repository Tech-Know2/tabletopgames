using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGenerator : MonoBehaviour
{
    public List<string> adjectives = new List<string>();
    public List<string> nouns = new List<string>();

    public string newName;

    public void GenerateName()
    {
        System.Random random = new System.Random(); // Create a new instance of System.Random

        int randomAdj = random.Next(0, adjectives.Count);
        int randomNoun = random.Next(0, nouns.Count);

        string adjWord = adjectives[randomAdj];
        string nounWord = nouns[randomNoun];

        if (adjWord.Length >= 3 && nounWord.Length >= 3)
        {
            string adjFirstThree = adjWord.Substring(0, 3);
            string nounFinalThree = nounWord.Substring(nounWord.Length - 3);

            newName = adjFirstThree + nounFinalThree;
        }
        else
        {
            // Handle the case where either the adjective or noun is too short
            // Assign a default value to newName or display an error message
            newName = "Invalid Name";
            Debug.LogError("Adjective or noun is too short to generate a name.");
        }
    }
}
