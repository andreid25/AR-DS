using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class AR_Asa_UI : MonoBehaviour
{
    [SerializeField] private CanvasGroup blackBG, startText, startButton, startButtonText, failText;
    [SerializeField] private CanvasGroup completeText, youGotText, rewardImage, rewardName, mainMenuButton, mainMenuButtonText;
    [SerializeField] private Button startButtonEnabler, mainMenuButtonEnabler;
    [SerializeField] private GameObject aRScriptObject;

    void Awake()
    {
        blackBG.alpha = 0;
        startText.alpha = 0;
        startButton.alpha = 0;
        startButtonText.alpha = 0;
        failText.alpha = 0;
        startButtonEnabler.interactable = false;

        completeText.alpha = 0;
        youGotText.alpha = 0;
        rewardImage.alpha = 0;
        rewardName.alpha = 0;
        mainMenuButton.alpha = 0;
        mainMenuButtonText.alpha = 0;
        mainMenuButtonEnabler.interactable = false;
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
        yield return new WaitForSeconds(1.0f);
        startButtonEnabler.interactable = true;
    }
    public void StartButton()
    {
        blackBG.DOFade(0, 1.0f);
        startText.DOFade(0, 1.0f);
        startButton.DOFade(0, 1.0f);
        startButtonText.DOFade(0, 1.0f);
        startButtonEnabler.interactable = false;
        aRScriptObject.GetComponent<Place_Asa>().PlaceAsaAllowed();
    }

    public void PlaceFail()
    {
        StartCoroutine(CoPlaceFail());
    }
    private IEnumerator CoPlaceFail()
    {
        failText.DOFade(1, .2f);

        yield return new WaitForSeconds(2f);

        failText.DOFade(0, 1.0f);
    }

    public void DateEnd(int rewardID)
    {
        StartCoroutine(CoDateEnd());
        blackBG.DOFade(1, 1.5f);
        youGotText.DOFade(1, 1.5f);
        rewardImage.DOFade(1, 1.5f);
        completeText.DOFade(1, 1.5f);
        rewardName.DOFade(1, 1.5f);
        mainMenuButton.DOFade(1, 1.5f);
        mainMenuButtonText.DOFade(1, 1.5f);
    }
    private IEnumerator CoDateEnd()
    {
        yield return new WaitForSeconds(1.5f);
        mainMenuButtonEnabler.interactable = true;
    }

    public void MainMenuReturn()
    {
        Loader.Load(Loader.Scene.PhoneMenus);
    }

}
