using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 
/// </summary>
public class LabeledButton : Component<int>
{
    #region Inspector Fields
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _buttonLabel;
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
        _button.onClick.AddListener(() => InvokeOnValueChanged(0));
    }
    #endregion


    #region Internal Functions
    internal void SetButton(string buttonLabel, Action onClick = null)
    {
        _buttonLabel.text = buttonLabel;
        _button.onClick.AddListener(() => onClick?.Invoke());
    }
    internal override void SetValue(int value)
    {
        // don't really need to set anything here because this is just a button and has no value.
    }
    #endregion


    #region Public API
    #endregion
}