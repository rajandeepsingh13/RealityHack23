using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.Rendering;

public enum ReplayType { Once, Loop, PingPong }

public enum CurrentDirection { Forward, Backwards }

public class MovementRecord
{
    public Vector3 movePosition;
    public Quaternion moveRotation;
    public Vector3 moveScale;
} 

public class Movement : MonoBehaviour
{
    public bool isInReplayMode = false;
    public bool isInRecordMode = false;
    public float startReplayTime;
    public float startRecordTime;
    public Vector3 startPosition;
    public Quaternion startRotation;
    public Vector3 startScale;
    public GameObject controller;
    public SerializedDictionary<float, MovementRecord> movementTempMap = new SerializedDictionary<float, MovementRecord>();
    public ObjectManipulator objectManipulator;
    public ReplayType type = ReplayType.Loop;
    public CurrentDirection currentDirection = CurrentDirection.Forward;

    public void StopRecord()
    {
        isInRecordMode = false;
    }

    public void StartRecord()
    {
        isInReplayMode = false;
        startRecordTime = Time.time;
        movementTempMap.Clear();
        isInRecordMode = true;
        startPosition = new Vector3(controller.transform.position.x, controller.transform.position.y, controller.transform.position.z);
        startRotation = controller.transform.localRotation;
        startScale = controller.transform.localScale;
    }

    public void StartReplay()
    {
        isInRecordMode = false;
        startReplayTime = Time.time;
        isInReplayMode = true;
        if (type == ReplayType.PingPong && currentDirection == CurrentDirection.Backwards)
        {
            float[] array = movementTempMap.Keys.ToArray();
            controller.transform.position = movementTempMap[array[array.Length - 1]].movePosition;
            controller.transform.localRotation = movementTempMap[array[array.Length - 1]].moveRotation;
            controller.transform.localScale = movementTempMap[array[array.Length - 1]].moveScale;
        }
        else
        {
            controller.transform.position = startPosition;
            controller.transform.localRotation = startRotation;
            controller.transform.localScale = startScale;
        }
        
    }

    public void StopReplay()
    {
        isInReplayMode = false;
    }

 
    void Update()
    {
        if (isInReplayMode)
        {

            MovementRecord moveRec = null;

            float[] array = movementTempMap.Keys.ToArray();
            var index = 0;

            if (currentDirection == CurrentDirection.Forward)
            { 
                if (startReplayTime != null)
                {
                    Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "Checking Start Replay time");
                    index = Array.IndexOf(array, array.OrderBy(a => Math.Abs((Time.time - startReplayTime) - a)).First());
                }

            } else
            {
                if (startReplayTime != null)
                {
                    Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "Checking Start Replay time");
                    index = Array.IndexOf(array, array.OrderBy(a => Math.Abs((array[array.Length-1] - (Time.time - startReplayTime)) - a)).First());
                }
            }

            Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "Checking index: " + index);
            moveRec = movementTempMap[array[index]];

            
            if (moveRec != null)
            {
                Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "Move rec is NOT null");
                controller.transform.position = moveRec.movePosition;
                Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "Position: " + moveRec.movePosition);
                controller.transform.localRotation = moveRec.moveRotation;
                controller.transform.localScale = moveRec.moveScale;
            }
            else
            {
                Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "Move rec is null");

            }

            if (currentDirection == CurrentDirection.Forward)
            {
                if (index == (movementTempMap.Count() - 1))
                {
                    isInReplayMode = false;
                    if (type == ReplayType.Loop)
                    {
                        StopReplay();
                        StartReplay();
                    }

                    if (type == ReplayType.PingPong)
                    {
                        currentDirection = CurrentDirection.Backwards;
                        StopReplay();
                        StartReplay();
                    }
                }
            } else
            {
                if (index <= 0)
                {
                    isInReplayMode = false;
                    if (type == ReplayType.Loop)
                    {
                        StopReplay();
                        StartReplay();
                    }

                    if (type == ReplayType.PingPong)
                    {
                        currentDirection = CurrentDirection.Forward;
                        StopReplay();
                        StartReplay();
                    }
                }
            }

            return;
        }
        
        if (isInRecordMode)
        {
            MovementRecord mr = new MovementRecord
            {
                movePosition = controller.transform.position,
                moveRotation = controller.transform.localRotation,
                moveScale = controller.transform.localScale
            };
            movementTempMap.Add(Time.time - startRecordTime, mr);
            Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "movementTempMap count: " + movementTempMap.Count());
        }
    }

}