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
    public int LibraryID;
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
    public string PandaGuid;
    public NodeSaveData[] NodeSaveDataArray;
}


/// <summary>
/// Register this Node Type to be available via the Node Menu
/// <para><strong>MenuTitle:</strong> Name to display for this Node variant</para>
/// <para><strong>Description:</strong> Description of this Nodes function, to display as a tooltip</para>
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class NodeAttribute : Attribute
{
    public string MenuTitle { get; private set; }
    public string Description { get; private set; }

    /// <summary>
    /// Adds this Node type to the Node library menu
    /// </summary>
    /// <param name="menuTitle">Name to display for this Node variant</param>
    /// <param name="description">Description of this Node function, to display as a tooltip</param>
    public NodeAttribute(string menuTitle, string description = default)
    {
        MenuTitle = menuTitle;
        Description = description;
    }
}