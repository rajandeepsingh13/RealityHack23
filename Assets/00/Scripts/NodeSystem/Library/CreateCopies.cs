using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// </summary>
public class CreateCopies : NodeBase
{
    #region Inspector Fields
    [SerializeField] private LabeledToggle _enableToggle;
    [SerializeField] private LabeledDropdown _directionDropdown;
    [SerializeField] private LabeledToggle _randomSpeedToggle;
    [SerializeField] private LabeledSlider _speedSlider;
    #endregion


    #region Public Properties
    #endregion

    #region Private Properties

    private IEnumerator 

    #endregion


    #region Event Handlers
    #endregion


    #region Internal Variables
    #endregion


    #region Data Constructs
    #endregion


    #region MonoBehaviour Loop
    private new void Awake()
    {
        base.Awake();
        _enableToggle.OnValueChanged += EnableToggleOnValueChanged;
        _directionDropdown.OnValueChanged += DirectionDropdownOnValueChanged;
        _randomSpeedToggle.OnValueChanged += RandomSpeedToggleOnValueChanged;
        _speedSlider.OnValueChanged += SpeedSliderOnValueChanged;
    }
    private void Start() { }
    private void Update() { }
    #endregion


    #region Event Handlers
    private void SpeedSliderOnValueChanged(float speed)
    {
        
    }
    private void RandomSpeedToggleOnValueChanged(bool obj)
    {
        
    }
    private void DirectionDropdownOnValueChanged(int obj)
    {
        
    }
    private void EnableToggleOnValueChanged(bool obj)
    {
        //Create a duplicate current thingy
        if (obj == true)
        {

        }
    }

    private IEnumerator CreateBabes() {
        //if in play mode
        while (true) {
            break;
            yield return new WaitForSeconds(Random.Range(0.5f, 3f));
        }
    }

    //Create coorutine that create new copies every x seconds.
    //All the children go in an array
    //Loop first checks the distance between the child in array and og parrent (static selected panda)
    //If distance too much, destroy
    //else position ++
    #endregion


    #region Internal Functions
    internal override int GetLibraryID() => 6794;
    
    internal override void ExecuteOnStart()
    {
        
    }
    internal override void ExecuteOnUpdate()
    {
        
    }

    internal override NodeSaveData GetNodeSaveData()
    {
        // manually get save data from the components we've included
        List<ComponentData> allNodeComponentData = new();
        allNodeComponentData.Add(_enableToggle.GetComponentData()); // 0
        allNodeComponentData.Add(_directionDropdown.GetComponentData()); // 1
        allNodeComponentData.Add(_randomSpeedToggle.GetComponentData()); // 2
        allNodeComponentData.Add(_speedSlider.GetComponentData()); // 3
        
        // standard part that should be common for every node
        NodeSaveData saveData = new NodeSaveData();
        saveData.Guid = _guid;
        saveData.CanvasGuid = _parentNodeCanvas._guid;
        saveData.ComponentDataArray = allNodeComponentData.ToArray();

        return saveData;
    }
    internal override void ApplyNodeSaveData(NodeSaveData saveData)
    {
        _enableToggle.SetComponentData(saveData.ComponentDataArray[0]);
        _directionDropdown.SetComponentData(saveData.ComponentDataArray[1]);
        _randomSpeedToggle.SetComponentData(saveData.ComponentDataArray[2]);
        _speedSlider.SetComponentData(saveData.ComponentDataArray[3]);
    }
    #endregion


    #region Public API
    #endregion


}