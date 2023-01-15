using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllerTrigger : MonoBehaviour
{
    [HideInInspector]
    public GameObject currentPrimitive;

    Collider m_Collider;
    Vector3 m_Size;

    void Update()
    {
        m_Collider = GetComponent<Collider>();
        m_Size = m_Collider.bounds.size;
        Vector3 p1 = transform.position;
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, m_Size, Quaternion.identity);
        int i = 0;
        Collider[] orderedByProximity = hitColliders.OrderBy(c => (p1 - c.transform.position).sqrMagnitude).ToArray();
        while (i < orderedByProximity.Length)
        {
            if (orderedByProximity[i].gameObject.tag == "PandaComponent" || orderedByProximity[i].gameObject.tag == "Primitive")
            {
                currentPrimitive = orderedByProximity[i].gameObject;
                break;
            }
            i++;
        }      
    }
}