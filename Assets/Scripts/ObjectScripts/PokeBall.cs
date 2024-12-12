using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeBall : Equipment
{
    [SerializeField]
    bool active = false;
    [SerializeField]
    GrabAction player;

    [SerializeField]
    GameObject entity;
    public override void Action()
    {
        player.Throw();

        active = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(!active) { return; }

        if (collision.gameObject.CompareTag("Floor"))
        {
            GameObject pokemon = Instantiate(entity);

            pokemon.transform.position = transform.position + (Vector3.up*transform.localScale.y);

            active = false;
        }
    }
}
