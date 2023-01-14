using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 
/// </summary>
public class LabeledToggle : MonoBehaviour
{
    #region Inspector Fields
    [Header("Components")]
    [SerializeField] private TMP_Text _label;
    [SerializeField] private Toggle _toggle;
    #endregion


    #region Public Properties
    #endregion


    #region Event Handlers
    public event Action<bool> OnToggled;
    #endregion


    #region Internal Variables
    #endregion


    #region Data Constructs
    #endregion


    #region MonoBehaviour Loop
    private void Awake()
    {
        _toggle.onValueChanged.AddListener(OnToggled.Invoke);
    }
    #endregion


    #region Internal Functions
    #endregion


    #region Public API
    #endregion
}