using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// 
/// </summary>
public class NodeMenu : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField] private NodeMenuItem _nodeMenuItemPrefab;
    #endregion


    #region Public Properties
    #endregion


    #region Event Handlers
    #endregion


    #region Internal Variables
    private NodeCanvas _currentActiveCanvas;
    #endregion


    #region Data Constructs
    #endregion


    #region MonoBehaviour Loop
    private void Awake()
    {
        NodeLibrary.Instance.OnNodeLibraryInitialized += OnNodeLibraryInitialized;
    }

    private void OnNodeLibraryInitialized()
    {
        var nodeTypes = NodeLibrary.Instance.NodeTypes;
        for (int i = 0; i < nodeTypes.Count; i++)
        {
            Type nodeType = nodeTypes.ElementAt(i).Value;
            NodeMenuItem newItem = Instantiate(_nodeMenuItemPrefab, transform);
            newItem.OnMenuItemSelected += () =>
            {
                // instantiate new node on the canvas
                // close the node menu
            };
        }
    }

    private void Update() { }
    #endregion


    #region Internal Functions
    #endregion


    #region Public API
    #endregion
}