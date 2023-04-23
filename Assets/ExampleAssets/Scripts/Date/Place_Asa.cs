
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using DG.Tweening;

[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class Place_Asa : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject dialogueController;
    private bool asaPlaced;
    private bool isSkipping;

    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject asaObj;
   // private GameObject cube;

    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        aRPlaneManager = GetComponent<ARPlaneManager>();
        asaPlaced = false;
    }
    public void PlaceAsaAllowed()
    {
        StartCoroutine(CoPlaceAsaAllowed());
    }

    private IEnumerator CoPlaceAsaAllowed()
    {
        int attemptCount = 0;
        while (!asaPlaced)
        {
            yield return new WaitForSeconds(.5f);

            //UnityEngine.Debug.Log(Camera.main.transform.forward);
            Ray ray = new Ray(Camera.main.transform.position, new Vector3(Camera.main.transform.forward.x, -.75f, Camera.main.transform.forward.z));

            if (aRRaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                asaPlaced = true;
                Pose pose = hits[0].pose;
                asaObj = Instantiate(prefab, pose.position, pose.rotation);
                UnityEngine.Debug.Log("i created an asa");
                //anim = GetComponent<Animation>();

                Vector3 position = asaObj.transform.position;
                position.y = 0f;
                Vector3 cameraPosition = Camera.main.transform.position;
                cameraPosition.y = 0f;
                Vector3 direction = cameraPosition - position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                asaObj.transform.rotation = targetRotation;

                asaObj.AddComponent<ARAnchor>();
                //objRotation.y = 180f;
                yield return new WaitForSeconds(6.2f);
                UnityEngine.Debug.Log(asaPlaced);
                FindObjectOfType<DialogueTrigger>().StartDateDialogue1();

            }
            else
            {
                attemptCount++;
                if (attemptCount >= 15)
                {
                    canvas.GetComponent<AR_Asa_UI>().PlaceFail();
                    attemptCount = 0;
                    yield return new WaitForSeconds(2.8f);
                }
            }
            UnityEngine.Debug.Log("In CoPlaceAsa() while loop");
        }
    }

    public void SkippingControl()
    {
        isSkipping = true;
        StartCoroutine(CoSkippingControl());
    }
    private IEnumerator CoSkippingControl()
    {
        UnityEngine.Debug.Log("Inside CoSkipping");
        float timeBetweenMove = .3f;

        int stillCount = 0;

        Vector3 camPointCalc = new Vector3(Camera.main.transform.position.x, asaObj.transform.position.y, Camera.main.transform.position.z);
        float asaCamDistance = Vector3.Distance(camPointCalc, asaObj.transform.position);
        while (isSkipping)
        {
            Vector3 camPosStart = Camera.main.transform.position;
            Vector3 camAngleStart = Camera.main.transform.eulerAngles;

            yield return new WaitForSeconds(timeBetweenMove);

            Vector3 camPosEnd = Camera.main.transform.position;
            Vector3 camAngleEnd = Camera.main.transform.eulerAngles;

            //determine where asa should face
            camPosStart.y = 0f;
            camPosEnd.y = 0f;
            Vector3 direction = camPosEnd - camPosStart;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            //asaObj.transform.rotation = targetRotation;
            

            Vector3 moveTo = Camera.main.transform.position + Camera.main.transform.forward * asaCamDistance;
            moveTo.y = asaObj.transform.position.y;

            //change Asa's y pos if new plane detected
            Ray ray = new Ray(Camera.main.transform.position, new Vector3(Camera.main.transform.forward.x, -.75f, Camera.main.transform.forward.z));
            if (aRRaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                //UnityEngine.Debug.Log("In if");
                Pose pose = hits[0].pose;
                moveTo.y = pose.position.y;

                //asaObj.transform.DORotate(targetRotation.eulerAngles, timeBetweenMove);
            }

            //move asa
            asaObj.transform.DOMove(moveTo, timeBetweenMove);


            UnityEngine.Debug.Log("Rotation change: " + Vector3.Distance(camAngleStart, camAngleEnd));
            //UnityEngine.Debug.Log(Vector3.Distance(camPosStart, camPosEnd));
            if (Vector3.Distance(camPosStart, camPosEnd) > .1f || Vector3.Distance(camAngleStart, camAngleEnd) > 100f) //change float for movement threshold to rotate
            {
                asaObj.transform.DORotate(targetRotation.eulerAngles, timeBetweenMove);
                stillCount = 0;
            }
            //check if asa is staying still so they can look at you
            else
            {
                stillCount ++;
                if (stillCount > 10)
                {
                    Vector3 asaPosition3 = asaObj.transform.position;
                    asaPosition3.y = 0f;
                    Vector3 direction3 = camPosEnd - asaPosition3;
                    Quaternion targetRotation3 = Quaternion.LookRotation(direction3);
                    asaObj.transform.DORotate(targetRotation3.eulerAngles, timeBetweenMove);
                }
            }

        }
        UnityEngine.Debug.Log("Outside Skipping loop");
        Vector3 asaPosition2 = asaObj.transform.position;
        asaPosition2.y = 0f;
        Vector3 cameraPosition2 = Camera.main.transform.position;
        cameraPosition2.y = 0f;
        Vector3 direction2 = cameraPosition2 - asaPosition2;
        Quaternion targetRotation2 = Quaternion.LookRotation(direction2);
        asaObj.transform.DORotate(targetRotation2.eulerAngles, 1f);
    }
    public void SkippingControlStop()
    {
        isSkipping = false;
    }

    public void DestroyAsa()
    {
        UnityEngine.Debug.Log("hi");
        Destroy(asaObj);
    }
    
}

