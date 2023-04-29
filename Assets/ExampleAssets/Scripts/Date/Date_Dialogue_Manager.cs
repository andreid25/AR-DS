using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq.Expressions;

public class Date_Dialogue_Manager : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText, option1Text, option2Text, option3Text;
    [SerializeField] private Image dialogueBox, optionsBox1, optionsBox2, optionsBox3;

    [SerializeField] private Animator dialogueAnimator, options1Animator, options2Animator, options3Animator;

    private Queue<string> sentences = new Queue<string>();
    private bool dialogueActive;
    private List<string> responses = new List<string>();
    private Queue<string> anims = new Queue<string>();
    private int responseGiven = 0;
    private string currentAnim = "";

    // Start is called before the first frame update
    void Awake()
    {
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

    public void StartDialogue (List<string> dialogue, List<string> options, List<string> animsGiven)
    {
        UnityEngine.Debug.Log("In StartDialogue()");
        responses = options;

        dialogueBox.enabled = true;
        dialogueText.enabled = true;
        dialogueAnimator.SetBool("IsOpen", true);

        sentences.Clear();
        anims.Clear();

        foreach (string sentence in dialogue)
        {
            UnityEngine.Debug.Log(sentence);
            sentences.Enqueue(sentence);
        }
        foreach (string animation in animsGiven)
        {
            anims.Enqueue(animation);
        }
        UnityEngine.Debug.Log(sentences);

        DisplayNextSentence();
        AnimationQueue();
    }

    //call appropriate function for animation manager
    private void AnimationQueue()
    {
        UnityEngine.Debug.Log("Reached queue");
        string animToPlay = anims.Dequeue();
        if (currentAnim == animToPlay)
        {
            return;
        }
        currentAnim = animToPlay;
        switch (animToPlay)
        {
            case "idle":
                FindObjectOfType<AsaAnimationManager>().Idle();
                break;
            case "pleased":
                FindObjectOfType<AsaAnimationManager>().Pleased();
                break;
            case "dissapointed":
                FindObjectOfType<AsaAnimationManager>().Dissapointed();
                break;
            case "nya":
                FindObjectOfType<AsaAnimationManager>().Nya();
                break;
            case "teehee":
                FindObjectOfType<AsaAnimationManager>().TeeHee();
                break;
            case "heartfelt":
                FindObjectOfType<AsaAnimationManager>().Heartfelt();
                break;
            case "dejected":
                FindObjectOfType<AsaAnimationManager>().Dejected();
                break;
            case "lean forward":
                FindObjectOfType<AsaAnimationManager>().LeanForward();
                break;
            case "think":
                FindObjectOfType<AsaAnimationManager>().Think();
                break;
            case "angry":
                FindObjectOfType<AsaAnimationManager>().Angry();
                break;
            default:
                // code block
                break;
        }
    }

    //displays the next sentence by creating a queue
    public void DisplayNextSentence()
    {
        UnityEngine.Debug.Log("In DisplayNextSentence()");
        if (sentences.Count == 0)
        {
            if (responses.Count != 0)
            {
                CheckResponses();
            }
            else
            {
                responseGiven = 0;
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
        UnityEngine.Debug.Log("In TypeSentence()");
        dialogueText.text = "";
        int charsDisplayed = 0;
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            charsDisplayed++;
            if (charsDisplayed % 3 == 0)
            {
                yield return null;
            }
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
            AnimationQueue();
        }
    }
}
