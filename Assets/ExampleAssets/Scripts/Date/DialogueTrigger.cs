using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private string dialogueSet;
    private int setPart;

    private List<string> dialogue = new List<string>();
    private List<string> anims = new List<string>();
    private List<string> options = new List<string>();

    private void Awake()
    {
        dialogueSet = "StartDate";
        setPart = 1;
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
                        break;
                    case 2:
                        dialogue.Add("I'm glad to hear that!");
                        anims.Add("pleased");
                        break;
                    case 3:
                        dialogue.Add("Hehe, I'm very excited for this too!");
                        anims.Add("pleased");
                        dialogue.Add("Though I am also a bit nervous.");
                        anims.Add("pleased");
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
                dialogueSet = "Skipping";

                switch (responseGiven)
                {
                    case 1:
                        dialogue.Add("Am I that boring? Well sorryyyyy");
                        anims.Add("dissapointed");
                        break;
                    case 2:
                        dialogue.Add("Aww thanks!");
                        anims.Add("pleased");
                        break;
                    case 3:
                        dialogue.Add("Hehe you jokester!");
                        anims.Add("pleased");
                        break;
                }
                dialogue.Add("The person who designed me worked very hard to make sure I look cute.");
                anims.Add("idle");
                dialogue.Add("I'm not very good at walking and talking at the same time, so please slow down when I want to say something.");
                anims.Add("idle");
                dialogue.Add("Well, lets get walking!");
                anims.Add("idle");
                options.Add("Heck yeah!");
                FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options, anims);
            }
            
        }
        else if (dialogueSet == "Skipping")
        {
            if (setPart == 1)
            {
                FindObjectOfType<AR_Asa_UI>().WalkingInstructions();
            }
        }
        else if (dialogueSet == "EndDate")
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

    public void SkipEnd()
    {
        setPart = 1;
        dialogueSet = "EndDate";
        StartCoroutine(CoSkipEnd());
    }
    private IEnumerator CoSkipEnd()
    {
        yield return new WaitForSeconds(3f);
        ChooseNextSetPart(0);
    }

}
