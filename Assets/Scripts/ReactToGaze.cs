using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactToGaze : MonoBehaviour, IRespondToGaze
{
    //head Rotation
    float verticalAngle = 0, horizontalAngle = 0;
    [SerializeField]
    int verticalLimit = 27;
    //Head Position
    [SerializeField]
    GameObject head;
    //Player Position
    [SerializeField]
    Transform player;

    //Should Stare?
    bool stare = false;

    //Animation controller
    Animator anim;

    void Start()
    {
        //assigns animation controller
        anim = GetComponent<Animator>();    
    }

    void Update()
    {
        if (stare)
        {
            //Sets Head Empty To Look At Player
            head.transform.LookAt(new Vector3(player.position.x, player.position.y+1, player.position.z));
            
            //Gets Rotation For X
            verticalAngle = head.transform.eulerAngles.x;
            //Gets Percentage for how far down should Mob Look
            //Converts to vertical angle to negative, because looking down is a positive value
            //when it should be a negative value for the controller
            //-1 being down, +1 being up
            //limit stops it from looking straight down or up
            verticalAngle = Mathf.Clamp((-verticalAngle / verticalLimit), -1, 1);

            //Takes Head empty value for Y rotation as where to look left to right
            //Also as a percentage of how far left to right should it look
            //-1 being left, +1 being right
            horizontalAngle = Mathf.Clamp(head.transform.rotation.y, -1, 1);

            //Sends the percentages to the animation controller
            anim.SetFloat("Vertical", verticalAngle);
            anim.SetFloat("Horizontal", horizontalAngle);
        }
    }

    public void StartGaze()
    {
        //Start Stare in Update
        stare = true;
    }

    public void HeldGaze()
    {
        //Start Attack Animation
        anim.SetTrigger("Attack");
    }

    public void StopGaze()
    {
        //Resets values
        stare = false;
        anim.SetFloat("Vertical", 0);
        anim.SetFloat("Horizontal", 0);
    }
}
