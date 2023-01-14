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


    #region Internal Functions
    internal void SetParentCanvas(NodeCanvas nodeCanvas)
    {
        _parentNodeCanvas = nodeCanvas;
    }
    internal abstract void ExecuteOnStart();
    internal abstract void ExecuteOnUpdate();

    internal void InvokeOnStartExecuted() => OnStartExecuted?.Invoke();
    internal void InvokeOnUpdateExecuted() => OnUpdateExecuted?.Invoke();
    #endregion


    #region Public API
    #endregion
}