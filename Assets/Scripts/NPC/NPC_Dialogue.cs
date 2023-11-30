using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC_Dialogue : MonoBehaviour
{
    public float dialogueDistance = 2f;
    public LayerMask playerLayer;
    public DialogueSettings dialogueSettings;

    bool playerHit;
    private string[] sentences = null;
    private string[] actorsNames = null;
    private Sprite[] actorsSprites = null;

    // Start is called before the first frame update
    void Start()
    {
        playerLayer = LayerMask.GetMask("Player");
        
        // Recupera as frases do NPC
        switch (DialogueControl.instance.language)
        {
            case DialogueControl.Language.Portuguese:
                sentences = dialogueSettings.dialogues.Select(x => x.sentence.portuguese).ToArray();
                break;
            case DialogueControl.Language.English:
                sentences = dialogueSettings.dialogues.Select(x => x.sentence.english).ToArray();
                break;
            case DialogueControl.Language.Spanish:
                sentences = dialogueSettings.dialogues.Select(x => x.sentence.spanish).ToArray();
                break;
            default:
                break;
        }

        actorsNames = dialogueSettings.dialogues.Select(x => x.actorName).ToArray();
        actorsSprites = dialogueSettings.dialogues.Select(x => x.profile).ToArray();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerHit)
        {
            DialogueControl.instance.Speech(sentences, actorsNames, actorsSprites);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ShowDialogue();
    }

    void ShowDialogue()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, dialogueDistance, playerLayer);

        if (hit != null)
            playerHit = true;
        else
            playerHit = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueDistance);
    }
}
