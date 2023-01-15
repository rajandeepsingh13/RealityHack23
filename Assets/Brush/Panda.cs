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

    private void Start()
    {
        foreach (NodeBase node in nodes) {
            node.ExecuteOnStart();
        }
    }

    private void Update()
    {
        foreach (NodeBase node in nodes) {
            node.ExecuteOnUpdate();
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
}
