using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 
/// </summary>
public class Move : NodeBase
{
    #region Inspector Fields
    [SerializeField] private LabeledDropdown _axisDropdown;
    [SerializeField] private LabeledSlider _speedSlider;
    #endregion


    #region Public Properties
    #endregion


    #region Events
    #endregion


    #region Internal Variables
    private Axis _axis = 0;
    private float _speed = 0.1f;
    #endregion


    #region Data Constructs
    internal enum Axis
    {
        x = 0,
        y = 1,
        z = 2
    }
    #endregion


    #region MonoBehaviour Loop
    private void Start()
    {
        _axisDropdown.OnValueChanged += AxisDropdownOnValueChanged;
        _speedSlider.OnValueChanged += SpeedSliderOnValueChanged;
    }
    #endregion


    #region Event Handlers
    private void SpeedSliderOnValueChanged(float speed)
    {
        _speed = speed;
    }
    private void AxisDropdownOnValueChanged(int axisValue)
    {
        _axis = (Axis)axisValue;
    }
    #endregion


    #region Internal Functions
    internal override void ExecuteOnStart()
    {
        
    }
    internal override void ExecuteOnUpdate()
    {
        Vector3 currentPos = _parentNodeCanvas.PandaTransform.position;
        
        switch (_axis)
        {
            case Axis.x:
                currentPos.x += _speed;
                break;
            case Axis.y:
                currentPos.y += _speed;
                break;
            case Axis.z:
                currentPos.z += _speed;
                break;
        }

        _parentNodeCanvas.PandaTransform.position = currentPos;
    }
    #endregion


    #region Public API
    #endregion
}