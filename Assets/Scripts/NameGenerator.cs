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

        //Pick a random number
        int randomAdj = random.Next(0, adjectives.Count);
        int randomNoun = random.Next(0, nouns.Count);

        //Pick a random word from the list
        string adjWord = adjectives[randomAdj];
        string nounWord = nouns[randomNoun];

        //Random Amounts from each word
        int randomAdjSum = random.Next(2, adjWord.Length - adjWord.Length/4);
        int randomNounSum = random.Next(2 , nounWord.Length - nounWord.Length/4);

        //Grab random amounts from the start of the word and the end of the word
        string adjFirstThree = adjWord.Substring(0, randomAdjSum); //Start of the word
        string nounFinalThree = nounWord.Substring(nounWord.Length - randomNounSum); //End of the word

        //Combine the words together to make the name
        newName = adjFirstThree + nounFinalThree;
    }
}
