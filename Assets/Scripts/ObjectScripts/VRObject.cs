using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class VRObject : MonoBehaviour
{
    [SerializeField]
    AudioClip collisionSound;
    public Vector3 heldOffset, heldRotation;

    AudioSource aS;

    void Awake()
    {
        aS = GetComponent<AudioSource>();
        aS.clip = collisionSound;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collisionSound)
            aS.Play();
    }
}
