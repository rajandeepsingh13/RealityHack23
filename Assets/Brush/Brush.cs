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
    private Vector3     _handPosition;
    private Quaternion  _handRotation;
    private BrushStroke _activeBrushStroke;

    public Drawing currentDrawing;

    private void Update() {
        // Start by figuring out which hand we're tracking
        XRNode node    = _hand == Hand.LeftHand ? XRNode.LeftHand : XRNode.RightHand;
        string trigger = _hand == Hand.LeftHand ? "Left Trigger" : "Right Trigger";

        // Get the position & rotation of the hand
        bool handIsTracking = UpdatePose(node, ref _handPosition, ref _handRotation);

        // Figure out if the trigger is pressed or not
        float triggerValue = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        bool triggerPressed = triggerValue > 0.1f;

        // If we lose tracking, stop drawing
        if (!handIsTracking)
            triggerPressed = false;

        if (currentDrawing == null) { return; }

        // If the trigger is pressed and we haven't created a new brush stroke to draw, create one!
        if (triggerPressed && _activeBrushStroke == null) {
            StartBrushStroke();
        }

        // If the trigger is pressed, and we have a brush stroke, move the brush stroke to the new brush tip position
        if (triggerPressed)
            _activeBrushStroke.MoveBrushTipToPoint(_handPosition, _handRotation);

        // If the trigger is no longer pressed, and we still have an active brush stroke, mark it as finished and clear it.
        if (!triggerPressed && _activeBrushStroke != null) {
            _activeBrushStroke.EndBrushStrokeWithBrushTipPoint(_handPosition, _handRotation);
            _activeBrushStroke = null;
        }
    }

    private void StartBrushStroke() {
        Debug.Log("start new brush stroke");

        // Instantiate a copy of the Brush Stroke prefab.
        GameObject brushStrokeGameObject = Instantiate(_brushStrokePrefab);
        currentDrawing.AddStroke(brushStrokeGameObject);

        // Grab the BrushStroke component from it
        _activeBrushStroke = brushStrokeGameObject.GetComponent<BrushStroke>();

        // Tell the BrushStroke to begin drawing at the current brush position
        _activeBrushStroke.BeginBrushStrokeWithBrushTipPoint(_handPosition, _handRotation);
    }

    //// Utility

    // Given an XRNode, get the current position & rotation. If it's not tracking, don't modify the position & rotation.
    private static bool UpdatePose(XRNode node, ref Vector3 position, ref Quaternion rotation) {
        position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
        return true;
    }
}
