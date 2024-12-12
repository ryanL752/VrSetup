using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour {
    [SerializeField]
    float bladeSpeed;
    [SerializeField]
    Transform mainBlades,tailBlades;


    void Update() {
        RotateBlades();
    }

    void RotateBlades() {
        mainBlades.Rotate(Vector3.forward * bladeSpeed * Time.deltaTime);
        tailBlades.Rotate(Vector3.right * bladeSpeed * Time.deltaTime);
    }

}
