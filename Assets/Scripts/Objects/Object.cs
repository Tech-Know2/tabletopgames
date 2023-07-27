using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "Object")]
public class Object : ScriptableObject
{
    public string objectName;
    public int turnsToProduce;

    //Is Religious
    public bool isReligious;
    public string religionColor;

}
