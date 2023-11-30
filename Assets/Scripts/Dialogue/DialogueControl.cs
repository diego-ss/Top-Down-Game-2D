using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [Serializable]
    public enum Language
    {
        Portuguese,
        English,
        Spanish
    }

    public Language language;

    [Header("Components")]
    public GameObject dialogueContainer; // janeça do dialogo
    public Image profileSprite; // sprite do perfil do personagem
    public Text dialogueText; // texto do dialogo
    public Text actorNameText; // nome do personagem

    [Header("Settings")]
    public float typingSpeed = 0.05f; // velocidade de digitação do texto

    // Variáveis de controle
    private bool isShowing; // se o dialogo está sendo mostrado
    private int index; // indice do texto atual
    private string[] sentences; // lista de textos
    private string[] actorsNames; // lista de nomes
    private Sprite[] profileImages; // lista de imagens de perfil

    private Player player;

    public static DialogueControl instance;

    public bool IsShowing { get => isShowing; set => isShowing = value; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TypeSentence()
    {
        dialogueText.text = "";
        actorNameText.text = actorsNames[index];
        profileSprite.sprite = profileImages[index];

        foreach (char letter in sentences[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    /// <summary>
    /// pular para próxima fala
    /// </summary>
    public void NextSentence()
    {
        if(dialogueText.text == sentences[index])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                StartCoroutine(TypeSentence());
            }
            else
            {
                dialogueContainer.SetActive(false);
                IsShowing = false;
                index = 0;
                dialogueText.text = "";
                sentences = null;
                player.CanMove = true;
            }
        }
    }

    /// <summary>
    /// chamar dialogo do personagem
    /// </summary>
    public void Speech(string[] txt, string[] actors, Sprite[] profile)
    {
        if(!IsShowing)
        {
            dialogueContainer.SetActive(true);
            sentences = txt;
            actorsNames = actors;
            profileImages = profile;
            IsShowing = true;
            StartCoroutine(TypeSentence());
            player.CanMove = false;
        }
    }
}
