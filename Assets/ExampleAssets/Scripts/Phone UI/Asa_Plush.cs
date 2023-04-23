using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Asa_Plush : MonoBehaviour
{
    [SerializeField] private GameObject plush;
    
    private bool isSelected = false;

    // Start is called before the first frame update
    void Awake()
    {
        UnityEngine.Debug.Log(plush.transform.eulerAngles);
    }
    void OnMouseDown()
    {
        /*if (SceneManager.GetActiveScene() == "PhoneMenus")
        {

        }*/
        if (isSelected)
        {
            isSelected = false;
            plush.transform.DOMove(new Vector3(500, -350, 2700), .5f);
            plush.transform.DORotate(new Vector3(0, 180, 0), .5f);
            plush.transform.DOScale(new Vector3(2500, 2500, 2500), .5f);

        }
        else
        {
            isSelected = true;
            plush.transform.DOMove(new Vector3(0, -500, 2000), .5f);
            plush.transform.DOScale(new Vector3(4000, 4000, 4000), .5f);
            StartCoroutine(Spinning());
        }
    }

    private IEnumerator Spinning()
    {
        yield return new WaitForSeconds(.5f);
        while (isSelected)
        {
            plush.transform.eulerAngles = new Vector3(plush.transform.eulerAngles.x, plush.transform.eulerAngles.y + .1f, plush.transform.eulerAngles.z);
            yield return null;
        }
        
    }

    public void Reset()
    {
        isSelected = false;
        plush.transform.position = new Vector3(500, -350, 2700);
        plush.transform.eulerAngles = new Vector3(0, 180, 0);
        plush.transform.localScale = new Vector3(2500, 2500, 2500);
    }
    // Update is called once per frame
}
