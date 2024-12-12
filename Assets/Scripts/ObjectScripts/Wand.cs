using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : Equipment
{
    [SerializeField]
    ParticleSystem particles;
    [SerializeField]
    bool active;

    void Awake()
    {
        particles = GetComponentInChildren<ParticleSystem>();
    }
    public override void Action()
    {
        if (!active) 
        {
            particles.Play();
            active = true;
        }
        else
        {
            particles.Stop();
            active = false;
        }
    }
}
