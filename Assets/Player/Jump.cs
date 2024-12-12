using UnityEngine;

public class Jump : MonoBehaviour
{
    [Tooltip("Jump height, default 500f")]
    public float jumpForce = 500f;

    Rigidbody rig;
    bool isLocked = false;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        InputManager.jumpAction += PerformJump;
    }

    /// <summary>
    /// Locks or unlocks the ability to Jump
    /// </summary>
    /// <param name="state">Lock state (True/False)</param>
    public void SetJumpLock(bool state)
    {
        isLocked = state;
    }

    void PerformJump()
    {
        if (rig.velocity.y == 0f && !isLocked) // Can be done by states and collision with a floor-bound tag as well.
        {
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnDisable()
    {
        InputManager.jumpAction -= PerformJump;
    }
}
