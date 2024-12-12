using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAction : MonoBehaviour
{
    public float strengthMultiplier = 5f;

    Joint joint;

    public Rigidbody otherObject;
    Rigidbody heldObject;

    [SerializeField]
    float dragMassMin = 20f, maxMass;

    public float sizeMultiplier = 1;

    void OnEnable()
    {
        InputManager.interactAction += Grab;
        InputManager.throwAction += Throw;
        InputManager.useAction += Use;
    }

    void Grab()
    {
        if (otherObject && !heldObject)
        {
            if (!joint)
            {
                if(otherObject.mass >= maxMass * sizeMultiplier) { return; }
                heldObject = otherObject;
                //heldObject.useGravity = false;
                heldObject.gameObject.transform.localPosition = transform.position + heldObject.GetComponent<VRObject>().heldOffset;
                heldObject.gameObject.transform.localEulerAngles = heldObject.GetComponent<VRObject>().heldRotation;
                if (heldObject.mass > dragMassMin * sizeMultiplier)
                {
                    joint = gameObject.AddComponent<SpringJoint>();
                }
                else
                {
                    heldObject.useGravity = false;
                    joint = gameObject.AddComponent<FixedJoint>();
                }
                joint.connectedBody = heldObject;
            }
        }
        else if (heldObject)
        {
            Release();
        }
    }

    void Release()
    {
        joint.connectedBody = null;
        Destroy(joint);
        heldObject.useGravity = true;
        heldObject = null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickupable"))
        {
            otherObject = other.gameObject.GetComponent<Rigidbody>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (otherObject)
        {
            otherObject = null;
        }
    }

    void Use()
    {
        if (!heldObject) { return; }

        if (heldObject.TryGetComponent(out VREquipment equipment))
        {
            equipment.Interaction();
        }
    }

    public void Throw()
    {
        if (heldObject)
        {
            joint.connectedBody = null;
            Destroy(joint);
            heldObject.velocity = transform.forward * strengthMultiplier;
            heldObject.useGravity = true;
            heldObject = null;
        }
    }

    void OnDisable()
    {
        InputManager.interactAction -= Grab;
        InputManager.throwAction -= Throw;
        InputManager.useAction -= Use;
    }
}