using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class Phone_Menus : MonoBehaviour
{
    [SerializeField] private Image batteryOutside, batteryInside, startDateButton;
    [SerializeField] private TMP_Text time, collectionHeader;
    [SerializeField] private GameObject phoneBG, bottomBar, dateBG, settingsOptions, settingsBG, topBar, credits, backButton, plushText, noItemsText;

    [SerializeField] private GameObject soundsOption, notificationsOption, maleOption, femaleOption, enbyOption; //settings options

    private bool soundsOn, notificationsOn, maleOn, femaleOn, enbyOn;

    void Awake()
    {
        soundsOn = true;
        notificationsOn = true;
        maleOn = true;
        femaleOn = true;

        //TEST CODE delete later

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
        bottomBar.SetActive(true);
        topBar.SetActive(false);
        backButton.SetActive(false);

        //initialize phone menu
        phoneBG.SetActive(true);

        //date screen
        dateBG.SetActive(false);

        //texts
        time.enabled = true;
        collectionHeader.enabled = false;
        noItemsText.SetActive(false);

        settingsOptions.SetActive(false);
        settingsBG.SetActive(false);
        credits.SetActive(false);

        plushText.SetActive(false);

        FindObjectOfType<AsaPlush>().Reset();
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
        topBar.SetActive(false);
        backButton.SetActive(true);

        //initialize phone menu
        phoneBG.SetActive(false);

        //date screen
        dateBG.SetActive(true);
    }
    public void CollectionMenu()
    {
        if (!(FindObjectOfType<GlobalData>().CollectionActive()))
        {
            noItemsText.SetActive(true);
        }
        //initialize perma UI
        topBar.SetActive(true);
        backButton.SetActive(true);

        //initialize phone menu
        phoneBG.SetActive(false);

        collectionHeader.enabled = true;
    }
    public void Settings()
    {
        //initialize perma UI
        topBar.SetActive(true);
        backButton.SetActive(true);

        //initialize phone menu
        phoneBG.SetActive(false);


        settingsOptions.SetActive(true);
        settingsBG.SetActive(true);
    }
    public void Credits()
    {
        //initialize perma UI
        topBar.SetActive(true);
        backButton.SetActive(true);

        //initialize phone menu
        phoneBG.SetActive(false);
        collectionHeader.enabled = false;

        settingsOptions.SetActive(false);
        credits.SetActive(true);
    }

    public void CollectionObjectShow(string objID)
    {
        if (objID == "plush")
        {
            plushText.SetActive(true);
        }
    }
    public void CollectionObjectBack(string objID)
    {
        if (objID == "plush")
        {
            plushText.SetActive(false);
        }
    }

    public void SoundsButton()
    {
        if (soundsOn)
        {
            soundsOn = false;
            soundsOption.SetActive(false);
        }
        else
        {
            soundsOn = true;
            soundsOption.SetActive(true);
        }
    }
    public void NotificationsButton()
    {
        if (notificationsOn)
        {
            notificationsOn = false;
            notificationsOption.SetActive(false);
        }
        else
        {
            notificationsOn = true;
            notificationsOption.SetActive(true);
        }
    }
    public void HeHimButton()
    {
        if (maleOn)
        {
            maleOn = false;
            maleOption.SetActive(false);
        }
        else
        {
            maleOn = true;
            maleOption.SetActive(true);
        }
    }
    public void SheHerButton()
    {
        if (femaleOn)
        {
            femaleOn = false;
            femaleOption.SetActive(false);
        }
        else
        {
            femaleOn = true;
            femaleOption.SetActive(true);
        }
    }
    public void TheyThemButton()
    {
        if (enbyOn)
        {
            enbyOn = false;
            enbyOption.SetActive(false);
        }
        else
        {
            enbyOn = true;
            enbyOption.SetActive(true);
        }
    }

    public void StartDate()
    {
        Loader.Load(Loader.Scene.Date);
    }
}
