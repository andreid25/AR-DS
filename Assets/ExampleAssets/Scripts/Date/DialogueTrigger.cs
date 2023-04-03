using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private string dialogueSet;
    private int setPart;

    private List<string> dialogue = new List<string>();
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
        if (dialogueSet == "StartDate")
        {
            if(setPart == 1)
            {
                if (responseGiven == 1)
                {
                    dialogue.Add("Eh? What's with that attitude?");
                    dialogue.Add("...");
                }
                else if (responseGiven == 2)
                {
                    dialogue.Add("I'm glad to hear that!");
                }
                else if (responseGiven == 3)
                {
                    dialogue.Add("Hehe, I'm very excited for this too!");
                    dialogue.Add("Though I am also a bit nervous.");
                }
                dialogue.Add("I have to say, you look really cute today... hehe.");
                setPart = 2;

                options.Add("You're looking even cuter Asa.");
                options.Add("I know, but so are you.");
                FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options);
                FindObjectOfType<AsaAnimationManager>().Pleased();
            }
            else if (setPart == 2)
            {
                setPart = 3;
                if (responseGiven == 1)
                {
                    dialogue.Add("Aww thanks!");
                }
                else if (responseGiven == 2)
                {
                    dialogue.Add("Hehe you jokester!");
                }
                dialogue.Add("The person who designed me worked very hard to make sure I look cute.");
                dialogue.Add("Well, this would be the part where we walk together but that hasn't been programmed yet!");
                options.Add("Bummer.");
                FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options);
                FindObjectOfType<AsaAnimationManager>().Pleased();
            }
        }
    }

    public void StartDateDialogue1()
    {
        dialogue.Clear();
        options.Clear();

        dialogue.Add("Hiii!!!");
        dialogue.Add("It's so nice being able to finally see you!");

        options.Add("Yeah, whatever.");
        options.Add("Glad to see you too!");
        options.Add("I'm excited to hang out with you Asa!");

        FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue, options);
    }

    public void EndOfDialogue(int responseGiven)
    {
        UnityEngine.Debug.Log("Response picked: " + responseGiven);
        ChooseNextSetPart(responseGiven);
    }


}
