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
    #endregion


    #region Public Properties
    public bool IsPlay => _play;
    public Panda PandaObject => _pandaObject;
    #endregion


    #region Event Handlers
    #endregion


    #region Internal Variables
    internal string _guid = "";
    private Panda _pandaObject;

    private bool _play = true;
    
    private List<NodeBase> _containedNodes = new();
    #endregion


    #region Data Constructs
    #endregion


    #region MonoBehaviour Loop
    private void Awake()
    {
        _guid = Guid.NewGuid().ToString();
        
        // check if we've added any preset node functions to the canvas
        _containedNodes = gameObject.GetComponentsInChildren<NodeBase>(true).ToList();
        for (int i = 0; i < _containedNodes.Count; i++)
        {
            _containedNodes[i].SetParentCanvas(this);
        }
    }
    private void Update()
    {
        if (!_play) return;
        
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
    internal void SetPandaObject(Panda pandaObject)
    {
        _pandaObject = pandaObject;
    }

    internal NodeCanvasSaveData GetSaveData()
    {
        int nodeCount = _containedNodes.Count;
        NodeCanvasSaveData saveData = new();
        NodeSaveData[] nodeSaveDataArray = new NodeSaveData[nodeCount];
        
        for (int i = 0; i < _containedNodes.Count; i++)
        {
            nodeSaveDataArray[i] = _containedNodes[i].GetNodeSaveData();
        }

        saveData.NodeSaveDataArray = nodeSaveDataArray;
        saveData.Guid = _guid;
        saveData.PandaGuid = _pandaObject._guid;

        return saveData;
    }
    
    internal void ApplySaveData(NodeCanvasSaveData saveData)
    {
        _guid = saveData.Guid;
        //TODO: Finish loading here
    }
    #endregion


    #region Public API
    public void SetGuid(string guid)
    {
        _guid = guid;
    }
    public void AddNode(NodeBase node)
    {
        _containedNodes.Add(node);
    }

    public void Play()
    {
        // call all the contained nodes start functions
        for (int i = 0; i < _containedNodes.Count; i++)
        {
            var node = _containedNodes[i];
            node.ExecuteOnStart();
            node.InvokeOnStartExecuted();
        }
        
        _play = true;
    }
    public void Pause()
    {
        _play = false;
    }
    #endregion
}