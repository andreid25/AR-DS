using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class AsaAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator asaAnimator;
    [SerializeField] private GameObject asaPlush, middleFinger, catEars, topOfHead;
    [SerializeField] private Rig head;

    private GameObject realPlush, realEars;
    private bool plushExists;
    void Awake()
    {
        plushExists = false;
        //headLooking = true;
    }

    public void Idle()
    {
        StartCoroutine(CoIdle());
    }
    private IEnumerator CoIdle()
    {
        asaAnimator.SetBool("PlayExit", true);
        while (!(asaAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
        {
            yield return null;
        }
        asaAnimator.SetBool("PlayExit", false);
    }

    public void Pleased()
    {
        StartCoroutine(CoPleased());
    }
    private IEnumerator CoPleased()
    {
        if (!(asaAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
        {
            asaAnimator.SetBool("SnapToIdle", true);
            while (!(asaAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
            {
                yield return null;
            }
            asaAnimator.SetBool("SnapToIdle", false); ;
        }
        asaAnimator.SetBool("IsPleased", true);
        yield return new WaitForSeconds(.2f);
        asaAnimator.SetBool("IsPleased", false);
    }

    public void Dissapointed()
    {
        StartCoroutine(CoDissapointed());
    }
    private IEnumerator CoDissapointed()
    {
        if (!(asaAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
        {
            asaAnimator.SetBool("SnapToIdle", true);
            while (!(asaAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
            {
                yield return null;
            }
            asaAnimator.SetBool("SnapToIdle", false); ;
        }
        asaAnimator.SetBool("IsDissapointed", true);
        yield return new WaitForSeconds(.2f);
        asaAnimator.SetBool("IsDissapointed", false);
    }

    public void Nya()
    {
        StartCoroutine(CoNya());
    }
    private IEnumerator CoNya()
    {
        if (!(asaAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
        {
            asaAnimator.SetBool("SnapToIdle", true);
            while (!(asaAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
            {
                yield return null;
            }
            asaAnimator.SetBool("SnapToIdle", false); ;
        }
        asaAnimator.SetBool("IsNyaing", true);
        yield return new WaitForSeconds(.2f);
        StartCoroutine(CatEars());
        asaAnimator.SetBool("IsNyaing", false);
    }
    private IEnumerator CatEars()
    {
        UnityEngine.Debug.Log("In CatEars()");
        realEars = Instantiate(catEars, topOfHead.transform.position, topOfHead.transform.rotation);
        while (!(asaAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
        {
            realEars.transform.position = topOfHead.transform.position;
            realEars.transform.eulerAngles = new Vector3(topOfHead.transform.eulerAngles.x, topOfHead.transform.eulerAngles.y, topOfHead.transform.eulerAngles.z);
            yield return null;
        }
        Destroy(realEars);
    }
    public void TeeHee()
    {
        StartCoroutine(CoTeeHee());
    }
    private IEnumerator CoTeeHee()
    {
        if (!(asaAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
        {
            asaAnimator.SetBool("SnapToIdle", true);
            while (!(asaAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
            {
                yield return null;
            }
            asaAnimator.SetBool("SnapToIdle", false); ;
        }
        asaAnimator.SetBool("IsTeeHeeing", true);
        yield return new WaitForSeconds(.2f);
        asaAnimator.SetBool("IsTeeHeeing", false);
    }

    public void GiveYouRun()
    {
        Look(2f, 0f);
        StartCoroutine(CoGiveYouRun());
    }
    private IEnumerator CoGiveYouRun()
    {
        asaAnimator.SetBool("IsGivingRun", true);
        yield return new WaitForSeconds(5f);
        realPlush = Instantiate(asaPlush, middleFinger.transform.position, middleFinger.transform.rotation);
        plushExists = true;
        while (plushExists)
        {
            realPlush.transform.position = middleFinger.transform.position;
            realPlush.transform.eulerAngles = new Vector3(middleFinger.transform.eulerAngles.x, middleFinger.transform.eulerAngles.y - 90f, middleFinger.transform.eulerAngles.z - 160f);
            yield return null;
        }
    }

    public void ItemTakeRun()
    {
        plushExists = false;
        Destroy(realPlush);
        asaAnimator.SetBool("ItemTaken", true);
        StartCoroutine(DestroyCheck());
    }
    private IEnumerator DestroyCheck()
    {
        while (!(asaAnimator.GetCurrentAnimatorStateInfo(0).IsName("Disappear")))
        {
            yield return null;
        }
        FindObjectOfType<Place_Asa>().DestroyAsa();
    }

    //Look controls
    public void Look(float time, float intensity)
    {
        StartCoroutine(CoLook(time, intensity));
    }
    private IEnumerator CoLook(float time, float intensity)
    {
        float currentIntensity = head.weight;
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            head.weight = (Mathf.Lerp(currentIntensity, intensity, (elapsedTime / time)) / 100);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    //skipping controls
    public void SkippingStart()
    {
        asaAnimator.SetBool("IsSkipping", true);
        FindObjectOfType<Place_Asa>().SkippingControl();
        FindObjectOfType<AR_Asa_UI>().Skipping();
    }
    public void SkippingStop()
    {
        asaAnimator.SetBool("IsSkipping", false);
        FindObjectOfType<Place_Asa>().SkippingControlStop();
        FindObjectOfType<DialogueTrigger>().SkipEnd();
    }
}
