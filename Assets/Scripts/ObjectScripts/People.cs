using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : Equipment
{
    [SerializeField]
    bool active = false;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public override void Action()
    {
        if (!active)
        {
            anim.SetTrigger("Death");
            active = true;
        }
    }
}
