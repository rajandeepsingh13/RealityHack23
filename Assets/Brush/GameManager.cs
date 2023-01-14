using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Brush brush;
    bool newDrawing = true;

    float prevGripR;
    float prevGripL;

    public Material material;

    void Update() {
        // Right trigger - draw
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.1f && newDrawing) {
            //Mesh combinedMesh = drawing.CombineMeshes();
            Debug.Log("begin new drawing");
            GameObject go = new GameObject();
            go.AddComponent<Drawing>();
            brush.currentDrawing = go.GetComponent<Drawing>();
            go.tag = "Drawing";
            newDrawing = false;
        }

        // Left trigger - merge mesh
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.1f) {
            newDrawing = true;
        }

        // Right grip - save 
        float gripR = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
        if (gripR > 0.1f && prevGripR < 0.1f) {
            SaveGame();
        }
        prevGripR = gripR;

        // Left grip - load
        float gripL = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
        if (gripL > 0.1f && prevGripL < 0.1f) {
            LoadGame();
        }
        prevGripL = gripL;
    }

    void SaveGame() {
        List<MeshAndTransform> meshesAndTransforms = new List<MeshAndTransform>();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Drawing")) {
            Drawing drawing = go.GetComponent<Drawing>();
            Mesh combinedMesh = drawing.CombineMeshes();
            Debug.Log("mesh num verts " + combinedMesh.vertexCount);
            SerializableMeshInfo mesh = new SerializableMeshInfo(combinedMesh);
            MeshAndTransform mat = new MeshAndTransform();
            mat.mesh = mesh;
            mat.position = new float[] { drawing.transform.position[0], drawing.transform.position[1], drawing.transform.position[2] };
            mat.eulerAngles = new float[] { drawing.transform.eulerAngles[0], drawing.transform.eulerAngles[1], drawing.transform.eulerAngles[2] };
            Debug.Log("mesh pos " + drawing.transform.position[0] + " " + drawing.transform.position[1] + " " + drawing.transform.position[2]);
            Debug.Log("mesh euler " + mat.eulerAngles);
            meshesAndTransforms.Add(mat);
        }
        
        SaveData sd = new SaveData();
        sd.meshesAndTransforms = (MeshAndTransform[]) meshesAndTransforms.ToArray();

        if (File.Exists(Application.persistentDataPath + "/saveFile.dat")) {
            Debug.Log("deleting existing save file");
            File.Delete(Application.persistentDataPath + "/saveFile.dat");
        }
        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        System.IO.FileStream fs = new System.IO.FileStream(Application.persistentDataPath + "/saveFile.dat", System.IO.FileMode.Create);
        bf.Serialize(fs, sd);
        fs.Close();
    }

    void LoadGame() {
        if (!System.IO.File.Exists(Application.persistentDataPath + "/saveFile.dat"))
        {
            Debug.LogError("saveFile.dat file does not exist.");
        }
        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        System.IO.FileStream fs = new System.IO.FileStream(Application.persistentDataPath + "/saveFile.dat", System.IO.FileMode.Open);
        SaveData sd = (SaveData) bf.Deserialize(fs);
        foreach (MeshAndTransform mat in sd.meshesAndTransforms) {
            SerializableMeshInfo smi = mat.mesh;
            Mesh mesh = smi.GetMesh();
            Debug.Log("mesh num verts " + mesh.vertexCount);
            Debug.Log("mesh pos " + mat.position[0] + " " + mat.position[1] + " " + mat.position[2]);
            Debug.Log("mesh euler " + mat.eulerAngles);

            GameObject go = new GameObject();
            go.AddComponent<MeshFilter>();
            go.GetComponent<MeshFilter>().mesh = mesh;
            go.AddComponent<MeshRenderer>();
            go.GetComponent<MeshRenderer>().material = material;
            go.transform.position = new Vector3(mat.position[0], mat.position[1], mat.position[2]);
            go.transform.eulerAngles = new Vector3(mat.eulerAngles[0], mat.eulerAngles[1], mat.eulerAngles[2]);
        }
        fs.Close();
    }    
}
