using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeManager : MonoBehaviour
{
    [SerializeField]
    int state = 1;
    [SerializeField]
    float speed = 2, minSize = 0.1f, maxSize = 4f;

    [SerializeField]
    float maxMultiplyer = 8f, handOffset = 0.5f;

    Vector3 miniVector, maxiVector;
    public Vector3 targetVector;

    Jump jumpController;
    MovementController moveController;
    Rigidbody rig;

    [SerializeField]
    GrabAction grabAction;

    Vector3 orginalHandPos;
    Transform handPos;

    float defaultSpeed, defaultJump,defaultMass,defaultStrengthM;

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

        defaultSpeed = moveController.speed;
        defaultJump = jumpController.jumpForce;
        defaultMass = rig.mass;

        defaultStrengthM = grabAction.strengthMultiplier;

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
            rig.mass = defaultMass * minSize;
            moveController.speed = (defaultSpeed/2) * minSize;

            handPos.localPosition = new Vector3(orginalHandPos.x, orginalHandPos.y, orginalHandPos.z + handOffset);
        }
        else if(state == 1)
        {
            targetVector = Vector3.one;
            jumpController.jumpForce = defaultJump;
            grabAction.sizeMultiplier = 1;
            grabAction.strengthMultiplier = defaultStrengthM;
            rig.mass = defaultMass;
            moveController.speed = defaultSpeed;

            handPos.localPosition = new Vector3(orginalHandPos.x, orginalHandPos.y, orginalHandPos.z);
        }
        else
        {
            targetVector = maxiVector;
            jumpController.jumpForce = defaultJump * maxMultiplyer;
            grabAction.sizeMultiplier = maxMultiplyer;
            grabAction.strengthMultiplier = defaultStrengthM * maxMultiplyer;
            rig.mass = defaultMass * (maxMultiplyer/2);
            moveController.speed = defaultSpeed * maxMultiplyer;

            handPos.localPosition = new Vector3(orginalHandPos.x, orginalHandPos.y, orginalHandPos.z + handOffset);
        }
    }

    void OnDisable()
    {
        InputManager.sizer -= ChangeSize;
    }
}
