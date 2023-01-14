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

    Guid guid = Guid.NewGuid();

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
        foreach (node in nodes) {
            node.ExecuteOnUpdate();
        }
    }
}
