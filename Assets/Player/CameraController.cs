using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [SerializeField][Tooltip("Camera rotation speed. Based on mouse movement. Default 1 or 2.")]
    float cameraSpeed = 2f;

    bool isLocked = false;
    Vector2 newDir = Vector2.zero;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Locks the cursor to the Game View and hides it.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEnable()
    {
        InputManager.lookAction += MouseLook;
    }

    /// <summary>
    /// Locks or unlocks the ability to rotate the Camera
    /// </summary>
    /// <param name="state">Lock state (True/False)</param>
    public void SetCameraLock(bool state)
    {
        isLocked = state;
    }

    /// <summary>
    /// Rotates the camera according to mouse movement
    /// </summary>
    /// <param name="lookDir">The XY movement of the mouse, which will be translated to camera rotation</param>
    void MouseLook(Vector2 lookDir)
    {
        if (!isLocked)
        {
            newDir += lookDir;
            transform.localRotation = Quaternion.Euler(Mathf.Clamp(-newDir.y * cameraSpeed, -90f, 90f), 0f, 0f);
            transform.parent.localRotation = Quaternion.Euler(0f, newDir.x * cameraSpeed, 0f);
        }
    }
    void OnDisable()
    {
        InputManager.lookAction -= MouseLook;
    }
}
