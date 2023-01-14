using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 
/// </summary>
public class LabeledSlider : MonoBehaviour
{
    #region Inspector Fields
    [Header("Components")]
    [SerializeField] private TMP_Text _label;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _value;
    #endregion


    #region Public Properties
    #endregion


    #region Event Handlers
    public event Action<float> OnValueChanged;
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
            OnValueChanged.Invoke(f);
            _value.text = f.ToString();
        });
    }
    #endregion


    #region Internal Functions
    #endregion


    #region Public API
    #endregion
}