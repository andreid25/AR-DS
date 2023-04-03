using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsaAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator asaAnimator;
    // Start is called before the first frame update
    void Awake()
    {
        asaAnimator.SetBool("IsPleased", false);
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
}
