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
    public GameObject PandaGameObject => _pandaGameObject;
    public Transform PandaTransform => _pandaTransform;
    #endregion


    #region Event Handlers
    #endregion


    #region Internal Variables
    private GameObject _pandaGameObject;
    private Transform _pandaTransform;
    
    private List<NodeBase> _containedNodes = new();
    #endregion


    #region Data Constructs
    #endregion


    #region MonoBehaviour Loop
    private void Awake()
    {
        // check if we've added any preset node functions to the canvas
        _containedNodes = _container.GetComponentsInChildren<NodeBase>(true).ToList();
        for (int i = 0; i < _containedNodes.Count; i++)
        {
            _containedNodes[i].SetParentCanvas(this);
        }
    }
    private void Start()
    {
        // call all the contained nodes start functions
        for (int i = 0; i < _containedNodes.Count; i++)
        {
            var node = _containedNodes[i];
            node.ExecuteOnStart();
            node.InvokeOnStartExecuted();
        }
    }
    private void Update()
    {
        // call all the contained nodes update functions
        for (int i = 0; i < _containedNodes.Count; i++)
        {
            var node = _containedNodes[i];
            node.ExecuteOnUpdate();
            node.InvokeOnUpdateExecuted();
        }
    }
    #endregion


    #region Internal Functions
    internal void SetPandaObject(GameObject pandaObject)
    {
        _pandaGameObject = pandaObject;
        _pandaTransform = pandaObject.transform;
    }
    #endregion


    #region Public API
    #endregion
}