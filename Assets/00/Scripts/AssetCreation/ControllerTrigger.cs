using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTrigger : MonoBehaviour
{
    [HideInInspector]
    public GameObject currentPrimitive;

    public void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("TriggerEntered");
        if (other.gameObject.tag == "Primitive")
        {
            Debug.LogWarning("Primitive");
            currentPrimitive = other.gameObject;
        }
        else if (other.gameObject.tag == "PandaComponent")
        {
            Debug.LogWarning("PandaComponent");
            currentPrimitive = other.transform.parent.gameObject;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.LogWarning("TriggerExited");
        if (other.gameObject.tag == "Primitive")
        {
            Debug.LogWarning("Primitive");
            currentPrimitive = null;
        }
        else if (other.gameObject.tag == "PandaComponent")
        {
            Debug.LogWarning("PandaComponent");
            currentPrimitive = null;
        }
    }
}