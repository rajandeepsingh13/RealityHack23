using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    [SerializeField]
    public MeshAndTransform[] meshesAndTransforms;
}

[System.Serializable]
public class MeshAndTransform
{
    [SerializeField]
    public SerializableMeshInfo mesh;

    [SerializeField]
    public float[] position;

    [SerializeField]
    public float[] eulerAngles;
}
