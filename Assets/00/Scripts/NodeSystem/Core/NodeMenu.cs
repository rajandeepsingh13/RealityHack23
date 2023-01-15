using System.Collections;
using System.Collections.Generic;
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
        
    }

    private void Update() { }
    #endregion


    #region Internal Functions
    #endregion


    #region Public API
    #endregion
}