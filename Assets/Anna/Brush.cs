using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Brush : MonoBehaviour {
    // Prefab to instantiate when we draw a new brush stroke
    [SerializeField] private GameObject _brushStrokePrefab = null;

    // Which hand should this brush instance track?
    private enum Hand { LeftHand, RightHand };
    [SerializeField] private Hand _hand = Hand.RightHand;

    // Used to keep track of the current brush tip position and the actively drawing brush stroke
    private BrushStrokeNew _activeBrushStroke;

    //public Panda currentPanda;

    private bool enabled = false;

    public GameObject brushTipPrefab;
    private GameObject brushTip;

    public static Material material;

    void Start() {
        brushTip = Instantiate(brushTipPrefab);
    }

    private void Update() {
        brushTip.GetComponentsInChildren<MeshRenderer>()[0].material = material;
        
        // Get the position & rotation of the controller
        Vector3 position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        Quaternion rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);

        // Figure out if the trigger is pressed or not
        float triggerValue = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        bool triggerPressed = triggerValue > 0.1f;
        

        // Get the position & rotation of the controller
        /*Vector3 position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        Quaternion rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);

        // Figure out if the trigger is pressed or not
        float triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        bool triggerPressed = triggerValue > 0.1f;*/

        brushTip.SetActive(enabled);
        brushTip.transform.position = position;
        brushTip.transform.rotation = rotation;

        if (!enabled) { return; }

        // If the trigger is pressed and we haven't created a new brush stroke to draw, create one!
        if (triggerPressed && _activeBrushStroke == null) {
            Debug.Log("start new brush stroke");

            // Instantiate a copy of the Brush Stroke prefab.
            GameObject brushStrokeGameObject = Instantiate(_brushStrokePrefab);
            brushStrokeGameObject.tag = "Primitive";
            //currentPanda.AddMesh(brushStrokeGameObject);

            // Grab the BrushStroke component from it
            _activeBrushStroke = brushStrokeGameObject.GetComponent<BrushStrokeNew>();
            Debug.Log("brush stroke is " + _activeBrushStroke);
            _activeBrushStroke.GetComponentInChildren<MeshRenderer>().material = material;

            // Tell the BrushStroke to begin drawing at the current brush position
            _activeBrushStroke.BeginBrushStrokeWithBrushTipPoint(position, rotation);
        }

        // If the trigger is pressed, and we have a brush stroke, move the brush stroke to the new brush tip position
        if (triggerPressed) {
            Debug.Log("move brush to " + position[0] + " " + position[1] + " " + position[2] + " ");
            _activeBrushStroke.MoveBrushTipToPoint(position, rotation);
        }

        // If the trigger is no longer pressed, and we still have an active brush stroke, mark it as finished and clear it.
        if (!triggerPressed && _activeBrushStroke != null) {
            Debug.Log("end brush stroke");
            _activeBrushStroke.EndBrushStrokeWithBrushTipPoint(position, rotation);
            _activeBrushStroke = null;
        }
    }

    public void ToggleEnabled() {
        enabled = !enabled;
    }
}
