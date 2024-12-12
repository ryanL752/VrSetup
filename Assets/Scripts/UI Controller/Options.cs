using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : Selectable
{
    [SerializeField]
    GameObject nextMenu;
    override public void Select()
    {
        nextMenu.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
}
