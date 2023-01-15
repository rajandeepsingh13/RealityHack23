using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Nodes.Library
{
    /// <summary>
    /// 
    /// </summary>
    [Node("PathAnimation")]
    public class PathAnimation : NodeBase
    {
        #region Component Fields
        [SerializeField] LabeledButton _enableButton;
        [SerializeField] LabeledButton _disableButton;
        [SerializeField] LabeledDropdown _modeDropdown;

        [SerializeField] private MoveController _movementController;
        [SerializeField] private Movement _movement;
        #endregion


        #region Internal Variables
        private LoopingType _loopingType = 0;
        #endregion

        #region Data Constructs
        internal enum LoopingType
        {
            Once = 0,
            Loop = 1,
            PingPong = 2
        }
        #endregion

        #region MonoBehaviour Loop
        private new void Awake()
        {
            base.Awake();
            _enableButton.OnValueChanged += OnEnableClicked;
            _disableButton.OnValueChanged += OnDisableClicked;
            _modeDropdown.OnValueChanged += OnDropDownSelected;
        }

        private void OnDropDownSelected(int loopType)
        {
            Debug.Log("Setting Movement Type: " + loopType);
            _loopingType = (LoopingType)loopType;
            _movement.type = (ReplayType)loopType;
        }

        private void OnEnableClicked(int _)
        {
            _movementController.EnableRecord();
            _enableButton.gameObject.SetActive(false);
            _disableButton.gameObject.SetActive(true);
        }

        private void OnDisableClicked(int _)
        {
            _movementController.DisableRecord();
            _movement.StopReplay();
            _disableButton.gameObject.SetActive(false);
            _enableButton.gameObject.SetActive(true);
        }

        // Start is called before the first frame update
        internal override void ExecuteOnStart()
        {
            
        }
        
        // Update is called once per frame
        internal override void ExecuteOnUpdate()
        {
            
        }
        #endregion
        
        
        #region Inherited NodeBase Behaviours
        // Set this to a new unique 4 or more digit number, and never change it
        internal override int GetLibraryID() => 0000;
        
        internal override ComponentData[] GetAllComponentData()
        {
            List<ComponentData> allNodeComponentData = new();
            allNodeComponentData.Add(_modeDropdown.GetComponentData()); // 0

            ComponentData animData = new();
            animData.Dataype = _movement.movementTempMap.GetType();
            animData.Data = _movement.movementTempMap;
            allNodeComponentData.Add(animData); // 1
            return allNodeComponentData.ToArray();
        }
        internal override void SetAllComponentData(ComponentData[] componentDataArray)
        {
            _modeDropdown.SetComponentData(componentDataArray[0]);
            _movement.movementTempMap = componentDataArray[1].Data as SerializedDictionary<float, MovementRecord>;
        }
        #endregion
    }
}
