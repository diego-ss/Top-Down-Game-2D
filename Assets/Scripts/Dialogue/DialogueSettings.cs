using System;
using System.Collections.Generic;
using UnityEditor;
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

#if UNITY_EDITOR
[CustomEditor(typeof(DialogueSettings))]
public class BuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialogueSettings dialogueSettings = (DialogueSettings)target;
        Languages languages = new Languages();
        languages.portuguese = dialogueSettings.sentence;

        Sentence sentence = new Sentence();
        sentence.profile = dialogueSettings.speakerSprite;
        sentence.sentence = languages;

        if (GUILayout.Button("Create Dialogue") && !string.IsNullOrEmpty(dialogueSettings.sentence))
        {
            dialogueSettings.dialogues.Add(sentence);
            dialogueSettings.speakerSprite = null;
            dialogueSettings.sentence = null;
        }
    }
}

#endif
