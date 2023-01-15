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

    private List<GameObject> spawneesPool;
    private Coroutine spawnCoroutine;
    private bool isFollowing = false;
    private float spawneeSpeed = 0.1f;

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

        spawneesPool = new List<GameObject>();
    }
    private void Start() { }
    private void Update() { }
    #endregion


    #region Event Handlers
    private void SpeedSliderOnValueChanged(float speed)
    {
        spawneeSpeed = speed;
    }
    private void RandomSpeedToggleOnValueChanged(bool obj)
    {
        
    }
    private void DirectionDropdownOnValueChanged(int obj)
    {
        //If the user selects "Constant follow", set isFollowing to true
        isFollowing = obj == 1;

        if (!isFollowing)
        {

        }
    }
    private void EnableToggleOnValueChanged(bool obj)
    {
        if (obj)
        {
            spawnCoroutine = StartCoroutine(CreateBabes());
        }
        else
        {
            StopCoroutine(spawnCoroutine);
        }
    }
    
    private IEnumerator CreateBabes() {
        //if in play mode
        while (ProgrammingManager.isPlayMode) {

            //Create coorutine that create new copies every x seconds.
            GameObject enemy = Instantiate(ProgrammingManager.selectedPanda);
            //All the children go in an array
            spawneesPool.Add(enemy);

            yield return new WaitForSeconds(Random.Range(0.5f, 3f));
        }
    }
    #endregion


    #region Internal Functions
    internal override int GetLibraryID() => 6794;
    
    internal override void ExecuteOnStart()
    {
        
    }
    internal override void ExecuteOnUpdate()
    {
        //Loop first checks the distance between the child in array and og parrent (static selected panda)
        //If distance too much, destroy
        //else position ++
    }

    internal override ComponentData[] GetAllComponentData()
    {
        // manually get save data from the components we've included
        List<ComponentData> allNodeComponentData = new();
        allNodeComponentData.Add(_enableToggle.GetComponentData()); // 0
        allNodeComponentData.Add(_directionDropdown.GetComponentData()); // 1
        allNodeComponentData.Add(_randomSpeedToggle.GetComponentData()); // 2
        allNodeComponentData.Add(_speedSlider.GetComponentData()); // 3
        
        return allNodeComponentData.ToArray();
    }
    internal override void SetAllComponentData(ComponentData[] componentDataArray)
    {
        _enableToggle.SetComponentData(componentDataArray[0]);
        _directionDropdown.SetComponentData(componentDataArray[1]);
        _randomSpeedToggle.SetComponentData(componentDataArray[2]);
        _speedSlider.SetComponentData(componentDataArray[3]);
    }
    #endregion


    #region Public API
    #endregion


}