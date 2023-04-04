using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARDebugger : MonoBehaviour
{
    [SerializeField] private ARSessionOrigin origin;

    private void Awake()
    {
        origin.trackablesParentTransformChanged += OnOriginChange;
    }

    private void OnOriginChange(ARTrackablesParentTransformChangedEventArgs args)
    {
        UnityEngine.Debug.Log("Origin changed!");
    }
}