using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Date_Dialogue_Manager : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText, option1Text, option2Text, option3Text;
    [SerializeField] private Image dialogueBox, optionsBox1, optionsBox2, optionsBox3;

    [SerializeField] private Animator dialogueAnimator, options1Animator, options2Animator, options3Animator;

    private Queue<string> sentences;
    private bool dialogueActive;
    private List<string> responses = new List<string>();
    private int responseGiven = 0;

    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
        dialogueBox.enabled = false;
        optionsBox1.enabled = false;
        optionsBox2.enabled = false;
        optionsBox3.enabled = false;

        dialogueText.enabled = false;
        option1Text.enabled = false;
        option2Text.enabled = false;
        option3Text.enabled = false;

        dialogueAnimator.SetBool("IsOpen", false);
        options1Animator.SetBool("OptionsOpen", false);
        options2Animator.SetBool("OptionsOpen", false);
        options3Animator.SetBool("OptionsOpen", false);
    }

    public void StartDialogue (List<string> dialogue, List<string> options)
    {
        responses = options;
        UnityEngine.Debug.Log("Dialogue Started");
        dialogueBox.enabled = true;
        dialogueText.enabled = true;
        dialogueAnimator.SetBool("IsOpen", true);

        sentences.Clear();

        foreach (string sentence in dialogue)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            if (responses.Count != 0)
            {
                CheckResponses();
            }
            else
            {
                EndDialogue();
            }
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

    void CheckResponses()
    {
        if (responses.Count != 0)
        {
            //code here to display dialogue options
            if (responses.Count == 3)
            {
                optionsBox3.enabled = true;
                option3Text.enabled = true;
                options3Animator.SetBool("OptionsOpen", true);
                option3Text.text = responses[2];
            }
            if (responses.Count >= 2)
            {
                optionsBox2.enabled = true;
                option2Text.enabled = true;
                options2Animator.SetBool("OptionsOpen", true);
                option2Text.text = responses[1];
            }
            if (responses.Count >= 1)
            {
                optionsBox1.enabled = true;
                option1Text.enabled = true;
                options1Animator.SetBool("OptionsOpen", true);
                option1Text.text = responses[0];
            }
        }
        
    }

    public void ResponseOne()
    {
        optionsBox1.enabled = false;
        options1Animator.SetBool("OptionsOpen", false);
        optionsBox2.enabled = false;
        options2Animator.SetBool("OptionsOpen", false);
        optionsBox3.enabled = false;
        options3Animator.SetBool("OptionsOpen", false);

        option1Text.enabled = false;
        option2Text.enabled = false;
        option3Text.enabled = false;

        responseGiven = 1;
        EndDialogue();
    }
    public void ResponseTwo()
    {
        optionsBox1.enabled = false;
        options1Animator.SetBool("OptionsOpen", false);
        optionsBox2.enabled = false;
        options2Animator.SetBool("OptionsOpen", false);
        optionsBox3.enabled = false;
        options3Animator.SetBool("OptionsOpen", false);

        option1Text.enabled = false;
        option2Text.enabled = false;
        option3Text.enabled = false;

        responseGiven = 2;
        EndDialogue();
    }
    public void ResponseThree()
    {
        optionsBox1.enabled = false;
        options1Animator.SetBool("OptionsOpen", false);
        optionsBox2.enabled = false;
        options2Animator.SetBool("OptionsOpen", false);
        optionsBox3.enabled = false;
        options3Animator.SetBool("OptionsOpen", false);

        option1Text.enabled = false;
        option2Text.enabled = false;
        option3Text.enabled = false;

        responseGiven = 3;
        EndDialogue();
    }

    void EndDialogue()
    {
        //code here to either end conversation and go back to dialogue trigger or deal with selected options.
        UnityEngine.Debug.Log("End of Conversation");
        dialogueBox.enabled = false;
        dialogueText.enabled = false;
        dialogueAnimator.SetBool("IsOpen", false);

        FindObjectOfType<DialogueTrigger>().EndOfDialogue(responseGiven);
    }

    void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began) && dialogueBox.enabled)
        {
            DisplayNextSentence();
        }
    }
}
