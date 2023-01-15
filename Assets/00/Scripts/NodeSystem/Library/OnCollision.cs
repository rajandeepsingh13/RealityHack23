using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*namespace Nodes.Library {

}*/


/// <summary>
/// 
/// </summary>
public class OnCollision : NodeBase
{
    #region Inspector Fields
    /*[SerializeField] private LabeledDropdown _colliderTypeDropdown;*/
    [SerializeField] private LabeledToggle _enableCollisionsToggle;
    [SerializeField] private LabeledToggle _isDestroyerToggle;
    [SerializeField] private LabeledSlider _changeScoreSlider;
    [SerializeField] private LabeledToggle _playAudioToggle;
    [SerializeField] private LabeledButton _recordAudioButton;
    [SerializeField] private LabeledButton _playAudioButton;
    [SerializeField] private VoiceRecording _voiceRecording;
    #endregion

    #region Public Properties
    #endregion

    #region Event Handlers
    void ColliderTypeDropdownOnValueChanged(int val) {
        /*Panda p = ProgrammingManager.selectedPanda.GetComponent<Panda>();
        switch (val) {
        case 0:
            p.collisionType = Panda.CollisionType.none;
            foreach(Collider c in p.GetComponentsInChildren<Collider>()) {
                GetComponent<Collider>().isTrigger = false;
            }
            break;
        case 1:
            p.collisionType = Panda.CollisionType.destroyer;
            foreach(Collider c in p.GetComponentsInChildren<Collider>()) {
                GetComponent<Collider>().isTrigger = true;
            }
            break;
        case 2:
            p.collisionType = Panda.CollisionType.destroyable;
            foreach(Collider c in p.GetComponentsInChildren<Collider>()) {
                GetComponent<Collider>().isTrigger = false;
            }
            break;
        default:
            Debug.Log("what the actual fuck");
            break;
        }*/
       
    }
    void EnableCollisionsToggleOnValueChanged(bool val) {
        ProgrammingManager.selectedPanda.GetComponent<Panda>().enableCollisions = false;
    }
    void IsDestroyerToggleOnValueChanged(bool val) {
        Panda p = ProgrammingManager.selectedPanda.GetComponent<Panda>();
        if (val) {
            p.collisionType = Panda.CollisionType.destroyer;
            foreach(Collider c in p.GetComponentsInChildren<Collider>()) {
                GetComponent<Collider>().isTrigger = true;
            }
        } else {
            p.collisionType = Panda.CollisionType.destroyable;
            foreach(Collider c in p.GetComponentsInChildren<Collider>()) {
                GetComponent<Collider>().isTrigger = false;
            }
        }
        //ProgrammingManager.selectedPanda.GetComponent<Panda>().enableCollisions = false;
    }
    void ChangeScoreSliderOnValueChanged(float val) {
        ProgrammingManager.selectedPanda.GetComponent<Panda>().scoreChangeOnCollision = (int) val;
    }
    void PlayAudioToggleOnValueChanged(bool val) {
        ProgrammingManager.selectedPanda.GetComponent<Panda>().playAudioOnCollision = val;
    }
    void RecordAudioButtonOnClick(int _) {
        VoiceRecording.audioSource = ProgrammingManager.selectedPanda.GetComponent<Panda>().collisionAudioSource;
        _voiceRecording.ToggleRecordingState();
    }
    void PlayAudioButtonOnClick(int _) {
        _voiceRecording.TogglePlayingState();
    }
    #endregion


    #region Internal Variables
    enum CollisionType {
        destroyer = 0,
        destroyable = 1
    }
    #endregion


    #region Data Constructs
    #endregion


    #region MonoBehaviour Loop
    private void Awake() {
        //_colliderTypeDropdown.OnValueChanged += ColliderTypeDropdownOnValueChanged;
        _enableCollisionsToggle.OnValueChanged += EnableCollisionsToggleOnValueChanged;
        _isDestroyerToggle.OnValueChanged += IsDestroyerToggleOnValueChanged;
        _changeScoreSlider.OnValueChanged += ChangeScoreSliderOnValueChanged;
        _playAudioToggle.OnValueChanged += PlayAudioToggleOnValueChanged;
        _recordAudioButton.OnValueChanged += RecordAudioButtonOnClick;
        _playAudioButton.OnValueChanged += PlayAudioButtonOnClick;
    }
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