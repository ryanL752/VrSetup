using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : Selectable
{
    override public void Select()
    {
        Application.Quit();
    }
}
