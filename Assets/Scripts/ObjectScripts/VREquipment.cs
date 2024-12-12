using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VREquipment : VRObject
{
    [SerializeField]
    Equipment equipment;

    void Awake()
    {
        if (!equipment)
            equipment = GetComponent<Equipment>();
    }

    public void Interaction()
    {
        if (!equipment) { return; }
        equipment.Action();
    }
}
