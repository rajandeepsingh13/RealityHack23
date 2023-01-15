using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelSceneManager : MonoBehaviour
{
    public static int currentColor;

    public static int currentAxis = 0;

    public Material[] allMaterials;
    public Image[] imagesToUpdate;

    OVRInput.Controller controller = OVRInput.Controller.RTouch;

    public ControllerTrigger controllerTrigger;

    public GameObject sphere;
    public GameObject cube;
    public GameObject capsule;

    private bool isManipulating = false;
    private bool isRegrab = false;
    private GameObject manipulatingObject;

    public GameObject createCanvas;

    // Start is called before the first frame update
    void Start()
    {
        SelectColor(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isManipulating)
        {
            Vector3 controllerPos = OVRInput.GetLocalControllerPosition(controller);
            Quaternion controllerRot = OVRInput.GetLocalControllerRotation(controller);
            manipulatingObject.transform.position = controllerPos;
            manipulatingObject.transform.rotation = controllerRot;

            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller))
            {
                isManipulating = false;
                manipulatingObject = null;
                return;
            }

            if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controller) && isRegrab)
            {
                isManipulating = false;
                manipulatingObject = null;
                isRegrab = false;
                return;
            }

            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                currentAxis = (currentAxis + 1) % 3;
            }

            if (OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).y > 0.8)
            {
                switch (currentAxis)
                {
                    case 0:
                        manipulatingObject.transform.localScale = new Vector3(manipulatingObject.transform.localScale.x + 0.01f, manipulatingObject.transform.localScale.y, manipulatingObject.transform.localScale.z);
                        break;
                    case 1:
                        manipulatingObject.transform.localScale = new Vector3(manipulatingObject.transform.localScale.x, manipulatingObject.transform.localScale.y + 0.01f, manipulatingObject.transform.localScale.z);
                        break;
                    case 2:
                        manipulatingObject.transform.localScale = new Vector3(manipulatingObject.transform.localScale.x, manipulatingObject.transform.localScale.y, manipulatingObject.transform.localScale.z + 0.01f);
                        break;
                }
            }

            if (OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).y < -0.8)
            {
                switch (currentAxis)
                {
                    case 0:
                        manipulatingObject.transform.localScale = new Vector3(manipulatingObject.transform.localScale.x - 0.01f, manipulatingObject.transform.localScale.y, manipulatingObject.transform.localScale.z);
                        break;
                    case 1:
                        manipulatingObject.transform.localScale = new Vector3(manipulatingObject.transform.localScale.x, manipulatingObject.transform.localScale.y - 0.01f, manipulatingObject.transform.localScale.z);
                        break;
                    case 2:
                        manipulatingObject.transform.localScale = new Vector3(manipulatingObject.transform.localScale.x, manipulatingObject.transform.localScale.y, manipulatingObject.transform.localScale.z - 0.01f);
                        break;
                }
            }
        }
        else
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controller) && controllerTrigger.currentPrimitive)
            {
                isManipulating = true;
                isRegrab = true;
                manipulatingObject = controllerTrigger.currentPrimitive;
            }
        }
    }

    public void SelectColor(int color)
    {
        //Change color of prefab buttons
        currentColor = color;
        foreach (var image in imagesToUpdate)
        {
            image.color = allMaterials[color].color;
        }
    }

    public void OnCreateShpere()
    {
        manipulatingObject = Instantiate(sphere, OVRInput.GetLocalControllerPosition(controller), Quaternion.identity);
        manipulatingObject.GetComponent<Renderer>().material = allMaterials[currentColor];
        isManipulating = true;
    }

    public void OnCreateCube()
    {
        manipulatingObject = Instantiate(cube, OVRInput.GetLocalControllerPosition(controller), Quaternion.identity);
        manipulatingObject.GetComponent<Renderer>().material = allMaterials[currentColor];
        isManipulating = true;
    }

    public void OnCreateCapsule()
    {
        manipulatingObject = Instantiate(capsule, OVRInput.GetLocalControllerPosition(controller), Quaternion.identity);
        manipulatingObject.GetComponent<Renderer>().material = allMaterials[currentColor];
        isManipulating = true;
    }


    public void Merge()
    {
        Debug.Log("merging");
        var primitives = GameObject.FindGameObjectsWithTag("Primitive");
        Debug.Log("prims are " + primitives);
        if (primitives.Length == 0) {
            Debug.Log("no prims");
            return;
        }
        var centerOfPandaParent = new Vector3(0f, 0f, 0f);
        foreach (var prim in primitives)
        {
            centerOfPandaParent += prim.transform.position;
            prim.tag = "PandaComponent";
        }
        centerOfPandaParent /= primitives.Length;
        var pandaParent = new GameObject("Panda");
        pandaParent.AddComponent<Panda>();
        pandaParent.transform.position = centerOfPandaParent;
        pandaParent.tag = "Panda";
        foreach (var prim in primitives)
        {
            prim.tag = "PandaComponent";
            prim.transform.SetParent(pandaParent.transform);
        }

        //Disable create
        Debug.Log("set create canvas active false");
        createCanvas.SetActive(false);

        //enable the progtramiing canvas
        ProgrammingManager pm = GameObject.FindObjectsOfType<ProgrammingManager>()[0];
        if (pm != null) {
            pm.programmingCanvas.SetActive(true);
        }

        //Static variable selectedPanda. = pandaParent
        ProgrammingManager.selectedPanda = pandaParent;
    }
}
