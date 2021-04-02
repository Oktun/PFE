using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Line
{
    public CharacterDS character;

    [TextArea(2, 5)]
    public string text; 
}


[CreateAssetMenu(fileName ="New Conversation", menuName ="Conversation")]
public class Conversation : ScriptableObject
{

    public CharacterDS speackerLeft;
    public CharacterDS speackerRight;
    public List<Line> lines;
}
