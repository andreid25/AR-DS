using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class AsaAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator asaAnimator;
    [SerializeField] private GameObject asaPlush, middleFinger;
    [SerializeField] private Rig head;

    private GameObject realPlush;
    private bool plushExists;
    void Awake()
    {
        asaAnimator.SetBool("IsPleased", false);
        asaAnimator.SetBool("IsGivingRun", false);
        asaAnimator.SetBool("ItemTaken", false);
        plushExists = false;
    }

    public void Pleased()
    {
        StartCoroutine(CoPleased());
    }
    private IEnumerator CoPleased()
    {
        asaAnimator.SetBool("IsPleased", true);
        yield return new WaitForSeconds(1f);
        asaAnimator.SetBool("IsPleased", false);
    }
    public void GiveYouRun()
    {
        NoLook();
        StartCoroutine(CoGiveYouRun());
    }
    private IEnumerator CoGiveYouRun()
    {
        asaAnimator.SetBool("IsGivingRun", true);
        yield return new WaitForSeconds(5f);
        realPlush = Instantiate(asaPlush, middleFinger.transform.position, middleFinger.transform.rotation);
        plushExists = true;
    }

    public void ItemTakeRun()
    {
        Destroy(realPlush);
        plushExists = false;
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

    public void NoLook()
    {
        StartCoroutine(CoNoLook());
    }
    private IEnumerator CoNoLook()
    {
        float elapsedTime = 0;
        float waitTime = 2f;
        while (elapsedTime < waitTime)
        {
            head.weight = (Mathf.Lerp(100, 0, (elapsedTime / waitTime))/100);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void Update()
    {
        if (plushExists)
        {
            realPlush.transform.position = middleFinger.transform.position;
            realPlush.transform.eulerAngles = new Vector3(middleFinger.transform.eulerAngles.x, middleFinger.transform.eulerAngles.y - 90f, middleFinger.transform.eulerAngles.z - 160f);
        }
    }
}
