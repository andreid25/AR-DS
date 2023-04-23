using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class Phone_Menus : MonoBehaviour
{
    [SerializeField] private Image phoneBG, bottomBar, homeButton, dateButton, collectionButton, settingsButton, dateBG, backButton, dateHeart;
    [SerializeField] private Image batteryOutside, batteryInside, startDateButton;
    [SerializeField] private TMP_Text time, dateText, collectionText, settingsText, dateHereText, startDateText, collectionHeader;

    void Awake()
    {
    }
    void Start()
    {
        StartMenu();
    }

    public void StartMenu()
    {
        //initialize perma UI
        batteryOutside.enabled = true;
        batteryInside.enabled = true;
        bottomBar.enabled = true;
        homeButton.enabled = true;

        //initialize phone menu
        phoneBG.enabled = true;
        dateButton.enabled = true;
        collectionButton.enabled = true;
        settingsButton.enabled = true;

        //date screen
        dateBG.enabled = false;
        backButton.enabled = false;
        dateHeart.enabled = false;
        startDateButton.enabled = false;

        //texts
        time.enabled = true;
        dateText.enabled = true;
        collectionText.enabled = true;
        settingsText.enabled = true;
        dateHereText.enabled = false;
        startDateText.enabled = false;
        collectionHeader.enabled = false;

        FindObjectOfType<Asa_Plush>().Reset();
    }
    
    /*private IEnumerator Start()
    {
    }*/

    // Update is called once per frame
    void Update()
    {
        if (DateTime.Now.Minute < 10)
        {
            time.text = DateTime.Now.Hour + ":0" + DateTime.Now.Minute;
        }
        else
        {
            time.text = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
        }
        var theBarRectTransform = batteryInside.transform as RectTransform;
        theBarRectTransform.sizeDelta = new Vector2((SystemInfo.batteryLevel * 100), theBarRectTransform.sizeDelta.y);
    }

    public void DateMenu()
    {
        //initialize perma UI
        batteryOutside.enabled = true;
        batteryInside.enabled = true;
        bottomBar.enabled = true;
        homeButton.enabled = true;

        //initialize phone menu
        phoneBG.enabled = false;
        dateButton.enabled = false;
        collectionButton.enabled = false;
        settingsButton.enabled = false;

        //date screen
        dateBG.enabled = true;
        backButton.enabled = true;
        dateHeart.enabled = true;
        startDateButton.enabled = true;

        //texts
        time.enabled = true;
        dateText.enabled = false;
        collectionText.enabled = false;
        settingsText.enabled = false;
        dateHereText.enabled = true;
        startDateText.enabled = true;
        collectionHeader.enabled = false;
    }
    public void CollectionMenu()
    {
        //initialize perma UI
        batteryOutside.enabled = true;
        batteryInside.enabled = true;
        bottomBar.enabled = true;
        homeButton.enabled = true;

        //initialize phone menu
        phoneBG.enabled = false;
        dateButton.enabled = false;
        collectionButton.enabled = false;
        settingsButton.enabled = false;

        //date screen
        dateBG.enabled = false;
        backButton.enabled = true;
        dateHeart.enabled = false;
        startDateButton.enabled = false;

        //texts
        time.enabled = true;
        dateText.enabled = false;
        collectionText.enabled = false;
        settingsText.enabled = false;
        dateHereText.enabled = false;
        startDateText.enabled = false;
        collectionHeader.enabled = true;
    }
    public void StartDate()
    {
        Loader.Load(Loader.Scene.Date);
    }
}
