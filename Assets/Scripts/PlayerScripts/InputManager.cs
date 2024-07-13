using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    public event Action<Vector2> onMove;
    public event Action<Vector2> onLook;

    public event Action<bool> onUseRMB;
    public event Action<bool> onUseLMB;
    public event Action<bool> onStorageUI;
    public event Action<bool> onOpenShop;
    public event Action<bool> onHiddenInventory;
    public event Action<bool> onSprint;
    public event Action onChestInteract;
    public Vector2 moveInput;

    private void OnEnable()
    {
        SetupInput();

    }
    private void SetupInput()
    {
        PlayerInput playerInput = new PlayerInput();

        playerInput.Player.Move.performed +=OnMove;
        playerInput.Player.Sprint.performed += OnSprint;
        playerInput.Player.Look.performed += OnLook;
        playerInput.Player.Sprint.canceled += OnSprint;

        playerInput.UseItem.RMB.performed += OnUseRMB;
        playerInput.UseItem.LMB.performed += OnUseLMB;

        playerInput.Interact.StorageUI.performed += OnStorageUI;
        playerInput.Interact.OpenShop.performed += OnOpenShop;
        playerInput.Interact.HandleInventory.performed += OnHiddenInventory;
        playerInput.Interact.ChestInteract.performed += OnChestInteract;


        playerInput.Enable();

    }
    private void OnMove(InputAction.CallbackContext context)
    {
        onMove?.Invoke(context.ReadValue<Vector2>());
    }

 private void OnChestInteract(InputAction.CallbackContext context) 
    {
        onChestInteract?.Invoke(); //
    }

    private void OnSprint(InputAction.CallbackContext context)
    {

        if (context.performed) 
        {
            onSprint?.Invoke(true);
        }
        else if (context.canceled) 
        {
            onSprint?.Invoke(false); 
        }


    }
    private void OnStorageUI(InputAction.CallbackContext context)
    {

        float pButtonValue = context.ReadValue<float>();
        if (pButtonValue > 0.5f)
        {
            onStorageUI?.Invoke(true);
        }

    }
    private void OnOpenShop(InputAction.CallbackContext context)
    {

        float oButtonValue = context.ReadValue<float>();
        if (oButtonValue > 0.5f)
        {
            onOpenShop?.Invoke(true);
        }

    }
    private void OnHiddenInventory(InputAction.CallbackContext context)
    {

        float tabButtonValue = context.ReadValue<float>();
        if (tabButtonValue > 0.5f)
        {
            onHiddenInventory?.Invoke(true);
        }

    }

    private void OnUseRMB(InputAction.CallbackContext context)
    {

        float rightButtonValue = context.ReadValue<float>();
        if (rightButtonValue > 0.5f) 
        {
            onUseRMB?.Invoke(true); 
        }


    }
    private void OnUseLMB(InputAction.CallbackContext context)
    {
        float leftButtonValue = context.ReadValue<float>();
        if (leftButtonValue > 0.5f) 
        {
            onUseLMB?.Invoke(true); 
        }

    }
    private void OnLook(InputAction.CallbackContext context)
    {
        onLook?.Invoke(context.ReadValue<Vector2>());

    }

}
