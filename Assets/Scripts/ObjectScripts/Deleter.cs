using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deleter : Equipment
{
    [SerializeField]
    GameObject highlightedObject;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickupable"))
        {
            highlightedObject = other.gameObject;
        }
    }

    public override void Action()
    {
        if (!highlightedObject) {   return; }

        Destroy(highlightedObject);
    }
}
