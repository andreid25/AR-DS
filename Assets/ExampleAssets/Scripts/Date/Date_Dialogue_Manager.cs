using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Date_Dialogue_Manager : MonoBehaviour
{
    public TMP_Text dialogueText;
    [SerializeField] Image dialogueBox;

    [SerializeField] Animator animator;

    private Queue<string> sentences;
    private bool dialogueActive;

    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
        dialogueBox.enabled = false;
    }

    public void StartDialogue (Dialogue dialogue)
    {
        UnityEngine.Debug.Log("Dialogue Started");
        dialogueBox.enabled = true;
        animator.SetBool("IsOpen", true);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        UnityEngine.Debug.Log("End of Conversation");
        dialogueBox.enabled = false;
        animator.SetBool("IsOpen", false);
    }

    void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began) && dialogueBox.enabled)
        {
            DisplayNextSentence();
        }
    }
}
