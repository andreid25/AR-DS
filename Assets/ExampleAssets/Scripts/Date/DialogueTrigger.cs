using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //public Dialogue dialogue;

    public void TriggerDialogue()
    {
        string[] dialogue = {"Hello sexy!",
            "Gotta say you looking fine as hell today.",
            "Tee-hee :3" };

        FindObjectOfType<Date_Dialogue_Manager>().StartDialogue(dialogue);
        //string[] sentences;
    }

        
}
