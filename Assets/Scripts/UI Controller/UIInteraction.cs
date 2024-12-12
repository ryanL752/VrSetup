using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteraction : MonoBehaviour
{
    [SerializeField]
    float distance = 5f;
    Selectable currentSelectable;
    Color rayColor;
    [SerializeField]
    GameObject menu;

    void OnEnable()
    {
        //using throw action as the trigger for a VR controller, it is binded to e
        InputManager.moveArrows += Arrows;
        InputManager.throwAction += Throw;
        InputManager.useAction += Interact;
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distance))
        {
            if (hit.collider.gameObject.GetComponent<Selectable>() != null)
            {
                rayColor = Color.green;
                if (hit.collider.gameObject.GetComponent<Selectable>() != currentSelectable)
                {
                    if(currentSelectable == null)
                    {
                        currentSelectable = hit.collider.gameObject.GetComponent<Selectable>();
                    }


                    if (currentSelectable.TryGetComponent(out MenuHolder menuHolder))
                    {
                        menuHolder.menuHighlighted = true;
                    }
                    else
                    {
                        if (currentSelectable != null)
                        {
                            currentSelectable.DeHighlight();
                        }
                    }
                    
                    currentSelectable = hit.collider.gameObject.GetComponent<Selectable>();
                    currentSelectable.Highlight();
                }
            }
        }
        else
        {
            rayColor = Color.black;
            if (currentSelectable != null)
            {
                if (currentSelectable.TryGetComponent(out MenuHolder menuHolder))
                    menuHolder.menuHighlighted = false;
                else
                {
                    currentSelectable.DeHighlight();
                }
            }
            currentSelectable = null;
        }
        /*
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, transform.forward * distance);
        GetComponent<LineRenderer>().material.color = rayColor;
        */
        Debug.DrawRay(transform.position, transform.forward * distance, rayColor);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void Throw()
    {
        if(!menu.activeInHierarchy)
        {
            menu.SetActive(true);
        }
    }

    void Interact()
    {
        if(currentSelectable != null && currentSelectable.transform.parent.gameObject.activeInHierarchy)
        {
            currentSelectable.Select();
        }
    }

    void Arrows(float dir)
    {
        if (currentSelectable && currentSelectable.TryGetComponent(out MenuHolder menuHolder)) 
        {
            if (menuHolder.menuHighlighted)
            {
                /*
                if (currentSelectable != null)
                    currentSelectable.DeHighlight();
                */

                menuHolder.menuOptions[menuHolder.currentSelection].DeHighlight();

                menuHolder.currentSelection = Mathf.Clamp(menuHolder.currentSelection + (int)dir, 0, menuHolder.menuOptions.Count - 1);

                menuHolder.menuOptions[menuHolder.currentSelection].Highlight();
                //currentSelectable = menuHolder.menuOptions[menuHolder.currentSelection];
            }
        }
    }
}
