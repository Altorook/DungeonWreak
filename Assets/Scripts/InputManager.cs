using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;
    TheControls inputActions;
    [SerializeField]
    GameObject usable;
    UsableItems usableItems;

    [SerializeField]
    GameObject axeObject;
    [SerializeField]
    GameObject swordObject;
    // Start is called before the first frame update
    void OnEnable()
    {

       usableItems = usable.GetComponent<UsableItems>();
        inputActions = new TheControls();
        inputActions.Player.Movement.performed += value => playerController.Movement(value.ReadValue<Vector2>());
        inputActions.Player.Sprint.started += value => playerController.EnableSprint();
        inputActions.Player.Sprint.canceled += value => playerController.DisableSprint();
        inputActions.Player.Interact.started += value => playerController.InteractWith();
        inputActions.Player.OneKey.started += value => usableItems.SwitchItem(1);
        inputActions.Player.TwoKey.started += value => usableItems.SwitchItem(2);
        inputActions.Player.ThreeKey.started += value => usableItems.SwitchItem(3);
        inputActions.Player.FourKey.started += value => usableItems.SwitchItem(4);
        inputActions.Player.FiveKey.started += value => usableItems.SwitchItem(5);

        if(axeObject != null)
        {
            inputActions.Player.LeftMouse.started += value => axeObject.GetComponent<AxeAttackController>().LAttack();
            inputActions.Player.RightMouse.started += value => axeObject.GetComponent<AxeAttackController>().RAttack();
        }
        if (swordObject != null)
        {
            inputActions.Player.LeftMouse.started += value => swordObject.GetComponent<AttackController>().LAttack();
            inputActions.Player.RightMouse.started += value => swordObject.GetComponent<AttackController>().RAttack();
        }
        //     inputActions.Player.Mouse.performed += value => playerController.Rotation(value.ReadValue<Vector2>());
        inputActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
