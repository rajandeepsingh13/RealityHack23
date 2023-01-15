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
    /// a node canvas. if this is changed, previous save data wont get loading in properly.
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

    internal abstract NodeSaveData GetNodeSaveData();
    internal abstract void ApplyNodeSaveData(NodeSaveData saveData);
    #endregion


    #region Public API
    #endregion
}