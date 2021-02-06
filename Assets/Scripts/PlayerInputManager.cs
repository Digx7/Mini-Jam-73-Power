using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2Event HorizonatlMovement;
    public Vector2Event HorizontalMovementStopped;
    public UnityEvent ShootStarted;
    public UnityEvent ShootStopped;
    public Vector2Event Rotation;

    private Controls controls;

    public void Awake() {
      controls = new Controls();

      setUpInputs();
    }

    private void setUpInputs(){
      controls.Player.Move_H.performed += ctx => HorizonatlMovement.Invoke(ctx.ReadValue<Vector2>());
      controls.Player.Move_H.canceled += ctx => HorizontalMovementStopped.Invoke(ctx.ReadValue<Vector2>());
      controls.Player.Shoot.performed += ctx => ShootStarted.Invoke();
      controls.Player.Shoot.canceled += ctx => ShootStopped.Invoke();
      controls.Player.Rotate.performed += ctx => Rotation.Invoke(ctx.ReadValue<Vector2>());
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }
}
