using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AsaPlush : MonoBehaviour
{
    [SerializeField] private GameObject activePlush;
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
            activePlush.transform.DOMove(new Vector3(14, -12, 75), .5f);
            activePlush.transform.DORotate(new Vector3(0, 180, 0), .5f);
            activePlush.transform.DOScale(new Vector3(75, 75, 75), .5f);
            FindObjectOfType<Phone_Menus>().CollectionObjectBack("plush");

        }
        else
        {
            isSelected = true;
            activePlush.transform.DOMove(new Vector3(0, -13, 60), .5f);
            activePlush.transform.DOScale(new Vector3(120, 120, 120), .5f);
            StartCoroutine(Spinning());
            FindObjectOfType<Phone_Menus>().CollectionObjectShow("plush");
        }
    }

    private IEnumerator Spinning()
    {
        yield return new WaitForSeconds(.5f);
        while (isSelected)
        {
            activePlush.transform.eulerAngles = new Vector3(activePlush.transform.eulerAngles.x, activePlush.transform.eulerAngles.y + .4f, activePlush.transform.eulerAngles.z);
            yield return null;
            UnityEngine.Debug.Log("Spinning");
        }

    }

    public void Reset()
    {
        isSelected = false;
        activePlush.transform.position = new Vector3(14, -12, 75);
        activePlush.transform.eulerAngles = new Vector3(0, 180, 0);
        activePlush.transform.localScale = new Vector3(75, 75, 75);
    }
}
