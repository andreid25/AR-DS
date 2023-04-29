using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private string dialogueSet, dialogueBranch;
    private int setPart;
    public float happiness; //0 is miserable and they will leave, 0-3 is bad and will leave unhappy, 4-7 is neutral and 9 or more is happy

    private List<string> dialogue = new List<string>();
    private List<string> anims = new List<string>();
    private List<string> options = new List<string>();

    private List<string> topics = new List<string>();
    private bool capstoneDiscussed = false;

    private void Awake()
    {
        dialogueSet = "StartDate";
        dialogueBranch = "";
        setPart = 1;
        happiness = 5f;

        //initailize skip dialogue topics
        topics.Add("TypesOfArt");
        topics.Add("Nya");
        topics.Add("DrawingInspiration");
        topics.Add("TimeOfDay");
        topics.Add("Exams");
    }
    private void ChooseNextSetPart(int responseGiven)
    {
        dialogue.Clear();
        options.Clear();
        anims.Clear();
        if (dialogueSet == "StartDate")
        {
            if(setPart == 1)
            {
                setPart = 2;
                switch (responseGiven)
                {
                    case 1:
                        dialogue.Add("Eh? What's with that attitude?");
                        anims.Add("dissapointed");
                        dialogue.Add("...");
                        anims.Add("dissapointed");
                        happiness--;
                        break;
                    case 2:
                        dialogue.Add("I'm glad to hear that!");
                        anims.Add("pleased");
                        happiness += .5f;
                        break;
                    case 3:
                        dialogue.Add("Hehe, I'm very excited for this too!");
                        anims.Add("pleased");
                        dialogue.Add("Though I am also a bit nervous.");
                        anims.Add("pleased");
                        happiness++;
                        break;
                }
                dialogue.Add("I have to say, you look really cute today... hehe.");
                anims.Add("idle");

                options.Add("I'm bored.");
                options.Add("You're looking even cuter Asa.");
                options.Add("I know, but so are you.");
                FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
            }
            else if (setPart == 2)
            {
                setPart = 1;
                dialogueBranch = "Start";
                dialogueSet = "Skipping";

                switch (responseGiven)
                {
                    case 1:
                        dialogue.Add("...");
                        anims.Add("dejected");
                        dialogue.Add("Sorry for being boring.");
                        anims.Add("dejected");
                        happiness--;
                        break;
                    case 2:
                        dialogue.Add("Aww thanks!");
                        anims.Add("teehee");
                        dialogue.Add("The person who designed me worked very hard to make sure I look cute.");
                        anims.Add("teehee");
                        happiness++;
                        break;
                    case 3:
                        dialogue.Add("Hehe you jokester!");
                        anims.Add("teehee");
                        dialogue.Add("The person who designed me worked very hard to make sure I look cute.");
                        anims.Add("teehee");
                        happiness++;
                        break;
                }
                dialogue.Add("Well, I think it'd be fun to go on a walk together.");
                anims.Add("think");
                dialogue.Add("But walking makes me think, and whenever I think for a bit I always want to say something.");
                anims.Add("think");
                dialogue.Add("I'm not very good at walking and talking at the same time, so please slow down when I want to say something.");
                anims.Add("think");
                dialogue.Add("Well, lets get walking!");
                anims.Add("idle");
                options.Add("Heck yeah!");
                FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
            }
            
        }
        else if (dialogueSet == "Skipping")
        {
            switch (dialogueBranch)
            {
                case "Start":
                    if (setPart == 1)
                    {
                        FindObjectOfType<AR_Asa_UI>().WalkingInstructions();
                    }
                    else if (setPart == 2)
                    {
                        setPart = 1;
                        StartCoroutine(RandomSkippingDialogue());
                    }
                    break;
                case "Capstone":
                    //TODO: finish this dialogue piece
                    if (setPart == 1)
                    {
                        FindObjectOfType<AsaAnimationManager>().SkippingStop();
                        setPart = 2;
                        UnityEngine.Debug.Log("In Capstone Dialogue");

                        dialogue.Add("I see you're at the capstone fair right now.");
                        anims.Add("idle");
                        dialogue.Add("Thanks for checking out the project for this long!");
                        anims.Add("heartfelt");
                        dialogue.Add("The person who made this worked really hard on this project!");
                        anims.Add("heartfelt");
                        dialogue.Add("Well, what do you think of it?");
                        anims.Add("lean forward");

                        options.Add("It's kinda lame.");
                        options.Add("It's fine.");
                        options.Add("I think it's awesome!");
                        FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
                    }
                    else if (setPart == 2)
                    {
                        setPart = 1;
                        dialogueBranch = "Return";
                        switch (responseGiven)
                        {
                            case 1:
                                dialogue.Add("LAME?!");
                                anims.Add("angry"); 
                                dialogue.Add("I hope you were joking...");
                                anims.Add("angry"); 
                                dialogue.Add("Even so that was a bad one!");
                                anims.Add("angry"); 
                                happiness--;
                                break;
                            case 2:
                                dialogue.Add("Fine? Well, I'll take that as a compliment!");
                                anims.Add("lean forward");
                                break;
                            case 3:
                                dialogue.Add("Yeah! It really is awesome!");
                                anims.Add("teehee"); //blush
                                dialogue.Add("You should make sure to congratulate the creator when you get the chance.");
                                anims.Add("idle"); //blush
                                happiness++;
                                break;
                        }
                        FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
                    }
                    break;
                case "Return":
                    setPart = 1;
                    StartCoroutine(RandomSkippingDialogue());
                    FindObjectOfType<AsaAnimationManager>().SkippingStart();
                    break;
            }

            //TODO: add rest of dialogue branches
        }
        else if (dialogueSet == "EndDate")
        {
            //TODO: add new happy branch
            if (dialogueBranch == "Neutral")
            {
                if (setPart == 1)
                {
                    setPart = 2;
                    dialogue.Add("That was fun! You were a pretty nice person to talk to.");
                    dialogue.Add("I'll be going on my way so see you around some time!");
                    anims.Add("ignore");
                    options.Add("See you around Asa!");
                    options.Add("Bye I guess.");
                    FindObjectOfType<AsaAnimationManager>().NeutralExit();
                    FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
                }
                else if (setPart == 2)
                {
                    setPart = 3;
                    dialogue.Add("Bye bye!");
                    anims.Add("ignore");
                    options.Add("Well that went okay.");
                    FindObjectOfType<AsaAnimationManager>().NeutralExitTwo();
                    FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
                }
                else if (setPart == 3)
                {
                    FindObjectOfType<AR_Asa_UI>().DateEnd(1);
                }
            }
            else if (dialogueBranch == "Sad")
            {
                if (setPart == 1)
                {
                    setPart = 2;
                    dialogue.Add("...");
                    dialogue.Add("You’ve been really mean to me throughout our date.");
                    dialogue.Add("I’m not having much fun… I think it’s best if we part ways.");
                    anims.Add("ignore");
                    options.Add("I’m sorry Asa I didn’t mean it.");
                    options.Add("Whatever.");
                    options.Add("Okay.");
                    FindObjectOfType<AsaAnimationManager>().SadExit();
                    FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
                }
                else if (setPart == 2)
                {
                    setPart = 3;
                    dialogue.Add("...");
                    if (responseGiven == 1)
                    {
                        dialogue.Add("If you really mean that… we might be able to meet again in the future.");
                        dialogue.Add("But not now.");
                    }
                    anims.Add("ignore");
                    FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
                }
                else if (setPart == 3)
                {
                    setPart = 4;
                    dialogue.Add("Goodbye.");
                    anims.Add("ignore");
                    options.Add("...");
                    FindObjectOfType<AsaAnimationManager>().SadExitTwo();
                    FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
                }
                else if (setPart == 4)
                {
                    FindObjectOfType<AR_Asa_UI>().DateEnd(1);
                }
            }
            else
            {
                if (setPart == 1)
                {
                    setPart = 2;
                    StartCoroutine(GiveYouItem());
                }
                else if (setPart == 2)
                {
                    setPart = 3;
                    dialogue.Add("Tee-hee... okgottarunbyeeeeeeeeeee");
                    anims.Add("ignore");
                    options.Add("Huh?");
                    FindObjectOfType<AsaAnimationManager>().ItemTakeRun();
                    FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
                }
                else if (setPart == 3)
                {
                    FindObjectOfType<GlobalData>().PlushAcquired();
                    FindObjectOfType<AR_Asa_UI>().DateEnd(1);
                }
            }
        }
    }
    private IEnumerator RandomSkippingDialogue()
    {
        FindObjectOfType<AR_Asa_UI>().Skipping();
        yield return new WaitForSeconds(UnityEngine.Random.Range(12.0f, 20.0f));
        //TODO: add at least 10 branches
        if (!capstoneDiscussed)
        {
            dialogueBranch = "Capstone";
            setPart = 1;
            capstoneDiscussed = true;

            ChooseNextSetPart(0);
            FindObjectOfType<AR_Asa_UI>().SkippingConversationStart();
        }
        else
        {
            if (topics.Count > 0)
            {
                int randomChooser = UnityEngine.Random.Range(0, topics.Count);
                dialogueBranch = topics[randomChooser];
                topics.RemoveAt(randomChooser);
                setPart = 1;

                ChooseNextSetPart(0);
                FindObjectOfType<AR_Asa_UI>().SkippingConversationStart();
            }
        }
    }
    private IEnumerator GiveYouItem()
    {

        FindObjectOfType<AsaAnimationManager>().GiveYouRun();
        yield return new WaitForSeconds(3f);
        dialogue.Add("I have something for you... just take it it's a bit embarassing but I made it myself...");
        anims.Add("ignore");
        options.Add("This is adorable! Thanks! (Take the item)");
        options.Add("Sure I'll take it. (Take the item)");
        FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
    }

    public void StartDateDialogue1()
    {
        UnityEngine.Debug.Log("In StartDateDialogue");
        dialogue.Clear();
        options.Clear();
        anims.Clear();

        dialogue.Add("Hiii!!!");
        anims.Add("idle");
        dialogue.Add("It's so nice being able to finally see you!");
        anims.Add("idle");

        options.Add("Yeah, whatever.");
        options.Add("Glad to see you too!");
        options.Add("I'm excited to hang out with you Asa!");

        FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
    }

    public void EndOfDialogue(int responseGiven)
    {
        UnityEngine.Debug.Log("Response picked: " + responseGiven);
        ChooseNextSetPart(responseGiven);
    }

    public void StartSkip()
    {
        setPart = 2;
        ChooseNextSetPart(0);
    }

    public void SkipEnd()
    {
        StopAllCoroutines();
        setPart = 1;
        dialogueSet = "EndDate";
        if (happiness > 3 && happiness < 7)
        {
            dialogueBranch = "Neutral";
        }
        else if (happiness <= 3)
        {
            dialogueBranch = "Sad";
        }
        else
        {
            dialogueBranch = "Happy";
        }
        StartCoroutine(CoSkipEnd());
    }
    private IEnumerator CoSkipEnd()
    {
        yield return new WaitForSeconds(3f);
        ChooseNextSetPart(0);
    }

}
