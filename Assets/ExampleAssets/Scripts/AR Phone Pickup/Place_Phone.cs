
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class Place_Phone : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject canvas;
    private Pose pose;
    private GameObject obj;
    private GameObject objTwo;
    //public Controller control;

    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();


    bool canPlace = false;


    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        aRPlaneManager = GetComponent<ARPlaneManager>();

        // StartCoroutine(WaitingToAllow());
    }

    public void PlacePhoneAllowed()
    {
        canPlace = true;
        StartCoroutine(CoPlacePhoneAllowed());
    }

    public IEnumerator CoPlacePhoneAllowed()
    {
        int attemptCount = 0;
        while (canPlace)
        {
            yield return new WaitForSeconds(.2f);
            //UnityEngine.Debug.Log(Camera.main.transform.forward);
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (aRRaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                canPlace = false;
                pose = hits[0].pose;
                GameObject obj = Instantiate(prefab, pose.position, pose.rotation);

                Vector3 position = obj.transform.position;
                position.y = 0f;
                Vector3 cameraPosition = Camera.main.transform.position;
                cameraPosition.y = 0f;
                Vector3 direction = cameraPosition - position;
                Quaternion targetRoation = Quaternion.LookRotation(direction);
                obj.transform.rotation = targetRoation;

                //GameObject objTwo = Instantiate(prefabTwo, pose.position, pose.rotation);

                //set color is not working on the object
                //prefab.GetComponent<Renderer>().sharedMaterial.color = new Color(255, 0, 255);
                StartCoroutine(WaitingToAllow());

            }
            else
            {
                attemptCount++;
                if (attemptCount >= 40)
                {
                    canvas.GetComponent<AR_Pone_UI>().PlaceFail();
                    attemptCount = 0;
                    yield return new WaitForSeconds(2.8f);
                }
            }
        }
    }


    /*//methoda for tapping and untapping screen
    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }
    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }

    //method for instantiating a model after tapping a plane
    private void FingerDown(EnhancedTouch.Finger finger)
    {
        //don't do anything if multiple fingers are down or more than one is spawned
        if (finger.index != 0) return;
        if (!canPlace) return;

        //code to instantiate object
        if (aRRaycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            pose = hits[0].pose; 
            GameObject obj = Instantiate(prefab, pose.position, pose.rotation);

            Vector3 position = obj.transform.position;
            position.y = 0f;
            Vector3 cameraPosition = Camera.main.transform.position;
            cameraPosition.y = 0f;
            Vector3 direction = cameraPosition - position;
            Quaternion targetRoation = Quaternion.LookRotation(direction);
            obj.transform.rotation = targetRoation;

            //GameObject objTwo = Instantiate(prefabTwo, pose.position, pose.rotation);

            //set color is not working on the object
            //prefab.GetComponent<Renderer>().sharedMaterial.color = new Color(255, 0, 255);
            StartCoroutine(WaitingToAllow());

            canPlace = false;
            
        }
        else
        {
            canvas.GetComponent<AR_Pone_UI>().PlaceFail();
        }
    } */

    IEnumerator WaitingToAllow()
    {
        yield return new WaitForSeconds(0.1f);
        //objTwo.GetComponent<Renderer>().material.color = new Color(0, 0, 255);
        //prefab.GetComponent<Renderer>().sharedMaterial.color = new Color(0, 0, 255);
        prefab.GetComponent<Phone_Model_Script>().TapPhoneAllow();
    }
}
