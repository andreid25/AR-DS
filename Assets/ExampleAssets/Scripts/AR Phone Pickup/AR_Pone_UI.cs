using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class AR_Pone_UI : MonoBehaviour //TODO: Test instructions button
{
    [SerializeField] private Button startButtonEnabler, instructionsButtonEnabler;
    [SerializeField] private CanvasGroup blackBG, startButton, startButtonText, failText, startText, instructionsButton;
    [SerializeField] private GameObject aRScriptObject;

    bool firstTimeInstructions, instructionsUp;

    void Awake()
    {
        startButtonEnabler.interactable = false;
        instructionsButtonEnabler.interactable = false;
        blackBG.alpha = 0;
        startText.alpha = 0;
        failText.alpha = 0;
        startButton.alpha = 0;
        startButtonText.alpha = 0;
        instructionsButton.alpha = 0;
        firstTimeInstructions = true;
        instructionsUp = true;

        FindObjectOfType<GlobalData>().Reset();
        
    }
    private void Start()
    {
        StartCoroutine(CoStart());
        blackBG.DOFade(1, 1.5f);
        startText.DOFade(1, 1.5f);
        startButton.DOFade(1, 1.5f);
        startButtonText.DOFade(1, 1.5f);
    }
    private IEnumerator CoStart()
    {
        yield return new WaitForSeconds(3.5f);
        StartAR();
    }

    public void StartAR()
    {
        blackBG.DOFade(0, 1.0f);
        startText.DOFade(0, 1.0f);
        startButton.DOFade(0, 1.0f);
        startButtonText.DOFade(0, 1.0f);
        instructionsButton.DOFade(1, .5f);
        startButtonEnabler.interactable = false;
        instructionsButtonEnabler.interactable = true;
        aRScriptObject.GetComponent<Place_Phone>().PlacePhoneAllowed();
        
    }
    public void HideInstructionsButton()
    {
        instructionsButtonEnabler.interactable = false;
        instructionsButton.DOFade(0, .1f);
    }
    public void Instructions()
    {
        StartCoroutine(CoStart());
        blackBG.DOFade(1, .5f);
        startText.DOFade(1, .5f);
        startButton.DOFade(1, .5f);
        startButtonText.DOFade(1, .5f);
        instructionsButton.DOFade(0, .5f);
        instructionsUp = true;
        FindObjectOfType<Place_Phone>().DisablePhonePlace();
    }

    // Update is called once per frame
    public void PlaceFail()
    {
        StartCoroutine(CoPlaceFail());
    }
    private IEnumerator CoPlaceFail()
    {
        if (!instructionsUp)
        {
            failText.DOFade(1, .2f);

            yield return new WaitForSeconds(2f);

            failText.DOFade(0, 1.0f);
        }
    }
    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            Loader.Load(Loader.Scene.PhonePickup);
        }
    }
}
