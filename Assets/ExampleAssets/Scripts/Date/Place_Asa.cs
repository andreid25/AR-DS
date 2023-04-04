
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class Place_Asa : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject dialogueController;
    //[SerializeField]
    //private GameObject fabCube;
    private bool asaPlaced;

    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject obj;
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
                obj = Instantiate(prefab, pose.position, pose.rotation);
                //cube = Instantiate(fabCube, pose.position, pose.rotation);
                UnityEngine.Debug.Log("i created an asa");
                //anim = GetComponent<Animation>();

                Vector3 position = obj.transform.position;
                position.y = 0f;
                Vector3 cameraPosition = Camera.main.transform.position;
                cameraPosition.y = 0f;
                Vector3 direction = cameraPosition - position;
                Quaternion targetRoation = Quaternion.LookRotation(direction);
                obj.transform.rotation = targetRoation;
                //objRotation.y = 180f;
                yield return new WaitForSeconds(6.2f);
                UnityEngine.Debug.Log(asaPlaced);
                dialogueController.GetComponent<DialogueTrigger>().StartDateDialogue1();

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
        }
    }

    public void DestroyAsa()
    {
        UnityEngine.Debug.Log("hi");
        Destroy(obj);
    }
    
}

