using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    [SerializeField]
    public MeshAndTransform[] meshesAndTransforms;
}

[Serializable]
public class MeshAndTransform
{
    [SerializeField]
    public SerializableMeshInfo mesh;

    [SerializeField]
    public float[] position;

    [SerializeField]
    public float[] eulerAngles;
}
