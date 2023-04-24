using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AsaPlush : MonoBehaviour
{
    [SerializeField] private GameObject activePlush;
    private bool isSelected = false;
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
            activePlush.transform.DOMove(new Vector3(500, 0, 2700), .5f);
            activePlush.transform.DORotate(new Vector3(0, 180, 0), .5f);
            activePlush.transform.DOScale(new Vector3(2500, 2500, 2500), .5f);
            FindObjectOfType<Phone_Menus>().CollectionObjectBack("plush");

        }
        else
        {
            isSelected = true;
            activePlush.transform.DOMove(new Vector3(0, 100, 2000), .5f);
            activePlush.transform.DOScale(new Vector3(4000, 4000, 4000), .5f);
            StartCoroutine(Spinning());
            FindObjectOfType<Phone_Menus>().CollectionObjectShow("plush");
        }
    }

    private IEnumerator Spinning()
    {
        yield return new WaitForSeconds(.5f);
        while (isSelected)
        {
            transform.eulerAngles = new Vector3(activePlush.transform.eulerAngles.x, activePlush.transform.eulerAngles.y + .1f, activePlush.transform.eulerAngles.z);
            yield return null;
        }

    }

    public void Reset()
    {
        isSelected = false;
        activePlush.transform.position = new Vector3(500, 0, 2700);
        activePlush.transform.eulerAngles = new Vector3(0, 180, 0);
        activePlush.transform.localScale = new Vector3(2500, 2500, 2500);
    }
}
