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


    #region Event Handlers
    #endregion


    #region Internal Variables
    internal string _guid = "";
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
    internal abstract void ExecuteOnStart();
    internal abstract void ExecuteOnUpdate();
    #endregion


    #region Public API
    #endregion
}