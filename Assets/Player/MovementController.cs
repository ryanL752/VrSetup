using UnityEngine;

public class MovementController : MonoBehaviour
{
    public static MovementController instance;
    
    [SerializeField][Tooltip("Camera to use for forward movement")]
    Transform cam;
    [Tooltip("Default walking speed, uses player mass, default 50,000")]
    public float speed = 50000f;

    bool isLocked = false;
    Rigidbody rig;
    Vector2 moveDir = Vector3.zero; // The direction of movement, WASD, Y/-Y = W/S, X/-X = D/A

    void Awake()
    {
        // The Singleton Pattern, with a rigidbody reference added.
        if (!instance)
        {
            instance = this;
            rig = GetComponent<Rigidbody>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        InputManager.moveAction += Move;
    }


    /// <summary>
    /// Locks or unlocks the ability to move
    /// </summary>
    /// <param name="state">Lock state (True/False)</param>
    public void SetMovementLock(bool state)
    {
        isLocked = state;
    }

    void Move(Vector2 moveDir)
    {
        this.moveDir = moveDir; // Sets direction for movement
    }

    void FixedUpdate()
    {
        // If not locked and W/A/S/D pressed, add force to rigidbody to move.
        if (moveDir != Vector2.zero && !isLocked)
        {
            rig.AddForce(speed * Time.fixedDeltaTime * (transform.forward * moveDir.y + transform.right * moveDir.x), ForceMode.Force);
        }
    }

    void OnDisable()
    {
        InputManager.moveAction -= Move;
    }
}
