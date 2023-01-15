using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


/// <summary>
/// 
/// </summary>
public class Panda : MonoBehaviour
{
    public bool enableCollisions = false;
    public enum CollisionType { // Only applies if collisions ARE enabled
        destroyer,
        destroyable
    }

    #region Inspector Fields
    [SerializeField] internal AudioSource _spawnAudioSource;
    [SerializeField] internal AudioSource _collisionAudioSource;

    [SerializeField] internal bool destroyObjectOnCollision = false;
    [SerializeField] internal bool playAudioOnCollision = false;
    [SerializeField] internal int scoreChangeOnCollision = 0;
    #endregion


    #region Public Properties
    public GameObject GameObject => _gameObject;
    public Transform Transform => _transform;
    #endregion


    #region Events
    #endregion


    #region Internal Variables
    private GameObject _gameObject;
    private Transform _transform;
    
    internal NodeCanvas _localNodeCanvas;
    internal float _creationTimestamp;
    internal string _guid;
    #endregion

    public CollisionType collisionType = CollisionType.destroyer;

    // Meshes making up this Panda are stored as children of the GameObject this script is on

    #region MonoBehaviour Loop
    private void Awake()
    {
        _gameObject = gameObject;
        _transform = transform;
        //_localNodeCanvas = NodeLibrary.Instance.GetFreshCanvas();
        
        _spawnAudioSource = gameObject.AddComponent<AudioSource>();
        _spawnAudioSource.loop = false;
        _collisionAudioSource = gameObject.AddComponent<AudioSource>();
        _collisionAudioSource.loop = false;
    }

    private void Update()
    {
        // Test
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            Debug.Log("testing collision audio");
            OnCollision(null);
        }
    }
    #endregion


    #region Internal Behaviours
    internal void SetGuid(string guid)
    {
        _guid = guid;
    }
    private void PlayAudioSource(AudioSource aus)
    {
        aus.time = VoiceRecording.TIME_TO_SKIP;
        aus.Play();
    }
    #endregion


    #region Public API
    // Meshes making up this Panda are stored as children of the GameObject this script is on
    public void AddMesh(GameObject go)
    {
        go.transform.parent = gameObject.transform;
    }
    public Mesh CombineMeshes()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }

        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine);
        return mesh;
    }

    public void OnCollision(Collider other) {
        if (!enableCollisions) {
            return;
        }
        Debug.Log("panda - collision");
        Panda p = other.gameObject.transform.parent.gameObject.GetComponent<Panda>();
        if ((collisionType == CollisionType.destroyer && p.collisionType == CollisionType.destroyable) ||
         (collisionType == CollisionType.destroyable && p.collisionType == CollisionType.destroyer)) {
            if (collisionType == CollisionType.destroyer) {
                Destroy(other.gameObject);
            }
            GameObject.FindObjectsOfType<Score>()[0].ChangeScoreBy(scoreChangeOnCollision);
            PlayAudioSource(_collisionAudioSource);
        }
    }
    #endregion
}