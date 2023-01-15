using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/// <summary>
/// 
/// </summary>
public class LabeledDropdown : Component
{
    #region Inspector Fields
    [SerializeField] private TMP_Dropdown _dropdown;
    #endregion


    #region Public Properties
    #endregion


    #region Event Handlers
    public event Action<int> OnValueChanged;
    #endregion


    #region Internal Variables
    #endregion


    #region Data Constructs
    #endregion


    #region MonoBehaviour Loop
    private void Awake()
    {
        _dropdown.onValueChanged.AddListener(OnValueChanged.Invoke);
    }
    #endregion


    #region Internal Functions
    #endregion


    #region Public API
    #endregion
}