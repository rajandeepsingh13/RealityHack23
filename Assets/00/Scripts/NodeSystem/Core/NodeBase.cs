using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public abstract class NodeBase : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField] private TMP_Text _label;
    #endregion


    #region Public Properties
    #endregion


    #region Events
    public event Action OnStartExecuted;
    public event Action OnUpdateExecuted;
    #endregion


    #region Internal Variables
    internal string _guid = "";
    internal NodeCanvas _parentNodeCanvas;
    #endregion


    #region Data Constructs
    #endregion


    #region MonoBehaviour Loop
    internal virtual void Awake()
    {
        _guid = Guid.NewGuid().ToString();
    }
    #endregion


    #region Event Handlers
    internal void InvokeOnStartExecuted() => OnStartExecuted?.Invoke();
    internal void InvokeOnUpdateExecuted() => OnUpdateExecuted?.Invoke();
    #endregion


    #region Internal Functions
    /// <summary>
    /// This is a hardcoded library placement identifier unique for every node
    /// to ensure proper type loading when loading in save data and reconstructing
    /// a node canvas. If this is changed, previous save data wont get loading in properly.
    /// </summary>
    internal abstract int GetLibraryID();
    
    /// <summary>
    /// The node canvas(only) will call and set this for every node it contains.
    /// </summary>
    internal void SetParentCanvas(NodeCanvas nodeCanvas)
    {
        _parentNodeCanvas = nodeCanvas;
    }
    
    /// <summary>
    /// Gets Called once when a scene starts playing
    /// </summary>
    internal abstract void ExecuteOnStart();
    
    /// <summary>
    /// Gets called every frame when a scene plays
    /// </summary>
    internal abstract void ExecuteOnUpdate();

    internal NodeSaveData GetNodeSaveData()
    {
        // standard part that should be common for every node
        NodeSaveData saveData = new NodeSaveData();
        saveData.LibraryID = GetLibraryID();
        saveData.Guid = _guid;
        saveData.CanvasGuid = _parentNodeCanvas._guid;
        saveData.ComponentDataArray = GetAllComponentData();

        return saveData;
    }
    internal void ApplyNodeSaveData(NodeSaveData saveData)
    {
        if (saveData.LibraryID != GetLibraryID())
        {
            Debug.LogError("This is bad. The library ID must have changed or some wire must've gotten crossed.");
            Debug.LogError("The Library ID needs to match for successful loading of the node from a save file.");
            return;
        }
        _guid = saveData.Guid;
        SetAllComponentData(saveData.ComponentDataArray);
    }
    
    /// <summary>
    /// Manually get save data from the components included in the node scripts
    /// </summary>
    internal abstract ComponentData[] GetAllComponentData();

    /// <summary>
    /// Manually set component data loaded from save file
    /// </summary>
    internal abstract void SetAllComponentData(ComponentData[] componentDataArray);
    #endregion


    #region Public API
    #endregion
}