using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : Equipment
{
    [SerializeField]
    SizeManager sizeManager;

    [SerializeField]
    bool active;

    public override void Action()
    {
        sizeManager.ChangeSize(+1);
        transform.localScale = sizeManager.targetVector;
    }
}
