using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerTeleportation : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    float maxDistance = 10f;
    const string floorTag = "Floor";
    Collider currentFloor;
    Vector3 currentPoint = Vector3.zero;
    Color rayColor;
    LineRenderer lineRen;
    List<GameObject> floors;

    [SerializeField]
    AudioClip charging, error, warp;

    [SerializeField]
    AudioSource audioSource;

    bool started = false;

    void Awake()
    {
        lineRen = GetComponent<LineRenderer>();
        lineRen.positionCount = 2;
        lineRen.enabled = false;
    }

    void Start()
    {
        floors = new();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(floorTag))
        {
            floors.Add(obj);
            obj.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void OnEnable()
    {
        InputManager.interactAction += StartTeleport;
        InputManager.interactAction += EndTeleport;
    }

    void StartTeleport()
    {
        if (!started)
        {
            if (currentPoint == Vector3.zero)
            {
                ChangeFloorVisiblity(true);
                audioSource.clip = charging;
                audioSource.Play();
                started = true;
            }
        }
        else
        {
            audioSource.Stop();
            audioSource.PlayOneShot(warp);
        }
    }

    void ChangeFloorVisiblity(bool state)
    {
        foreach (GameObject obj in floors)
        {
            obj.GetComponent<MeshRenderer>().enabled = state;
        }
    }

    void EndTeleport()
    {
        if (started)
        {
            if (currentPoint != Vector3.zero)
            {
                player.position = currentPoint;
                lineRen.enabled = false;
                ChangeFloorVisiblity(false);
                started = false;
            }
            else
            {
                audioSource.Stop();
                audioSource.PlayOneShot(error);
            }
        }
    }

    void ChangeVisuals(bool state, Color color, Vector3 pos)
    {
        rayColor = color;
        currentPoint = pos;
        lineRen.enabled = state;
        ChangeFloorVisiblity(state);

    }

    void FixedUpdate()
    {
        if (started)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance))
            {
                currentPoint = hit.point;
                if (hit.collider.CompareTag(floorTag))
                {
                    lineRen.enabled = true;
                    rayColor = Color.green;
                    if (!audioSource.isPlaying)
                        audioSource.Play();
                    ChangeFloorVisiblity(true);
                    if (hit.collider != currentFloor)
                    {
                        currentFloor = hit.collider;
                    }
                }
                else
                {
                    ChangeVisuals(false, Color.red, Vector3.zero);
                }
            }
            else
            {
                ChangeVisuals(false, Color.black, Vector3.zero);
            }
        }
        else
        {
            ChangeVisuals(false, Color.black, Vector3.zero);
        }


        if (lineRen.enabled)
        {
            lineRen.SetPosition(0, new Vector3(transform.position.x, 1.75f, transform.position.z));
            lineRen.SetPosition(1, currentPoint);
            if (lineRen.startColor != rayColor)
            {
                lineRen.startColor = rayColor;
                lineRen.endColor = rayColor;
            }
        }
        Debug.DrawRay(transform.position, transform.forward * maxDistance, rayColor);
    }
}
