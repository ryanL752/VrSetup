using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : Selectable
{

    override public void Select()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
