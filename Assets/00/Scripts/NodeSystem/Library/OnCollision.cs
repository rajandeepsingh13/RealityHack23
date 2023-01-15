using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// </summary>
public class OnCollision : NodeBase
{
    #region Inspector Fields
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
    private void Start() { }
    private void Update() { }
    #endregion


    #region Internal Functions
    internal override int GetLibraryID() => 2389;
    
    internal override void ExecuteOnStart()
    {
        
    }
    internal override void ExecuteOnUpdate()
    {
        
    }
    internal override NodeSaveData GetNodeSaveData()
    {
        NodeSaveData saveData = new();
        return saveData;
    }
    internal override void ApplyNodeSaveData(NodeSaveData saveData)
    {
        
    }
    #endregion


    #region Public API
    #endregion
}