using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TypeSentence()
    {
        dialogueText.text = "";
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

    }

    /// <summary>
    /// chamar dialogo do personagem
    /// </summary>
    public void Speech(string[] txt)
    {
        if(!isShowing)
        {
            dialogueContainer.SetActive(true);
            sentences = txt;
            isShowing = true;
            StartCoroutine(TypeSentence());
        }
    }
}
