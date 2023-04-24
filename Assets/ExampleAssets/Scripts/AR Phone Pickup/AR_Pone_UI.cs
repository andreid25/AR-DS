using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class AR_Pone_UI : MonoBehaviour
{
    [SerializeField] private Button startButtonEnabler;
    [SerializeField] private CanvasGroup blackBG, startButton, startButtonText, failText, startText;
    [SerializeField] private GameObject aRScriptObject;
    // Start is called before the first frame update


    void Awake()
    {
        startButtonEnabler.interactable = false;
        blackBG.alpha = 0;
        startText.alpha = 0;
        failText.alpha = 0;
        startButton.alpha = 0;
        startButtonText.alpha = 0;

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
        yield return new WaitForSeconds(1.0f);
        startButtonEnabler.interactable = true;
    }

    public void StartAR()
    {
        blackBG.DOFade(0, 1.0f);
        startText.DOFade(0, 1.0f);
        startButton.DOFade(0, 1.0f);
        startButtonText.DOFade(0, 1.0f);
        startButtonEnabler.interactable = false;
        aRScriptObject.GetComponent<Place_Phone>().PlacePhoneAllowed();
    }


    // Update is called once per frame
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
