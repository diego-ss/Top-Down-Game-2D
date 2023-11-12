using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue", order = 1)]
public class DialogueSettings : ScriptableObject
{
    [Header("Settings")]
    public GameObject actor;

    [Header("Dialogue")]
    public Sprite speakerSprite;
    public string sentence;

    public List<Sentence> dialogues = new List<Sentence>();
}

[Serializable]
public class Sentence
{
    public string actorName;
    public Sprite profile;
    public Languages sentence;
}

[Serializable]
public class Languages
{
    public string portuguese;
    public string english;
    public string spanish;
}
