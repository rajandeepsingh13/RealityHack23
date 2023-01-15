using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 
/// </summary>
public class LabeledToggle : Component<bool>
{
    #region Inspector Fields
    [SerializeField] private Toggle _toggle;
    #endregion


    #region Public Properties
    #endregion


    #region Event Handlers
    #endregion


    #region Internal Variables
    #endregion


    #region Data Constructs
    #endregion


    #region MonoBehaviour Loop
    private void Awake()
    {
        _toggle.onValueChanged.AddListener(InvokeOnValueChanged);
    }
    #endregion


    #region Internal Functions
    internal override void SetValue(bool value)
    {
        _value = value;
        _toggle.SetIsOnWithoutNotify(value);
        InvokeOnValueChanged(value);
    }
    #endregion


    #region Public API
    #endregion
}