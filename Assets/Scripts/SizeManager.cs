using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeManager : MonoBehaviour
{
    [SerializeField]
    int state = 1;
    [SerializeField]
    float speed = 2, minSize = 0.1f, maxSize = 4f;

    Vector3 miniVector, maxiVector;
    public Vector3 targetVector;

    Jump jumpController;
    MovementController moveController;
    Rigidbody rig;

    [SerializeField]
    GrabAction grabAction;

    Vector3 orginalHandPos;
    Transform handPos;

    void OnEnable()
    {
        InputManager.sizer += ChangeSize;
    }

    void Start()
    {
        moveController = GetComponent<MovementController>();
        jumpController = GetComponent<Jump>();
        rig = GetComponent<Rigidbody>();

        miniVector = new Vector3(minSize, minSize, minSize);
        maxiVector = new Vector3(maxSize, maxSize, maxSize);

        handPos = grabAction.gameObject.transform;
        orginalHandPos = handPos.localPosition;

        ChangeSize(0);
    }

    void Update()
    {
        if(transform.localScale == targetVector) { return; }

        transform.localScale = Vector3.Lerp(transform.localScale, targetVector, speed * Time.deltaTime);
    }

    public void ChangeSize(float plusMinus)
    {
        state = Mathf.Clamp(state + (int)plusMinus, 0, 2);

        if(state == 0)
        {
            targetVector = miniVector;
            jumpController.jumpForce = 15f;
            grabAction.sizeMultiplier = minSize;
            grabAction.strengthMultiplier = 2f;
            rig.mass = 72.5f*minSize;
            moveController.speed = 50000f * minSize;

            handPos.localPosition = new Vector3(orginalHandPos.x, orginalHandPos.y, orginalHandPos.z + 0.5f);
        }
        else if(state == 1)
        {
            targetVector = Vector3.one;
            jumpController.jumpForce = 500f;
            grabAction.sizeMultiplier = 1;
            grabAction.strengthMultiplier = 5;
            rig.mass = 72.5f;
            moveController.speed = 50000f;

            handPos.localPosition = new Vector3(orginalHandPos.x, orginalHandPos.y, orginalHandPos.z);
        }
        else
        {
            targetVector = maxiVector;
            jumpController.jumpForce = 500f * 8;
            grabAction.sizeMultiplier = 8;
            grabAction.strengthMultiplier = 5 * 8;
            rig.mass = 72.5f * 4;
            moveController.speed = 50000f * 8;

            handPos.localPosition = new Vector3(orginalHandPos.x, orginalHandPos.y, orginalHandPos.z + 0.5f);
        }
    }

    void OnDisable()
    {
        InputManager.sizer -= ChangeSize;
    }
}