using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private bool isPlayerInRange;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 7)] private string[] dialogueLines;

    private bool didDialogueStart;
    private int lineIndex;

    void Update()
    {
        if(isPlayerInRange && Input.GetButtonDown("Fire1"))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogue();
            }        
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        StartCoroutine(Showline());
    }

    private void NextDialogue()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(Showline());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
        }
    }

    private IEnumerator Showline()
    {
        dialogueText.text = string.Empty;

        foreach (char c in dialogueLines[lineIndex])
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {   
        isPlayerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {     
            isPlayerInRange = false;       
    }
}
