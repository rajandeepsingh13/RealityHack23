using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


/// <summary>
/// 
/// </summary>
public abstract class Component<T> : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField] internal TMP_Text _label;
    #endregion


    #region Public Properties
    #endregion


    #region Event Handlers
    public event Action<T> OnValueChanged;
    #endregion


    #region Internal Variables
    internal T _value;
    #endregion


    #region Data Constructs
    #endregion


    #region MonoBehaviour Loop
    private void Awake() { }
    #endregion


    #region Internal Functions
    internal void SetLabel(string label)
    {
        _label.text = label;
    }
    internal abstract void SetValue(T value);
    internal T GetValue() => _value;

    internal void InvokeOnValueChanged(T value)
    {
        OnValueChanged?.Invoke(value);
    }
    #endregion


    #region Public API
    #endregion
}