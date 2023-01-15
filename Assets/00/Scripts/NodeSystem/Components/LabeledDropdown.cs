using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/// <summary>
/// 
/// </summary>
public class LabeledDropdown : Component<int>
{
    #region Inspector Fields
    [SerializeField] private TMP_Dropdown _dropdown;
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
        _dropdown.onValueChanged.AddListener(InvokeOnValueChanged);
    }
    #endregion


    #region Internal Functions
    internal override void SetValue(int value)
    {
        _dropdown.SetValueWithoutNotify(value);
        InvokeOnValueChanged(value);
    }
    #endregion


    #region Public API
    #endregion
}