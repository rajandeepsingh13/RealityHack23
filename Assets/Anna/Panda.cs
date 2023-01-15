using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panda : MonoBehaviour
{
    enum PandaType {
        npc,
        controller
    }

    // Meshes making up this Panda are stored as children of the GameObject this script is on

    float creationTimestamp;

    //Guid guid = System.Guid.NewGuid();

    // Panda's associated behaviors
    public List<NodeBase> nodes = new List<NodeBase>();

    public AudioSource spawnAudioSource;
    public AudioSource collisionAudioSource;

    public bool destroyObjectOnCollision = false;
    public bool playAudioOnCollision = false;
    public int scoreChangeOnCollision = 0;

    private void Start()
    {
        spawnAudioSource = gameObject.AddComponent<AudioSource>();
        spawnAudioSource.loop = false;
        collisionAudioSource = gameObject.AddComponent<AudioSource>();
        collisionAudioSource.loop = false;

        foreach (NodeBase node in nodes) {
            node.ExecuteOnStart();
        }
    }

    private void Update()
    {
        foreach (NodeBase node in nodes) {
            node.ExecuteOnUpdate();
        }

        // Test
        if (OVRInput.GetDown(OVRInput.RawButton.A)) {
            Debug.Log("testing collision audio");
            OnCollision(null);
        }
    }

    public void AddMesh(GameObject go) {
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
        if (destroyObjectOnCollision) {
            Destroy(other.gameObject);
        }
        if (playAudioOnCollision) {
            PlayAudioSource(collisionAudioSource);
        }
        GameObject.FindObjectsOfType<Score>()[0].ChangeScoreBy(scoreChangeOnCollision);
    }

    void PlayAudioSource(AudioSource aus) {
        aus.time = VoiceRecording.TIME_TO_SKIP;
        aus.Play();
    }
}
