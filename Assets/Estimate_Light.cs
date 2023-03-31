using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Estimate_Light : MonoBehaviour
{
    public ARCameraManager arcam;
    public Light our_light;
    void OnEnable()
    {
        arcam.frameReceived += GetLight;
    }

    void OnDisable()
    {
        arcam.frameReceived -= GetLight;
    }

    void GetLight(ARCameraFrameEventArgs args)
    {
        /*UnityEngine.Debug.Log(args.lightEstimation);
        if (args.lightEstimation.mainLightColor:HasValue)
        {
            our_light.color = args.lightEstimation.mainLightColor.Value;
        }*/
    }

}
