using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveController : MonoBehaviour
{
    public ObjectManipulator objectManipulator;
    public Movement moveObject;
    [HideInInspector]
    public bool RecordEnabled = false;
    [HideInInspector]
    public bool isInReplayMode = false;
    [HideInInspector]
    public bool isInRecordMode = false;
    [SerializeField]
    public bool playAfterRecord = false;
    [HideInInspector]
    public bool isGrabbed = false;

    public void EnableRecord()
    {
        RecordEnabled = true;
    }

    public void DisableRecord()
    {
        RecordEnabled = false;
    }

    void AlignWithController(OVRInput.Controller cntrlr)
    {
        //controller.position = OVRInput.GetLocalControllerPosition(controller);
        objectManipulator.grabObject.transform.rotation = OVRInput.GetLocalControllerRotation(cntrlr);

    }

    private void Update()
    {
        if (objectManipulator.grabObject != null)
        {
            isGrabbed = true; 
        } else
        {
            isGrabbed = false;
        }

        if (isGrabbed && !isInReplayMode)
        {
            AlignWithController(OVRInput.Controller.RTouch);
        }

        // Is Recording Enabled
        if (RecordEnabled) {
            Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "Recording is Enabled");
            // Is Object Being Grabbed
            if (isGrabbed)
            {
                Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "Object is grabbed");

                // Is Object Ready To Record
                bool triggerPressed = OVRInput.Get(OVRInput.RawButton.A);
                if (triggerPressed && !isInRecordMode)
                {
                    Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "Start Recording! ??");
                    moveObject.StartRecord();
                    isInRecordMode = true;
                    return;
                }
                if (triggerPressed && isInRecordMode)
                {
                    return;
                }
            }
            //else
            //{
            //    Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "Object is NOT grabbed");
            //}
        }// else
        //{
        //    Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "Recording is NOT Enabled");
        //}

        if (isInRecordMode)
        {
            Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "Start Replaying! ??");
            moveObject.StopRecord();
            isInRecordMode = false;
            if (playAfterRecord)
            {
                moveObject.StartReplay();
            }
        }
        moveObject.StopRecord();


    }

}
