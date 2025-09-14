using UnityEngine;
using UnityEngine.InputSystem;
public class GatherInput : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is create
    private Control myControl;
    public float valueX;
    public bool JumpInput;

    public void Awake()
    {
        myControl = new Control();
    }
    private void OnEnable()
    {
        myControl.Player.Move.performed += StartMove;
        myControl.Player.Move.canceled += StopMove;
        myControl.Player.Jump.performed += JumpStart;
        myControl.Player.Jump.canceled += JumpStope;
        myControl.Player.Enable();
    }
    public void OnDisable()
    {
        myControl.Player.Move.performed -= StartMove;
        myControl.Player.Move.canceled -= StopMove;
        myControl.Player.Jump.performed -= JumpStart;
        myControl.Player.Jump.canceled -= JumpStope;
        myControl.Player.Disable();
        valueX = 0; 
        //myControl.Disable();
    }
    private void StartMove(InputAction.CallbackContext ctx) {
        valueX = ctx.ReadValue<float>();
    }
    private void StopMove(InputAction.CallbackContext ctx) {
        valueX = 0;
    }
    private void JumpStart(InputAction.CallbackContext ctx)
    {
        JumpInput = true;
    }
    private void JumpStope(InputAction.CallbackContext ctx)
    {
        JumpInput = false;
    }
}
