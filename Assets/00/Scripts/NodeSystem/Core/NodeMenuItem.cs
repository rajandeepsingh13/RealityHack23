using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 
/// </summary>
public class NodeMenuItem : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField] private TMP_Text _label;
    [SerializeField] private Button _button;
    #endregion


    #region Public Properties
    #endregion


    #region Event Handlers
    public event Action OnMenuItemSelected;
    #endregion


    #region Internal Variables
    #endregion


    #region Data Constructs
    #endregion


    #region MonoBehaviour Loop
    private void Awake()
    {
        _button.onClick.AddListener(() => OnMenuItemSelected?.Invoke());
    }
    #endregion


    #region Internal Functions
    internal void SetLabel(string label)
    {
        _label.text = label;
    }
    #endregion


    #region Public API
    #endregion
}