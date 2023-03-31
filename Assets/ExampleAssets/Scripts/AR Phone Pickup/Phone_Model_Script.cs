using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Diagnostics;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using System;

[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]

public class Phone_Model_Script : MonoBehaviour
{
    [SerializeField] private GameObject aRCam;
    [SerializeField] private Camera aRCamera;
    [SerializeField] private GameObject phone;
    private static bool canTap;

    private int layerNumber = 6;
    private int layerMask;

    private ARRaycastManager aRRaycastManager;
    //private ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        canTap = false;
        aRCam = GameObject.Find("AR Camera");
        aRCamera = GameObject.Find("AR Camera").GetComponent<Camera>(); ;
        layerMask = 1 << layerNumber;

        aRRaycastManager = GetComponent<ARRaycastManager>();
    }
    public void TapPhoneAllow()
    {
        canTap = true;
        UnityEngine.Debug.Log(canTap);
    }
    void Update()
    {
        //UnityEngine.Debug.Log(canTap);
        if (canTap == false)
        {
            //phone.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
            return;
        }
        else
        {
            //UnityEngine.Debug.Log(Input.touchCount + "" + Input.GetTouch(0).phase + "" + TouchPhase.Began);
            if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
            {
                Ray ray = aRCamera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;

                //Touch touch = Input.GetTouch(0);
                //touchPosition = touch.position;

                UnityEngine.Debug.Log("Raycast sent");
                if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, layerMask))   
                {
                    UnityEngine.Debug.Log("Hit" + raycastHit.transform.gameObject.layer);
                    StartCoroutine(Levitate());
                }
            }
        }
    }

    IEnumerator Levitate()
    {
        Vector3 phonePosition = phone.transform.position;
        Vector3 camPosition = aRCam.transform.position;

        //phonePosition.y = 0f;
        //camPosition.y = 0f;

        Vector3 direction = camPosition - phonePosition;
        //direction.x = 0f;
        Quaternion targetRoation = Quaternion.LookRotation(direction);
        UnityEngine.Debug.Log(targetRoation.eulerAngles);
        //targetRoation.x = -90f;
        targetRoation *= Quaternion.Euler(90, 0, 0);
        UnityEngine.Debug.Log(targetRoation.eulerAngles);


        phone.transform.DORotate(targetRoation.eulerAngles, 1.5f);
        //phone.transform.DOLookAt(camPosition, 1.5f);
        UnityEngine.Debug.Log(phone.transform.rotation.eulerAngles);
        UnityEngine.Debug.Log(targetRoation.eulerAngles);
        yield return new WaitForSeconds(1.5f);
        UnityEngine.Debug.Log(phone.transform.rotation.eulerAngles);


        phone.transform.DORotate(targetRoation.eulerAngles, 0.1f);
        for (int i = 0; i <= 14; i++)
        {
            phonePosition = phone.transform.position;
            camPosition = aRCam.transform.position;
            Vector3 movePhone = Camera.main.transform.position + Camera.main.transform.forward * 0.11f;
            movePhone = phonePosition + ((movePhone - phonePosition) / (15-i));
            phone.transform.DOMove(movePhone, 0.1f);

            direction = camPosition - phonePosition;
            targetRoation = Quaternion.LookRotation(direction);
            targetRoation *= Quaternion.Euler(90, 0, 0);
            UnityEngine.Debug.Log(targetRoation.eulerAngles);

            phone.transform.DORotate(targetRoation.eulerAngles, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);


        Loader.Load(Loader.Scene.PhoneMenus);
    }
}
