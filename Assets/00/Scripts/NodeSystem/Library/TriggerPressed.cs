using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// </summary>
public class TriggerPressed : NodeBase
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
    internal override int GetLibraryID() => 1290;
    
    internal override void ExecuteOnStart()
    {
        
    }
    internal override void ExecuteOnUpdate()
    {
        
    }
    internal override ComponentData[] GetAllComponentData()
    {
        List<ComponentData> allNodeComponentData = new();
        return allNodeComponentData.ToArray();
    }
    internal override void SetAllComponentData(ComponentData[] componentDataArray)
    {
        
    }
    #endregion


    #region Public API
    #endregion
}