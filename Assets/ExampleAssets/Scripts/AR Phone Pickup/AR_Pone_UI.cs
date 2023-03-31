using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AR_Pone_UI : MonoBehaviour
{
    [SerializeField] private CanvasGroup blackBG, startText, failText;
    [SerializeField] private GameObject ARScriptObject;
    // Start is called before the first frame update

    void Awake()
    {
        blackBG.alpha = 0;
        startText.alpha = 0;
        failText.alpha = 0;
    }
    private IEnumerator Start()
    {
        blackBG.DOFade(1, 1.5f);
        startText.DOFade(1, 1.5f);

        yield return new WaitForSeconds(3f);

        blackBG.DOFade(0, 1.0f);
        startText.DOFade(0, 1.0f);
        ARScriptObject.GetComponent<Place_Phone>().PlacePhoneAllowed();
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
