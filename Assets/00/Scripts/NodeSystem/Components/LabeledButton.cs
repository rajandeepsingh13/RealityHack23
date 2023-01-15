using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 
/// </summary>
public class LabeledButton : Component
{
    #region Inspector Fields
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _buttonLabel;
    #endregion


    #region Public Properties
    #endregion


    #region Event Handlers
    public event Action OnButtonClicked;
    #endregion


    #region Internal Variables
    #endregion


    #region Data Constructs
    #endregion


    #region MonoBehaviour Loop
    private void Awake()
    {
        _button.onClick.AddListener(() => OnButtonClicked?.Invoke());
    }
    #endregion


    #region Internal Functions
    internal void SetButton(string buttonLabel, Action onClick = null)
    {
        _buttonLabel.text = buttonLabel;
        _button.onClick.AddListener(() => onClick?.Invoke());
    }
    #endregion


    #region Public API
    #endregion
}