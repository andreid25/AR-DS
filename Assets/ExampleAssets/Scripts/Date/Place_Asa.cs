
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using DG.Tweening;
using System;

[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class Place_Asa : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject dialogueController;
    private bool asaPlaced, isSkipping;
    private Vector3 endPos = new Vector3(0,0,0);

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
        asaPlaced = false;
        StartCoroutine(CoPlaceAsaAllowed());
    }
    public void DisableAsaPlace()
    {
        asaPlaced = true;
    }

    private IEnumerator CoPlaceAsaAllowed()
    {
        yield return new WaitForSeconds(1.1f);
        int attemptCount = 0;
        while (!asaPlaced)
        {

            //UnityEngine.Debug.Log(Camera.main.transform.forward);
            Ray ray = new Ray(Camera.main.transform.position, new Vector3(Camera.main.transform.forward.x, -.75f, Camera.main.transform.forward.z));

            if (aRRaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                FindObjectOfType<AR_Asa_UI>().HideInstructions();
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
                //StartCoroutine(TurnToYou());
                yield return new WaitForSeconds(6.2f);
                FindObjectOfType<DialogueTrigger>().StartDateDialogue1();
                //StartCoroutine(AsaPlace());

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
            yield return new WaitForSeconds(.5f);
            
        }
    }
    /*private IEnumerator AsaPlace()
    {
        while (true)
        {
            FindObjectOfType<AR_Asa_UI>().PositionDebug(asaObj.transform.position);
            yield return new WaitForSeconds(.2f);
        }
    }*/
    private IEnumerator TurnToYou() //One day this will work
    {
        //float camRotation;
        //float asaRotation;
        while (!isSkipping)
        {
            Vector3 position = asaObj.transform.position;
            position.y = 0f;
            Vector3 cameraPosition = Camera.main.transform.position;
            cameraPosition.y = 0f;
            Vector3 direction = cameraPosition - position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            //asaObj.transform.rotation = targetRotation;
            Quaternion currentRotation = asaObj.transform.rotation;

            //float angleDifference = Quaternion.Angle(targetRotation, currentRotation);

            float asaRotation = asaObj.transform.rotation.y;
            float camRotation = Camera.main.transform.rotation.y;

            //UnityEngine.Debug.Log(angleDifference);
            if (camRotation > 0)
            {
                camRotation -= 1; //check if this is right
            }
            else
            {
                camRotation += 1f;
            }
            float rotationDifference = camRotation - asaRotation;
            if (rotationDifference > .3f || rotationDifference < -.3f)
            {
                asaObj.transform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, 2f);
            }
            /*asaDirection = asaObj.transform.rotation.EulerAngles * Vector3.forward;
            var forwardB = Camera.main.transform.rotation.EulerAngles * Vector3.forward;

            var angleA = Mathf.Atan2(asaDirection.x, asaDirection.z);
            var angleB = Mathf.Atan2(forwardB.x, forwardB.z);

            float angleDiff = Mathf.DeltaAngle(angleA, angleB);

            UnityEngine.Debug.Log("This is hard");*/
            yield return null;
        }
    }
    public void SkippingControl()
    {
        Destroy(GetComponent<ARAnchor>());
        isSkipping = true;
        StartCoroutine(CoSkippingControl());
    }
    private IEnumerator CoSkippingControl()
    {
        UnityEngine.Debug.Log("Inside CoSkipping");
        float timeBetweenMove = .3f;

        int stillCount = 0;
        float lookTimer = 0;
        float timetoLook = UnityEngine.Random.Range(1.5f, 5.0f);
        bool isLooking = true;

        Vector3 camPointCalc = new Vector3(Camera.main.transform.position.x, asaObj.transform.position.y, Camera.main.transform.position.z);
        float asaStartDistance = Vector3.Distance(camPointCalc, asaObj.transform.position);
        float sevenSecondTime = 0f;
        float asaDistance;

        FindObjectOfType<AsaAnimationManager>().Look(.2f, 70f);
        while (isSkipping)
        {
            Vector3 asaStartLocation = asaObj.transform.position;

            yield return new WaitForSeconds(timeBetweenMove);

            Vector3 newCamPos = Camera.main.transform.position;

            sevenSecondTime += (timeBetweenMove / 7);
            if (sevenSecondTime < 1f)
            {
                asaDistance = Mathf.Lerp(asaStartDistance, 1.2f, sevenSecondTime);
            }
            else
            {
                asaDistance = 1.2f;
            }


            //calvulate where asa should move to
            Vector3 moveTo = newCamPos + Camera.main.transform.forward * asaDistance;
            moveTo.y = asaObj.transform.position.y;

            //change Asa's y pos if new plane detected
            Ray ray = new Ray(Camera.main.transform.position, new Vector3(Camera.main.transform.forward.x, -.75f, Camera.main.transform.forward.z));
            if (aRRaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose pose = hits[0].pose;
                moveTo.y = pose.position.y;
            }

            //determine where asa should face
            Vector3 moveToNoY = new Vector3(moveTo.x, 0, moveTo.z);
            Vector3 asaStartLocationNoY = new Vector3(asaStartLocation.x, 0, asaStartLocation.z);
            Vector3 direction = moveToNoY - asaStartLocationNoY;
            Quaternion asaLookAngle = Quaternion.LookRotation(direction);

            //move asa
            asaObj.transform.DOMove(moveTo, timeBetweenMove);

            if (Vector3.Distance(asaStartLocation, moveTo) > .3f)
            {
                asaObj.transform.DORotate(asaLookAngle.eulerAngles, timeBetweenMove);
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
                    Vector3 newCamPosNoY = new Vector3(newCamPos.x, 0, newCamPos.z);
                    Vector3 direction3 = newCamPosNoY - asaPosition3;
                    Quaternion targetRotation3 = Quaternion.LookRotation(direction3);
                    asaObj.transform.DORotate(targetRotation3.eulerAngles, timeBetweenMove);
                }
            }
            //look over at you
            lookTimer += timeBetweenMove;
            if (lookTimer >= timetoLook)
            {
                lookTimer = 0f;
                if (isLooking)
                {
                    UnityEngine.Debug.Log("Looking away");
                    isLooking = false;
                    FindObjectOfType<AsaAnimationManager>().Look(.4f, 0f);
                    timetoLook = UnityEngine.Random.Range(5f, 15f);
                }
                else
                {
                    UnityEngine.Debug.Log("Looking at u");
                    isLooking = true;
                    FindObjectOfType<AsaAnimationManager>().Look(.4f, 70f);
                    timetoLook = UnityEngine.Random.Range(2f, 6f);
                }
            }

            endPos = asaObj.transform.position;

        }
        UnityEngine.Debug.Log("Outside Skipping loop");

        //stop skipping rotate to you
        Vector3 asaPosition2 = asaObj.transform.position;
        asaPosition2.y = 0f;
        Vector3 cameraPosition2 = Camera.main.transform.position;
        cameraPosition2.y = 0f;
        Vector3 direction2 = cameraPosition2 - asaPosition2;
        Quaternion targetRotation2 = Quaternion.LookRotation(direction2);
        asaObj.transform.DORotate(targetRotation2.eulerAngles, 1f);

        //position still override
        StartCoroutine(ManualPosOverride(endPos));
        yield return new WaitForSeconds(1f);
        asaObj.AddComponent<ARAnchor>();
        //StartCoroutine(TurnToYou());
        StartCoroutine(ManualRotationOverride(targetRotation2));
    }
    public void SkippingControlStop()
    {
        isSkipping = false;

    }
    private IEnumerator ManualPosOverride(Vector3 endPos)
    {
        while (!isSkipping)
        {
            asaObj.transform.position = endPos;
            yield return null;
        }
    }
    private IEnumerator ManualRotationOverride(Quaternion look)
    {
        while (!isSkipping)
        {
            asaObj.transform.rotation = look;
            yield return null;
        }
    }

    public void DestroyAsa()
    {
        UnityEngine.Debug.Log("hi");
        asaPlaced = false;
        Destroy(asaObj);
    }
    
}

