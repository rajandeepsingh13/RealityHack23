using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Save data for Components
/// </summary>
[Serializable] public struct ComponentData
{
    public Type Dataype;
    public object Data;
}

/// <summary>
/// Save Data for Nodes
/// </summary>
[Serializable] public struct NodeSaveData
{
    public string Guid;
    public string CanvasGuid;
    public ComponentData[] ComponentDataArray;
}

/// <summary>
/// Save data for Node Canvas
/// </summary>
[Serializable] public struct NodeCanvasSaveData
{
    public string Guid;
    public NodeSaveData[] NodeSaveDataArray;
}