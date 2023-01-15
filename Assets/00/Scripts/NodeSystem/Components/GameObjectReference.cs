using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 
/// </summary>
public class GameObjectReference : Component
{
    #region Inspector Fields
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _referenceLabel;
    #endregion


    #region Public Properties
    #endregion


    #region Events
    #endregion


    #region Internal Variables
    private GameObject _localReference;
    #endregion


    #region Data Constructs
    #endregion


    #region MonoBehaviour Loop
    private void Awake()
    {
        _button.onClick.AddListener(LaunchSelector);
    }
    #endregion


    #region Internal Functions
    private void LaunchSelector()
    {
        // the logic for selecting an object should happen here
        //GameObject theSelectedObject;
        //SetSelected(theSelectedObject); //set the selected gameObject
    }
    private void SetSelected(GameObject selected)
    {
        _localReference = selected;
        _referenceLabel.text = selected.name;
    }
    #endregion


    #region Public API
    #endregion
}