using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeInteractor : MonoBehaviour
{
    [SerializeField]
    float distance = 5f;
    [SerializeField]
    float heldTime = 5f;
    float timer = 0;
    IRespondToGaze currentGazedObject;
    Color rayColor;

    void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distance))
        {
            if(hit.collider.gameObject.GetComponent<IRespondToGaze>() != null)
            {
                rayColor = Color.green;
                if(hit.collider.gameObject.GetComponent<IRespondToGaze>() != currentGazedObject)
                {
                    currentGazedObject = hit.collider.gameObject.GetComponent<IRespondToGaze>();
                    currentGazedObject.StartGaze();
                }
                else
                {
                    timer += Time.fixedDeltaTime;
                    if(timer > heldTime)
                    {
                        timer -= timer;
                        currentGazedObject.HeldGaze();
                    }
                }
            }
            else
            {
                rayColor = Color.red;
                StopInteract();
            }
        }
        else
        {
            rayColor = Color.black;
            StopInteract();
        }
        Debug.DrawRay(transform.position, transform.forward * distance, rayColor);
    }

    void StopInteract()
    {
        if(currentGazedObject != null)
        {
            timer -= timer;
            rayColor = Color.black;
            currentGazedObject.StopGaze();
            currentGazedObject = null;
        }
    }
}
