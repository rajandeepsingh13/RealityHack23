using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.Rendering;

public enum ReplayType { Once = 0, Loop = 1, PingPong = 2 }

public enum CurrentDirection { Forward, Backwards }

public class MovementRecord
{
    public Vector3 movePosition;
    public Quaternion moveRotation;
    public Vector3 moveScale;
} 

public class Movement : MonoBehaviour
{
    [HideInInspector]
    public bool isInReplayMode = false;
    [HideInInspector]
    public bool isInRecordMode = false;
    [HideInInspector]
    public float startReplayTime;
    [HideInInspector]
    public float startRecordTime;
    [HideInInspector]
    public Vector3 startPosition;
    [HideInInspector]
    public Quaternion startRotation;
    [HideInInspector]
    public Vector3 startScale;
    [SerializeField]
    public GameObject moveableObject;
    [HideInInspector]
    public SerializedDictionary<float, MovementRecord> movementTempMap = new SerializedDictionary<float, MovementRecord>();
    [SerializeField]
    public ObjectManipulator objectManipulator;
    [HideInInspector]
    public ReplayType type = ReplayType.Once;
    [HideInInspector]
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
        startPosition = new Vector3(moveableObject.transform.position.x, moveableObject.transform.position.y, moveableObject.transform.position.z);
        startRotation = moveableObject.transform.localRotation;
        startScale = moveableObject.transform.localScale;
    }

    public void StartReplay()
    {
        isInRecordMode = false;
        startReplayTime = Time.time;
        isInReplayMode = true;
        if (type == ReplayType.PingPong && currentDirection == CurrentDirection.Backwards)
        {
            float[] array = movementTempMap.Keys.ToArray();
            moveableObject.transform.position = movementTempMap[array[array.Length - 1]].movePosition;
            moveableObject.transform.localRotation = movementTempMap[array[array.Length - 1]].moveRotation;
            moveableObject.transform.localScale = movementTempMap[array[array.Length - 1]].moveScale;
        }
        else
        {
            moveableObject.transform.position = startPosition;
            moveableObject.transform.localRotation = startRotation;
            moveableObject.transform.localScale = startScale;
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
                    //Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "Checking Start Replay time");
                    index = Array.IndexOf(array, array.OrderBy(a => Math.Abs((Time.time - startReplayTime) - a)).First());
                }

            } else
            {
                if (startReplayTime != null)
                {
                    //Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "Checking Start Replay time");
                    index = Array.IndexOf(array, array.OrderBy(a => Math.Abs((array[array.Length-1] - (Time.time - startReplayTime)) - a)).First());
                }
            }

            //Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "Checking index: " + index);
            moveRec = movementTempMap[array[index]];

            
            if (moveRec != null)
            {
                //Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "Move rec is NOT null");
                moveableObject.transform.position = moveRec.movePosition;
                //Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "Position: " + moveRec.movePosition);
                moveableObject.transform.localRotation = moveRec.moveRotation;
                moveableObject.transform.localScale = moveRec.moveScale;
            }
            else
            {
                //Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "Move rec is null");

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
                movePosition = moveableObject.transform.position,
                moveRotation = moveableObject.transform.localRotation,
                moveScale = moveableObject.transform.localScale
            };
            movementTempMap.Add(Time.time - startRecordTime, mr);
            //Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "movementTempMap count: " + movementTempMap.Count());
        }
    }

}