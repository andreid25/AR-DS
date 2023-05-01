using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CatEars : MonoBehaviour
{
    [SerializeField] private GameObject activeEars;
    [SerializeField] private bool isSelected = false;
    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    void OnMouseDown()
    {
        if (isSelected)
        {
            isSelected = false;
            activeEars.transform.DOMove(new Vector3(-14, -4, 75), .5f);
            activeEars.transform.DORotate(new Vector3(0, 0, 0), .5f);
            activeEars.transform.DOScale(new Vector3(7, 7, 7), .5f);
            FindObjectOfType<Phone_Menus>().CollectionObjectBack("ears");

        }
        else
        {
            isSelected = true;
            activeEars.transform.DOMove(new Vector3(0, 0, 60), .5f);
            activeEars.transform.DOScale(new Vector3(12, 12, 12), .5f);
            StartCoroutine(Spinning());
            FindObjectOfType<Phone_Menus>().CollectionObjectShow("ears");
        }
    }

    private IEnumerator Spinning()
    {
        yield return new WaitForSeconds(.5f);
        while (isSelected)
        {
            activeEars.transform.eulerAngles = new Vector3(activeEars.transform.eulerAngles.x, activeEars.transform.eulerAngles.y + .4f, activeEars.transform.eulerAngles.z);
            yield return null;
            UnityEngine.Debug.Log("Spinning");
        }

    }

    public void Reset()
    {
        isSelected = false;
        activeEars.transform.position = new Vector3(-14, -4, 75);
        activeEars.transform.eulerAngles = new Vector3(0, 0, 0);
        activeEars.transform.localScale = new Vector3(7, 7, 7);
    }
}
