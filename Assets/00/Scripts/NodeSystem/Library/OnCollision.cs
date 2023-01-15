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
    [SerializeField] public GameObject targetObject;
    [SerializeField] private LabeledToggle _destroyToggle;
    [SerializeField] private LabeledSlider _changeScoreSlider;
    [SerializeField] private LabeledToggle _playAudioToggle;
    [SerializeField] private LabeledButton _recordAudioButton;
    [SerializeField] private LabeledButton _playAudioButton;
    [SerializeField] private VoiceRecording _voiceRecording;
    #endregion

    #region Public Properties
    #endregion

    #region Event Handlers
    void DestroyToggleOnValueChanged(bool val) {
        ProgrammingManager.selectedPanda.GetComponent<Panda>().destroyObjectOnCollision = val;
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
    
    #endregion


    #region Data Constructs
    #endregion


    #region MonoBehaviour Loop
    private void Awake() {
        _destroyToggle.OnValueChanged += DestroyToggleOnValueChanged;
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
    internal override void ApplyNodeSaveData(NodeSaveData saveData)
    {
        
    }
    #endregion


    #region Public API
    #endregion
}