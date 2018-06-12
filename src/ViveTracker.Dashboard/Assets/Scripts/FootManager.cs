﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Unity_Overlay))]
public class FootManager : MonoBehaviour {

    [HideInInspector]
    public Unity_Overlay overlay;
    public Unity_SteamVR_Handler handler;

    public GameObject ObjectToFollow;

    public bool IsLeftFoot = false;

    protected void Awake()
    {
        overlay = GetComponent<Unity_Overlay>();
    }

    // Use this for initialization
    void Start () {
		if (handler == null)
        {
            Debug.LogError("Unable to find SteamVR handler in scene");
        }

        //SetDeviceToTrack();
    }

    public uint GetFootIndex()
    {
        if (IsLeftFoot)
            return handler.poseHandler.leftFootIndex;
        else
            return handler.poseHandler.rightFootIndex;
    }

    private uint previousIndex = OpenVR.k_unTrackedDeviceIndexInvalid;
    //private bool CheckForTrackerIndexChange()
    //{
    //    var newIndex = GetFootIndex();

    //    if (newIndex != previousIndex)
    //    {
    //        previousIndex = newIndex;
    //        return true;
    //    }
    //    return false;
    //}

    //private void SetDeviceToTrack()
    //{
    //    if (previousIndex == OpenVR.k_unTrackedDeviceIndexInvalid)
    //    {
    //        overlay.deviceToTrack = Unity_Overlay.OverlayTrackedDevice.None;
    //        overlay.customDeviceIndex = 0;
    //    }
    //    else
    //    {
    //        overlay.deviceToTrack = Unity_Overlay.OverlayTrackedDevice.CustomIndex;
    //        overlay.customDeviceIndex = previousIndex;
    //    }
    //}
	
	// Update is called once per frame
	void Update () {
        //if (CheckForTrackerIndexChange())
        //{
        //    SetDeviceToTrack();
        //}

        if (ObjectToFollow != null && GetFootIndex() != OpenVR.k_unTrackedDeviceIndexInvalid)
        {
            if (!overlay.isVisible)
                overlay.isVisible = true;

            //this.transform.position = new Vector3(
            //    ObjectToFollow.transform.position.x,
            //    ObjectToFollow.transform.position.y,
            //    ObjectToFollow.transform.position.z);

            //var rotation = new Quaternion(
            //  ObjectToFollow.transform.rotation.x,
            //  ObjectToFollow.transform.rotation.y,
            //  ObjectToFollow.transform.rotation.z,
            //  ObjectToFollow.transform.rotation.w);

            float RotationX = 0.0f;
            float RotationY = 0.0f;
            float RotationZ = 0.0f;

            if (IsLeftFoot)
            {
                RotationX = MovementCalibration.ComputedLeftFeetBaseRotationX;
                RotationY = MovementCalibration.ComputedLeftFeetBaseRotationY;
                RotationZ = MovementCalibration.ComputedLeftFeetBaseRotationZ;
            }
            else
            {
                RotationX = MovementCalibration.ComputedRightFeetBaseRotationX;
                RotationY = MovementCalibration.ComputedRightFeetBaseRotationY;
                RotationZ = MovementCalibration.ComputedLeftFeetBaseRotationZ;
            }
            
            this.gameObject.transform.localRotation = Quaternion.Euler(RotationX, RotationY, RotationZ);
        }
        else
        {
            if (overlay.isVisible)
                overlay.isVisible = false;
        }
    }
}
