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
    [SerializeField] private TMP_Text time;
    [SerializeField] private GameObject phoneBG, bottomBar, dateBG, settingsOptions, settingsBG, topBar, credits, creditsTwo, plushText, earsText, noItemsText;

    [SerializeField] private GameObject soundsOption, notificationsOption, maleOption, femaleOption, enbyOption;
    [SerializeField] private GameObject dateIntroScreen, collectionIntroScreen; //intro screens
    [SerializeField] private AudioSource click;

    private bool soundsOn, notificationsOn, maleOn, femaleOn, enbyOn;

    void Awake()
    {
        soundsOn = true;
        notificationsOn = true;
        maleOn = true;
        femaleOn = true;
        enbyOn = true;

        //TEST CODE delete later
        //FindObjectOfType<GlobalData>().PlushAcquired();
        //FindObjectOfType<GlobalData>().CatEarsAcquired();
    }
    void Start()
    {
        StopAllCoroutines();
        batteryOutside.enabled = true;
        batteryInside.enabled = true;
        bottomBar.SetActive(true);
        topBar.SetActive(false);

        //initialize phone menu
        phoneBG.SetActive(true);

        //date screen
        dateBG.SetActive(false);

        //texts
        time.enabled = true;
        noItemsText.SetActive(false);

        settingsOptions.SetActive(false);
        settingsBG.SetActive(false);
        credits.SetActive(false);
        creditsTwo.SetActive(false);

        plushText.SetActive(false);
        earsText.SetActive(false);

        dateIntroScreen.SetActive(false);
        collectionIntroScreen.SetActive(false);

        FindObjectOfType<CatEars>().Reset();
        FindObjectOfType<AsaPlush>().Reset();
    }

    public void StartMenu()
    {
        click.Play();
        Start();
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
        click.Play();
        //initialize perma UI
        dateIntroScreen.SetActive(true);
        StartCoroutine(CoDateMenu());

    }
    IEnumerator CoDateMenu()
    {
        yield return new WaitForSeconds(.6f);
        dateIntroScreen.SetActive(false);
        //initialize phone menu
        phoneBG.SetActive(false);

        //date screen
        dateBG.SetActive(true);
    }
    public void CollectionMenu()
    {
        collectionIntroScreen.SetActive(true);
        click.Play();
        StartCoroutine(CoCollectionMenu());
        
    }
    IEnumerator CoCollectionMenu()
    {
        yield return new WaitForSeconds(.6f);
        collectionIntroScreen.SetActive(false);
        if (!(FindObjectOfType<GlobalData>().CollectionActive()))
        {
            noItemsText.SetActive(true);
        }
        //initialize perma UI
        topBar.SetActive(true);

        //initialize phone menu
        phoneBG.SetActive(false);
    }
    public void SettingsApp()
    {
        settingsBG.SetActive(true);
        StartCoroutine(CoSettingsApp());
    }
    IEnumerator CoSettingsApp()
    {
        yield return new WaitForSeconds(.6f);
        Settings();
    }
    public void Settings()
    {
        click.Play();
        //initialize perma UI
        topBar.SetActive(true);

        //initialize phone menu
        phoneBG.SetActive(false);

        credits.SetActive(false);
        creditsTwo.SetActive(false);
        settingsOptions.SetActive(true);
        settingsBG.SetActive(true);
    }
    public void Credits()
    {
        click.Play();
        //initialize perma UI
        topBar.SetActive(true);

        //initialize phone menu
        phoneBG.SetActive(false);

        settingsOptions.SetActive(false);
        credits.SetActive(true);
        creditsTwo.SetActive(false);
    }
    public void CreditsTwo()
    {
        click.Play();
        //initialize perma UI
        topBar.SetActive(true);

        //initialize phone menu
        phoneBG.SetActive(false);

        settingsOptions.SetActive(false);
        credits.SetActive(false);
        creditsTwo.SetActive(true);
    }

    public void CollectionObjectShow(string objID)
    {
        click.Play();
        if (objID == "plush")
        {
            plushText.SetActive(true);
            earsText.SetActive(false);
            FindObjectOfType<CatEars>().Reset();
        }
        else if (objID == "ears")
        {
            earsText.SetActive(true);
            plushText.SetActive(false);
            FindObjectOfType<AsaPlush>().Reset();
        }
    }
    public void CollectionObjectBack(string objID)
    {
        click.Play();
        if (objID == "plush")
        {
            plushText.SetActive(false);
        }
        else if (objID == "ears")
        {
            earsText.SetActive(false);
        }
    }

    public void SoundsButton()
    {
        click.Play();
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
        click.Play();
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
        click.Play();
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
        click.Play();
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
        click.Play();
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
        click.Play();
        Loader.Load(Loader.Scene.Date);
    }
    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            Loader.Load(Loader.Scene.PhonePickup);
        }
    }
}
