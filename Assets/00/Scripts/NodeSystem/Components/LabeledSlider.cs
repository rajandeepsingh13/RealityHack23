using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 
/// </summary>
public class LabeledSlider : Component<float>
{
    #region Inspector Fields
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _valueLabel;
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
        _slider.onValueChanged.AddListener(f =>
        {
            _value = f;
            _valueLabel.text = f.ToString();
            InvokeOnValueChanged(f);
        });
    }

    internal override void SetValue(float value)
    {
        _value = value;
        _valueLabel.text = value.ToString();
        _slider.SetValueWithoutNotify(value);
        InvokeOnValueChanged(value);
    }
    #endregion


    #region Internal Functions
    #endregion


    #region Public API
    #endregion
}