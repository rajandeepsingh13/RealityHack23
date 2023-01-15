using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// 
    /// </summary>
public class PrimitiveCollisions : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        Debug.Log("primitive - collision");
        Panda p = gameObject.transform.parent.gameObject.GetComponent<Panda>();
        if (p != null) {
            p.OnCollision(other);
        } else {
            Debug.Log("primitive collision - panda is null");
        }
    }
}
