using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// 
/// </summary>
public class NodeCanvas : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField] private Transform _container;
    #endregion


    #region Public Properties
    #endregion


    #region Event Handlers
    #endregion


    #region Internal Variables
    private GameObject _objectToActOn;
    
    private List<NodeBase> _containedNodes = new();
    #endregion


    #region Data Constructs
    #endregion


    #region MonoBehaviour Loop
    private void Awake()
    {
        // check if we've added any preset node functions to the canvas
        _containedNodes = _container.GetComponentsInChildren<NodeBase>(true).ToList();
    }
    private void Start()
    {
        // call all the contained nodes start functions
        for (int i = 0; i < _containedNodes.Count; i++)
        {
            _containedNodes[i].ExecuteOnStart();
        }
    }
    private void Update()
    {
        // call all the contained nodes update functions
        for (int i = 0; i < _containedNodes.Count; i++)
        {
            _containedNodes[i].ExecuteOnUpdate();
        }
    }
    #endregion


    #region Internal Functions
    #endregion


    #region Public API
    #endregion
}