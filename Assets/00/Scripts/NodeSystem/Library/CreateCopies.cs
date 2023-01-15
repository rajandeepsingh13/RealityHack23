using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// </summary>

namespace Nodes.Library
{
    public class CreateCopies : NodeBase
    {
        #region Inspector Fields
        [SerializeField] private LabeledToggle _enableToggle;
        [SerializeField] private LabeledDropdown _directionDropdown;
        [SerializeField] private LabeledSlider _speedSlider;
        [SerializeField] private LabeledToggle _randomTimeToogle;
        #endregion


        #region Public Properties
        #endregion

        #region Private Properties

        private List<GameObject> spawneesPool;
        private Coroutine spawnCoroutine;
        private bool isFollowing = false;
        private float spawneeSpeed = 0.1f;
        private Vector3 anchorPos;
        private bool randomTimeValue = true;
        private bool anchorPlaced = false;

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
            _speedSlider.OnValueChanged += SpeedSliderOnValueChanged;
            _randomTimeToogle.OnValueChanged += _randomTimeToogle_OnValueChanged;

            spawneesPool = new List<GameObject>();
        }

        

        private void Start() { }
        private void Update() { }
        #endregion


        #region Event Handlers

        private void _randomTimeToogle_OnValueChanged(bool obj)
        {
            randomTimeValue = obj;
        }

        private void SpeedSliderOnValueChanged(float speed)
        {
            spawneeSpeed = speed;
        }
        private void DirectionDropdownOnValueChanged(int obj)
        {
            //If the user selects "Constant follow", set isFollowing to true
            isFollowing = obj == 1;
        }
        private void EnableToggleOnValueChanged(bool obj)
        {
            if (obj)
            {
                Debug.LogWarning("Lqqwdqwwqdqqwddddd");
                spawnCoroutine = StartCoroutine(CreateBabes());
            }
            else
            {
                StopCoroutine(spawnCoroutine);
            }
        }

        private IEnumerator CreateBabes()
        {
            Debug.LogWarning("IsPlayMode" + ProgrammingManager.isPlayMode);
            //if in play mode
            while (ProgrammingManager.isPlayMode)
            {
                if (anchorPlaced)
                {
                    Debug.LogWarning("instnactaed");
                    //Create coorutine that create new copies every x seconds.
                    GameObject enemy = Instantiate(ProgrammingManager.selectedPanda);
                    enemy.transform.position = ProgrammingManager.selectedPanda.transform.position + new Vector3(0.1f, 0.1f, 0.1f);

                    //All the children go in an array
                    spawneesPool.Add(enemy);

                    Debug.LogWarning(spawneesPool.Count);

                }

                if (randomTimeValue)
                {
                    yield return new WaitForSeconds(Random.Range(0.5f, 3f));
                }
                else
                {
                    yield return new WaitForSeconds(1f);
                }
            }
        }
        #endregion


        #region Internal Functions
        internal override int GetLibraryID() => 6794;

        internal override void ExecuteOnStart()
        {
            anchorPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        }

        internal override void ExecuteOnUpdate()
        {
            //isFollowing is false
            //Loop first checks the distance between the child in array and og parrent (static selected panda)
            //If distance too much, destroy
            //else position ++

            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                anchorPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
                anchorPlaced = true;
            }

            foreach (var spawnee in spawneesPool)
            {
                if (!isFollowing)
                {
                    var dist = Vector3.Distance(anchorPos, spawnee.transform.position);

                    if (dist >= 0.01f)
                    {
                        spawnee.transform.position = Vector3.MoveTowards(spawnee.transform.position, anchorPos, spawneeSpeed * Time.deltaTime);
                    }
                    else
                    {
                        spawneesPool.Remove(spawnee);
                        GameObject.Destroy(spawnee);
                    }
                }
                else
                {
                    var dist = Vector3.Distance(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch), spawnee.transform.position);

                    if (dist >= 0.01f)
                    {
                        spawnee.transform.position = Vector3.MoveTowards(spawnee.transform.position, OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch), spawneeSpeed * Time.deltaTime);
                    }
                    else
                    {
                        spawneesPool.Remove(spawnee);
                        GameObject.Destroy(spawnee);
                    }
                }
            }
        }

        internal override ComponentData[] GetAllComponentData()
        {
            // manually get save data from the components we've included
            List<ComponentData> allNodeComponentData = new();
            allNodeComponentData.Add(_enableToggle.GetComponentData()); // 0
            allNodeComponentData.Add(_directionDropdown.GetComponentData()); // 1
            allNodeComponentData.Add(_speedSlider.GetComponentData()); // 3

            return allNodeComponentData.ToArray();
        }
        internal override void SetAllComponentData(ComponentData[] componentDataArray)
        {
            _enableToggle.SetComponentData(componentDataArray[0]);
            _directionDropdown.SetComponentData(componentDataArray[1]);
            _speedSlider.SetComponentData(componentDataArray[2]);
        }
        #endregion


        #region Public API
        #endregion


    }
}