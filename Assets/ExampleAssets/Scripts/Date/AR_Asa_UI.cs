using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AR_Asa_UI : MonoBehaviour
{
    [SerializeField] private CanvasGroup blackBG, startText;

    void Awake()
    {
        blackBG.alpha = 0;
        startText.alpha = 0;
    }
    private IEnumerator Start()
    {
        blackBG.DOFade(1, 1.5f);
        startText.DOFade(1, 1.5f);

        yield return new WaitForSeconds(3f);

        blackBG.DOFade(0, 1.0f);
        startText.DOFade(0, 1.0f);
    }

}
