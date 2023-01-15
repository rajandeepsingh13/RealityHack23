using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveController : MonoBehaviour
{
    public ObjectManipulator objectManipulator;
    public Movement moveObject; 
    public bool RecordEnabled = false;
    public bool isInReplayMode = false;
    public bool isInRecordMode = false;
    public bool isGrabbed = false;

    public void EnableRecord()
    {
        RecordEnabled = true;
    }

    public void DisableRecord()
    {
        RecordEnabled = false;
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
            moveObject.StartReplay();
        }
        moveObject.StopRecord();


    }

}
