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
    private int _previewDecimalPlaces = 2;
    #endregion


    #region Data Constructs
    #endregion


    #region MonoBehaviour Loop
    private void Awake()
    {
        _slider.onValueChanged.AddListener(f =>
        {
            _value = f;
            _valueLabel.text = Truncate(f, _previewDecimalPlaces).ToString();
            InvokeOnValueChanged(f);
        });
    }

    internal override void SetValue(float value)
    {
        _value = value;
        _valueLabel.text = Truncate(value, _previewDecimalPlaces).ToString();
        _slider.SetValueWithoutNotify(value);
        InvokeOnValueChanged(value);
    }
    #endregion


    #region Internal Functions
    public float Truncate(float value, int digits)
    {
        double mult = Math.Pow(10.0, digits);
        double result = Math.Truncate(mult * value) / mult;
        return (float)result;
    }
    #endregion


    #region Public API
    #endregion
}