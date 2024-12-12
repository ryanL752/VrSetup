using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHolder : Selectable
{
    public bool menuHighlighted = false;
    public List<Selectable> menuOptions = new();
    public int currentSelection = 0;

    override public void Select()
    {
        menuOptions[currentSelection].Select();
    }
}
