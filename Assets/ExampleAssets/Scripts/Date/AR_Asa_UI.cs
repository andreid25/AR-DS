using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class AR_Asa_UI : MonoBehaviour
{
    [SerializeField] private CanvasGroup blackBG, startText, startButton, startButtonText, failText;
    [SerializeField] private Button startButtonEnabler;
    [SerializeField] private GameObject aRScriptObject;

    void Awake()
    {
        blackBG.alpha = 0;
        startText.alpha = 0;
        startButton.alpha = 0;
        startButtonText.alpha = 0;
        failText.alpha = 0;
        startButtonEnabler.interactable = false;
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

}
