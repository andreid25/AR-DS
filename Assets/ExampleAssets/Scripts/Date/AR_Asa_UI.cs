using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class AR_Asa_UI : MonoBehaviour
{
    [SerializeField] private CanvasGroup blackBG, startText, startButton, failText, endWalkButton, walkingInstructions, letsGoButton;
    [SerializeField] private CanvasGroup completeText, youGotText, plushImage, mainMenuButton, mainMenuButtonText;
    [SerializeField] private CanvasGroup noRewardsText, earsImage;
    [SerializeField] private Button startButtonEnabler, mainMenuButtonEnabler, endWalkButtonEnabler, letsGoButtonEnabler;
    [SerializeField] private GameObject aRScriptObject;
    [SerializeField] private RectTransform plushReward, earsReward;
    [SerializeField] private AudioSource dateEnd, click;

    [SerializeField]
    private TMP_Text positionText;

    void Awake()
    {
        blackBG.alpha = 0;
        startText.alpha = 0;
        startButton.alpha = 0;
        failText.alpha = 0;
        startButtonEnabler.interactable = false;

        completeText.alpha = 0;
        youGotText.alpha = 0;
        plushImage.alpha = 0;
        noRewardsText.alpha = 0;
        earsImage.alpha = 0;
        mainMenuButton.alpha = 0;
        mainMenuButtonText.alpha = 0;
        mainMenuButtonEnabler.interactable = false;

        endWalkButton.alpha = 0;
        endWalkButtonEnabler.interactable = false;

        walkingInstructions.alpha = 0;
        letsGoButton.alpha = 0;
        letsGoButtonEnabler.interactable = false;

        //letsGoButton.blocksRaycasts = false;
    }
    private void Start()
    {
        StartCoroutine(CoStart());
        blackBG.DOFade(1, 1.5f);
        startText.DOFade(1, 1.5f);
        startButton.DOFade(1, 1.5f);
    }
    private IEnumerator CoStart()
    {
        yield return new WaitForSeconds(1.0f);
        startButtonEnabler.interactable = true;
    }
    public void StartButton()
    {
        click.Play();
        blackBG.DOFade(0, 1.0f);
        startText.DOFade(0, 1.0f);
        startButton.DOFade(0, 1.0f);
        startButtonEnabler.interactable = false;
        startButton.blocksRaycasts = false;
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

    public void DateEnd()
    {
        dateEnd.Play();
        StartCoroutine(CoDateEnd());
        blackBG.DOFade(1, 1.5f);
        mainMenuButton.DOFade(1, 1.5f);
        mainMenuButtonText.DOFade(1, 1.5f);
        completeText.DOFade(1, 1.5f);
        bool plushAcquired = FindObjectOfType<GlobalData>().CheckPlush();
        bool earsAcquired = FindObjectOfType<GlobalData>().CheckCatEars();
        if (plushAcquired && earsAcquired)
        {
            plushReward.anchoredPosition = new Vector2(plushReward.anchoredPosition.x - 200, plushReward.anchoredPosition.y);
            earsReward.anchoredPosition = new Vector2(earsReward.anchoredPosition.x + 200, earsReward.anchoredPosition.y);
            youGotText.DOFade(1, 1.5f);
            plushImage.DOFade(1, 1.5f);
            earsImage.DOFade(1, 1.5f);
        }
        else if (plushAcquired)
        {
            youGotText.DOFade(1, 1.5f);
            plushImage.DOFade(1, 1.5f);
        }
        else if (earsAcquired)
        {
            youGotText.DOFade(1, 1.5f);
            earsImage.DOFade(1, 1.5f);
        }
        else
        {
            noRewardsText.DOFade(1, 1.5f);
        }

    }
    private IEnumerator CoDateEnd()
    {
        yield return new WaitForSeconds(1.5f);
        mainMenuButtonEnabler.interactable = true;
    }

    public void MainMenuReturn()
    {
        click.Play();
        Loader.Load(Loader.Scene.PhoneMenus);
    }

    public void WalkingInstructions()
    {
        StartCoroutine(CoWalkingInstructions());
        blackBG.DOFade(1, 1.5f);
        walkingInstructions.DOFade(1, 1.5f);
        letsGoButton.DOFade(1, 1.5f);
    }
    private IEnumerator CoWalkingInstructions()
    {
        yield return new WaitForSeconds(1.0f);
        letsGoButtonEnabler.interactable = true;
    }

    public void LetsGoButton()
    {
        click.Play();
        blackBG.DOFade(0, 1.0f);
        walkingInstructions.DOFade(0, 1.0f);
        letsGoButton.DOFade(0, 1.0f);
        letsGoButtonEnabler.interactable = false;
        FindObjectOfType<DialogueTrigger>().StartSkip();
    }
    public void Skipping()
    {
        endWalkButton.alpha = 1;
        endWalkButtonEnabler.interactable = true;
    }
    public void SkippingConversationStart()
    {
        endWalkButton.alpha = 0;
        endWalkButtonEnabler.interactable = false;
    }
    public void EndSkipping()
    {
        click.Play();
        FindObjectOfType<AsaAnimationManager>().SkippingStop();
        endWalkButton.alpha = 0;
        endWalkButtonEnabler.interactable = false;
        FindObjectOfType<DialogueTrigger>().SkipEnd();
    }

    public void PositionDebug(Vector3 position)
    {
        positionText.text = position.ToString();
    }
}
