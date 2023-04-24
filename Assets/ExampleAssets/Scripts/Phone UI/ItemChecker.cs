using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemChecker : MonoBehaviour
{
    [SerializeField] private GameObject plush;

    void Start()
    {
        UnityEngine.Debug.Log("plush created");
        //GameObject activePlush = Instantiate(plush, new Vector3(500, -350, 2700), Quaternion.identity);
        if (FindObjectOfType<GlobalData>().CheckPlush())
        {
            GameObject activePlush = Instantiate(plush);
        }
    }
    // Update is called once per frame
}
