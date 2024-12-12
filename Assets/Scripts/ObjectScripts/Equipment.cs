using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VREquipment))]
public abstract class Equipment : MonoBehaviour
{
    abstract public void Action();
}
