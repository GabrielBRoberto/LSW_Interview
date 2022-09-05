using System.Collections.Generic;
using NodeCanvas.DialogueTrees;
using System.Collections;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    private DialogueTreeController dialogueTreeController;
    bool onAreaToDialogue = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogueTreeController = GetComponent<DialogueTreeController>();
    }

    private void Update()
    {
        if (dialogueTreeController == null)
        {
            return;
        }

        if (onAreaToDialogue && Input.GetKeyDown(KeyCode.E))
        {
            dialogueTreeController.StartDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            onAreaToDialogue = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            onAreaToDialogue = false;

            if (dialogueTreeController.isRunning)
            {
                dialogueTreeController.StopDialogue();
            }
        }
    }
}
