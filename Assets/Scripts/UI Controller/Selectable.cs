using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selectable : MonoBehaviour
{
    [SerializeField]
    Image image;

    virtual public void Select() { }

    public void Highlight()
    {
        image.color = Color.yellow;
    }
    
    public void DeHighlight() 
    {
        image.color = Color.white;
    }

    void Start()
    {
        image = GetComponent<Image>();
        DeHighlight();
    }
}