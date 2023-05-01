using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private string dialogueSet, dialogueBranch;
    private int setPart;
    public float happiness; //0 is miserable and they will leave, 0-3 is bad and will leave unhappy, 4-7 is neutral and 9 or more is happy
    private bool sad = false;

    private List<string> dialogue = new List<string>();
    private List<string> anims = new List<string>();
    private List<string> options = new List<string>();

    //private List<string> topics = new List<string>();
    private int topicsDiscussed = 0;

    private void Awake()
    {
        dialogueSet = "StartDate";
        dialogueBranch = "";
        setPart = 1;
        happiness = 5f;

        //initailize skip dialogue topics
        /*topics.Add("TypesOfArt");
        topics.Add("Nya");
        topics.Add("DrawingInspiration");
        topics.Add("TimeOfDay");
        topics.Add("Exams");*/
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
                        dialogue.Add("Let's just pretend you didn't say that.");
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
                        sad = true;
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
                    if (setPart == 1)
                    {
                        sad = false;
                        FindObjectOfType<AsaAnimationManager>().SkippingStop();
                        setPart = 2;
                        UnityEngine.Debug.Log("In Capstone Dialogue");

                        dialogue.Add("I see you're at the capstone fair right now.");
                        anims.Add("idle");
                        dialogue.Add("Thanks for checking out the project!");
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
                                sad = true;
                                happiness--;
                                break;
                            case 2:
                                dialogue.Add("Fine? Well, I'll take that as a compliment!");
                                anims.Add("lean forward");
                                break;
                            case 3:
                                dialogue.Add("Yeah! It really is awesome!");
                                anims.Add("blush"); //something here is wrong
                                dialogue.Add("You should make sure to congratulate the creator when you get the chance.");
                                anims.Add("blush");
                                happiness++;
                                break;
                        }
                        FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
                    }
                    break;
                case "Nya":
                    if (setPart == 1)
                    {
                        sad = false;
                        FindObjectOfType<AsaAnimationManager>().SkippingStop();
                        setPart = 2;
                        UnityEngine.Debug.Log("In Nya Dialogue");

                        dialogue.Add("You know, sometimes I like to think I’m a cat…");
                        anims.Add("think");

                        options.Add("How come?");
                        FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
                    }
                    else if (setPart == 2)
                    {
                        setPart = 3;

                        dialogue.Add("Well I sleep a lot, get cranky, and sometimes I like to meow.");
                        anims.Add("pointpoint");

                        options.Add("That's pathetic.");
                        options.Add("I get it. Sleeping is super nice!");
                        if (happiness >= 7)
                        {
                            options.Add("Meow? Show me.");
                        }
                        FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
                    }
                    else if (setPart == 3)
                    {
                        switch (responseGiven)
                        {
                            case 1:
                                setPart = 1;
                                dialogueBranch = "Return";
                                dialogue.Add("Hey... it's only somewhat pathetic...");
                                anims.Add("dissapointed");
                                dialogue.Add("It's just who I am though!");
                                anims.Add("idle");
                                sad = true;
                                happiness--;
                                break;
                            case 2:
                                setPart = 4;
                                dialogue.Add("Yeah! I sleep around 10 hours a day and I still end up tired!");
                                anims.Add("pointpoint");
                                options.Add("Not sure whether I should be impressed or concerned.");
                                happiness++;
                                break;
                            case 3:
                                setPart = 5;
                                dialogue.Add("Oh?");
                                anims.Add("pointpoint");
                                dialogue.Add("You really want to see me meow?");
                                anims.Add("teehee");
                                dialogue.Add("I can show you since you've been so nice to me!");
                                anims.Add("teehee");
                                options.Add("Actually nevermind."); //Test this option
                                options.Add("Why not?");
                                break;
                        }
                        FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
                    }
                    else if (setPart == 4)
                    {
                        setPart = 1;
                        dialogueBranch = "Return";

                        dialogue.Add("Probably concerned.");
                        anims.Add("teehee");
                        FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
                    }
                    else if (setPart == 5)
                    {
                        switch (responseGiven)
                        {
                            case 1:
                                setPart = 1;
                                dialogueBranch = "Return"; //idk if this will work
                                break;
                            case 2:
                                setPart = 6;
                                dialogue.Add("Nyaaa~");
                                anims.Add("nya");
                                dialogue.Add("...");
                                anims.Add("nya");
                                dialogue.Add("...");
                                anims.Add("nya");
                                dialogue.Add("...");
                                anims.Add("nya");
                                dialogue.Add("You’re so weird for wanting this!!");
                                anims.Add("embarrassed");
                                dialogue.Add("I can't believe you made me do that!");
                                anims.Add("embarrassed");

                                options.Add("You're the one who said you can meow!");
                                options.Add("Tee-hee");
                                FindObjectOfType<GlobalData>().CatEarsAcquired();
                                happiness++;
                                break;
                        }
                        FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
                    }
                    else if (setPart == 6)
                    {
                        setPart = 1;
                        dialogueBranch = "Return";
                        switch (responseGiven)
                        {
                            case 1:
                                dialogue.Add("I know... but you still egged me on!");
                                anims.Add("embarrassed");
                                break;
                            case 2:
                                dialogue.Add("I can’t believe you made me do something so embarrassing...");
                                anims.Add("embarrassed");
                                break;
                        }
                        dialogue.Add("I have to admit though... that was fun.");
                        anims.Add("embarrassed");
                        FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
                    }
                    break;
                case "Last Topic":
                    if (setPart == 1)
                    {
                        sad = false;
                        FindObjectOfType<AsaAnimationManager>().SkippingStop();
                        dialogueBranch = "Return";
                        UnityEngine.Debug.Log("In Last Topic");

                        dialogue.Add("Wow you've been playing this for quite a while.");
                        anims.Add("shocked");
                        dialogue.Add("To be honest, I didn't expect anyone to get this far through the walking dialogues!");
                        anims.Add("shocked");
                        dialogue.Add("There aren't any walking dialogues left after this.");
                        anims.Add("idle");
                        dialogue.Add("Of course there's still the ending of the date to check out!");
                        anims.Add("idle");
                        dialogue.Add("So as flattered as I am that you want to walk with me this much, definitely consider wrapping up soon.");
                        anims.Add("blush");
                        dialogue.Add("We want to give everyone else a chance to try the experience!");
                        anims.Add("idle");
                        dialogue.Add("Plus you should check out everyone else's projects too! They all worked really hard!");
                        anims.Add("heartfelt");
                        dialogue.Add("So I recommend hitting that \"Stop Walking\" button soon!");
                        anims.Add("idle");
                        FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
                    }
                    break;
                case "Art":
                    if (setPart == 1)
                    {
                        sad = false;
                        FindObjectOfType<AsaAnimationManager>().SkippingStop();
                        UnityEngine.Debug.Log("In Last Topic");
                        setPart = 2;

                        dialogue.Add("All this walking and sightseeing is making me think about stuff I should be drawing.");
                        anims.Add("idle");
                        dialogue.Add("I'm actually quite the art nerd hehe.");
                        anims.Add("lean forward");
                        dialogue.Add("I'm curious, do you prefer classical or modern art?");
                        anims.Add("lean forward");
                        options.Add("Art is a massive waste of time.");
                        options.Add("Modern art.");
                        options.Add("Classical art.");
                        FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
                    }
                    if (setPart == 2)
                    {
                        dialogueBranch = "Return";
                        setPart = 1;
                        
                        switch (responseGiven)
                        {
                            case 1:
                                dialogue.Add("Waste of time?!");
                                anims.Add("angry");
                                dialogue.Add("Well even if you think that, that doesn't change that I think it's cool!");
                                anims.Add("angry");
                                sad = true;
                                happiness--;
                                break;
                            case 2:
                                dialogue.Add("Ah modern art...");
                                anims.Add("think");
                                dialogue.Add("Actually, me too!");
                                anims.Add("pleased");
                                dialogue.Add("Although a lot of it is really gimmicky and lame, some of it is actually super creative!");
                                anims.Add("pleased");
                                dialogue.Add("Seeing artists step into new territory is always super cool.");
                                anims.Add("pleased");
                                happiness++;
                                break;
                            case 3:
                                dialogue.Add("Classical huh...");
                                anims.Add("think");
                                dialogue.Add("Yeah I get it. Painters from the past were insanely skilled!");
                                anims.Add("idle");
                                dialogue.Add("It still impresses me how realistic and detailed some old paintings are.");
                                anims.Add("idle");
                                dialogue.Add("But don't brush modern art off either! A lot of it is actually really creative.");
                                anims.Add("idle");
                                break;
                        }
                        FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
                    }
                    break;
                case "Return":
                    setPart = 1;
                    StartCoroutine(RandomSkippingDialogue());
                    FindObjectOfType<AsaAnimationManager>().SkippingStart(sad);
                    break;
            }

        }
        else if (dialogueSet == "EndDate")
        {
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
                    FindObjectOfType<AR_Asa_UI>().DateEnd();
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
                    FindObjectOfType<AR_Asa_UI>().DateEnd();
                }
            }
            else if (dialogueBranch == "Happy")
            {
                if (setPart == 1)
                {
                    setPart = 2;
                    dialogue.Add("Uuuuu... YAY THAT WAS SO MUCH FUN!");
                    dialogue.Add("I had such a great time! You were so much fun to talk to!");
                    dialogue.Add("You've made this such a great date!!");
                    anims.Add("ignore");
                    options.Add("You were great to talk to.");
                    options.Add("It was very fun!");
                    FindObjectOfType<AsaAnimationManager>().HappyExit();
                    FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
                }
                else if (setPart == 2)
                {
                    setPart = 3;
                    dialogue.Add("Ooh I have something for you!");
                    dialogue.Add("It's really embarassing... I knit it myself... Take it...");
                    anims.Add("ignore");
                    options.Add("This is adorable! Thanks Asa! (Take the item)");
                    options.Add("Sure I'll take it. (Take the item)");
                    FindObjectOfType<AsaAnimationManager>().HappyExitTwo();
                    FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
                    FindObjectOfType<GlobalData>().PlushAcquired();
                }
                else if (setPart == 3)
                {
                    setPart = 4;
                    dialogue.Add("Omg I'm so happy it's embarassing...");
                    dialogue.Add("Tee-hee...");
                    dialogue.Add("Hee-hee......");
                    dialogue.Add("okgottarunbyeeeeeeeeeee............");
                    anims.Add("ignore");
                    options.Add("Huh?");
                    FindObjectOfType<AsaAnimationManager>().HappyExitThree();
                    FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
                }
                else if (setPart == 4)
                {
                    FindObjectOfType<AR_Asa_UI>().DateEnd();
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
                    FindObjectOfType<AR_Asa_UI>().DateEnd();
                }
            }
        }
    }
    private IEnumerator RandomSkippingDialogue()
    {
        FindObjectOfType<AR_Asa_UI>().Skipping();
        yield return new WaitForSeconds(UnityEngine.Random.Range(12.0f, 16.0f));
        switch (topicsDiscussed)
        {
            case 0:
                dialogueBranch = "Capstone";
                setPart = 1;
                topicsDiscussed = 1;

                ChooseNextSetPart(0);
                FindObjectOfType<AR_Asa_UI>().SkippingConversationStart();
                break;
            case 1:
                dialogueBranch = "Nya";
                setPart = 1;
                topicsDiscussed = 2;

                ChooseNextSetPart(0);
                FindObjectOfType<AR_Asa_UI>().SkippingConversationStart();
                break;
            case 2:
                dialogueBranch = "Art";
                setPart = 1;
                topicsDiscussed = 3;

                ChooseNextSetPart(0);
                FindObjectOfType<AR_Asa_UI>().SkippingConversationStart();
                break;
            case 3:
                dialogueBranch = "Last Topic";
                setPart = 1;
                topicsDiscussed = 4;

                ChooseNextSetPart(0);
                FindObjectOfType<AR_Asa_UI>().SkippingConversationStart();
                break;
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
        FindObjectOfType<AsaAnimationManager>().SkippingStart(sad);
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
