using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


/// <summary>
/// 
/// </summary>
public class Component : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField] internal TMP_Text _label;
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
    private void Awake() { }
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