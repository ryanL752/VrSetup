using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    static FirstPerson actions;

    public static Action<Vector2> moveAction;
    public static Action<float> moveArrows;
    public static Action<float> sizer;
    public static Action<Vector2> lookAction;
    public static Action interactAction;
    public static Action useAction;
    public static Action throwAction;
    public static Action jumpAction;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            actions = new(); // Actions must be created and enabled, Input Actions Manifest Script must be referenced above.
            actions.Enable();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        // Perform event subscriptions of methods to Input Actions
        actions.InGame.Movement.performed += InvokeMovement;
        actions.InGame.Cycle.performed += InvokeArrows;
        actions.InGame.Sizer.performed += InvokeSizer;
        actions.InGame.Movement.canceled += InvokeMovement;
        actions.InGame.MouseLook.performed += InvokeMouseLook;
        actions.InGame.MouseLook.canceled += InvokeMouseLook;
        //actions.InGame.Interact.started += InvokeInteract;
        actions.InGame.Interact.performed += InvokeInteract;
        //actions.InGame.Interact.canceled += InvokeInteract;
        actions.InGame.Use.started += InvokeUse;
        actions.InGame.Use.canceled += InvokeUse;
        actions.InGame.Throw.performed += InvokeThrow;
        //actions.InGame.Throw.started += InvokeThrow;
        //actions.InGame.Throw.canceled += InvokeThrow;
        actions.InGame.Jump.performed += InvokeJump;
    }

    void InvokeJump(InputAction.CallbackContext ctx)
    {
        jumpAction?.Invoke();
    }

    void InvokeInteract(InputAction.CallbackContext ctx)
    {
        interactAction?.Invoke();
    }

    void InvokeThrow(InputAction.CallbackContext ctx)
    {
        throwAction?.Invoke();
    }
    void InvokeUse(InputAction.CallbackContext ctx)
    {
        useAction?.Invoke();
    }

    void InvokeMovement(InputAction.CallbackContext ctx)
    {
        moveAction?.Invoke(ctx.ReadValue<Vector2>()); // Remember to pass in values for XY inputs
    }

    void InvokeArrows(InputAction.CallbackContext ctx)
    {
        moveArrows?.Invoke(ctx.ReadValue<float>());
    }

    void InvokeSizer(InputAction.CallbackContext ctx)
    {
        sizer?.Invoke(ctx.ReadValue<float>());
    }

    void InvokeMouseLook(InputAction.CallbackContext ctx)
    {
        lookAction?.Invoke(ctx.ReadValue<Vector2>());
    }

    void OnDisable()
    {
        // Don't forget to unsubscribe to events to prevent leaks.
        actions.InGame.Movement.performed -= InvokeMovement;
        actions.InGame.Movement.canceled -= InvokeMovement;
        actions.InGame.MouseLook.performed -= InvokeMouseLook;
        actions.InGame.MouseLook.canceled -= InvokeMouseLook;
        actions.InGame.Interact.performed -= InvokeInteract;
        //actions.InGame.Interact.canceled -= InvokeInteract;
        actions.InGame.Use.started -= InvokeUse;
        actions.InGame.Use.canceled -= InvokeUse;
        actions.InGame.Throw.performed -= InvokeThrow;
        actions.InGame.Jump.performed -= InvokeJump;
        actions.InGame.Cycle.performed -= InvokeArrows;
        actions.InGame.Sizer.performed -= InvokeSizer;
    }
}
